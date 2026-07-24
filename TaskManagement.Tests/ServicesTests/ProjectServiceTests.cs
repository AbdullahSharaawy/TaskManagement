using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementBLL.DTOs.ProjectDTOs;
using TaskManagementBLL.Services.Implementation;
using TaskManagementDAL.Entities;
using TaskManagementDAL.FilterModels.Project;
using TaskManagementDAL.Repository.Abstraction;

namespace TaskManagement.Tests.ServicesTests
{
    public class ProjectServiceTests
    {
        // A.Fake<T>() creates a mock implementation of the repository interface.
        // We never touch a real DbContext here — that's the whole point of unit-testing the service.
        private readonly IProjectRepository _projectRepository = A.Fake<IProjectRepository>();
        private readonly ProjectService _sut; // "system under test"

        public ProjectServiceTests()
        {
            _sut = new ProjectService(_projectRepository);
        }

        [Fact]
        public async Task AddProjectAsync_WhenRepositorySucceeds_ReturnsSuccessResponseWithMappedData()
        {
            // Arrange
            var dto = new CreateProjectDto { name = "Website Revamp", description = "Q3 project" };

            // Whenever the repository's AddProjectAsync is called with ANY project, return this fake saved entity.
            A.CallTo(() => _projectRepository.AddProjectAsync(A<project>._))
                .Returns(new project
                {
                    Id = 1,
                    name = dto.name,
                    description = dto.description,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                });

            // Act
            var result = await _sut.AddProjectAsync(dto);

            // Assert — FluentAssertions reads like plain English and gives much better failure messages
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.name.Should().Be("Website Revamp");
            result.Message.Should().Be("The project was added successfully.");
        }

        [Fact]
        public async Task AddProjectAsync_WhenRepositoryReturnsNull_ReturnsFailureResponse()
        {
            // Arrange — simulate SaveChangesAsync affecting 0 rows (repository returns null)
            var dto = new CreateProjectDto { name = "Duplicate Name" };
            A.CallTo(() => _projectRepository.AddProjectAsync(A<project>._))
                .Returns((project?)null);

            // Act
            var result = await _sut.AddProjectAsync(dto);

            // Assert
            result.Success.Should().BeFalse();
            result.Data.Should().BeNull();
            result.Message.Should().Contain("Failed");
        }

        [Fact]
        public async Task GetByIdAsync_WhenProjectExists_ReturnsMappedProject()
        {
            // Arrange
            var entity = new project { Id = 5, name = "Mobile App", created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow };
            A.CallTo(() => _projectRepository.GetByIdAsync(5)).Returns(entity);

            // Act
            var result = await _sut.GetByIdAsync(5);

            // Assert
            result.Success.Should().BeTrue();
            result.Data!.Id.Should().Be(5);
            result.Data.name.Should().Be("Mobile App");
        }

        [Fact]
        public async Task GetByIdAsync_WhenProjectDoesNotExist_ReturnsFailure()
        {
            // Arrange
            A.CallTo(() => _projectRepository.GetByIdAsync(A<int>._)).Returns((project?)null);

            // Act
            var result = await _sut.GetByIdAsync(999);

            // Assert
            result.Success.Should().BeFalse();
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_WhenRepositorySucceeds_ReturnsTrueSuccessResponse()
        {
            // Arrange
            A.CallTo(() => _projectRepository.DeleteAsync(1)).Returns(true);

            // Act
            var result = await _sut.DeleteAsync(1);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().BeTrue();

            // Verify the repository was actually called exactly once with the right id —
            // this is the "mocking" half of the ask: proving the service talks to its dependency correctly.
            A.CallTo(() => _projectRepository.DeleteAsync(1)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteAsync_WhenRepositoryFails_ReturnsFalseFailureResponse()
        {
            // Arrange
            A.CallTo(() => _projectRepository.DeleteAsync(A<int>._)).Returns(false);

            // Act
            var result = await _sut.DeleteAsync(42);

            // Assert
            result.Success.Should().BeFalse();
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task GetAllAsync_WhenNoProjectsFound_ReturnsFailureWithZeroCount()
        {
            // Arrange
            var query = new ProjectQueryParameters { Page = 1, Limit = 10 };
            A.CallTo(() => _projectRepository.GetAllAsync(query))
                .Returns((Enumerable.Empty<project>(), 0));

            // Act
            var result = await _sut.GetAllAsync(query);

            // Assert
            result.Success.Should().BeFalse();
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllAsync_WhenProjectsExist_ReturnsMappedListWithCount()
        {
            // Arrange
            var query = new ProjectQueryParameters { Page = 1, Limit = 10 };
            var projects = new List<project>
            {
                new() { Id = 1, name = "Alpha", created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow },
                new() { Id = 2, name = "Beta", created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow }
            };
            A.CallTo(() => _projectRepository.GetAllAsync(query)).Returns((projects, 2));

            // Act
            var result = await _sut.GetAllAsync(query);

            // Assert
            result.Success.Should().BeTrue();
            result.Count.Should().Be(2);
            result.Data.Should().HaveCount(2);
            result.Data.Should().Contain(p => p.name == "Alpha");
        }

        [Fact]
        public async Task UpdateAsync_SetsIdFromRouteParameterBeforeCallingRepository()
        {
            // Arrange
            var dto = new UpdateProjectDto { name = "Renamed Project" };
            project? capturedEntity = null;

            // Capture whatever entity the service passes to the repository so we can assert on it
            A.CallTo(() => _projectRepository.UpdateAsync(A<project>._))
                .Invokes((project p) => capturedEntity = p)
                .Returns(new project { Id = 7, name = dto.name, created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow });

            // Act
            var result = await _sut.UpdateAsync(dto, projectId: 7);

            // Assert
            result.Success.Should().BeTrue();
            capturedEntity.Should().NotBeNull();
            capturedEntity!.Id.Should().Be(7); // proves the service correctly stamped the route id onto the mapped entity
        }
    }
}
