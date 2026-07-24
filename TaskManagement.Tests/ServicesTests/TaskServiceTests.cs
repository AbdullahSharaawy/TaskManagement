using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementBLL.DTOs.TaskDTOs;
using TaskManagementBLL.Services.Implementation;
using TaskManagementDAL.Entities;
using TaskManagementDAL.Enums;
using TaskManagementDAL.FilterModels.Task;
using TaskManagementDAL.Repository.Abstraction;

namespace TaskManagement.Tests.ServicesTests
{
    public class TaskServiceTests
    {
        private readonly ITaskRepository _taskRepository = A.Fake<ITaskRepository>();
        private readonly TaskService _sut;

        public TaskServiceTests()
        {
            _sut = new TaskService(_taskRepository);
        }

        [Fact]
        public async Task GetByIdAsync_WhenTaskExists_ReturnsMappedTask()
        {
            // Arrange
            var entity = new task
            {
                Id = 10,
                title = "Write tests",
                status = Status.todo,
                priority = TaskManagementDAL.Enums.Priority.high,
                project_id = 1,
                project = new project { Id = 1, name = "Backend Task" },
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };
            A.CallTo(() => _taskRepository.GetByIdAsync(10)).Returns(entity);

            // Act
            var result = await _sut.GetByIdAsync(10);

            // Assert
            result.Success.Should().BeTrue();
            result.Data!.title.Should().Be("Write tests");
            result.Data.priority.Should().Be(TaskManagementDAL.Enums.Priority.high);
        }

        [Fact]
        public async Task GetByIdAsync_WhenTaskMissing_ReturnsFailure()
        {
            // Arrange
            A.CallTo(() => _taskRepository.GetByIdAsync(A<int>._)).Returns((task?)null);

            // Act
            var result = await _sut.GetByIdAsync(999);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Failed to find the Task.");
        }

        [Fact]
        public async Task DeleteAsync_WhenSuccessful_ReturnsTrue()
        {
            A.CallTo(() => _taskRepository.DeleteAsync(3)).Returns(true);

            var result = await _sut.DeleteAsync(3);

            result.Success.Should().BeTrue();
            result.Data.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_WhenTaskDoesNotExist_ReturnsFailure()
        {
            A.CallTo(() => _taskRepository.DeleteAsync(A<int>._)).Returns(false);

            var result = await _sut.DeleteAsync(404);

            result.Success.Should().BeFalse();
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateAsync_StampsRouteIdOntoMappedEntityBeforeCallingRepository()
        {
            // Arrange
            var dto = new UpdateTaskDto { title = "Updated title", status = Status.in_progress };
            task? captured = null;

            A.CallTo(() => _taskRepository.UpdateAsync(A<task>._))
                .Invokes((task t) => captured = t)
                .Returns(new task
                {
                    Id = 8,
                    title = dto.title,
                    status = dto.status,
                    project = new project { Id = 1, name = "Some Project" },
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                });

            // Act
            var result = await _sut.UpdateAsync(dto, taskId: 8);

            // Assert
            result.Success.Should().BeTrue();
            captured!.Id.Should().Be(8);
            result.Data!.title.Should().Be("Updated title");
        }

        [Fact]
        public async Task GetAllAsync_WhenNoTasksMatch_ReturnsFailureWithZeroCount()
        {
            // Arrange
            var query = new TaskQueryParameters { Page = 1, Limit = 20 };
            A.CallTo(() => _taskRepository.GetAllAsync(query, null))
                .Returns((new List<task>(), 0));

            // Act
            var result = await _sut.GetAllAsync(query);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("No tasks found.");
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllAsync_WhenTasksExist_ReturnsPagedResultWithProjectNameFlattened()
        {
            // Arrange
            var query = new TaskQueryParameters { Page = 1, Limit = 20 };
            var tasks = new List<task>
            {
                new()
                {
                    Id = 1, title = "Task A", project_id = 1,
                    project = new project { Id = 1, name = "Project X" },
                    status = Status.todo, priority = TaskManagementDAL.Enums.Priority.medium,
                    created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow
                }
            };
            A.CallTo(() => _taskRepository.GetAllAsync(query, null)).Returns((tasks, 1));

            // Act
            var result = await _sut.GetAllAsync(query);

            // Assert
            result.Success.Should().BeTrue();
            result.Data!.Items.Should().HaveCount(1);
            result.Data.Items[0].project_name.Should().Be("Project X"); // proves the t.project.name flattening works
            result.Data.TotalCount.Should().Be(1);
            result.Data.Page.Should().Be(1);
        }

        [Fact]
        public async Task GetAllAsync_WhenPageIsLessThanOne_NormalizesToPageOne()
        {
            // Arrange — the service clamps invalid paging input before calling the repository
            var query = new TaskQueryParameters { Page = -5, Limit = 0 };
            A.CallTo(() => _taskRepository.GetAllAsync(A<TaskQueryParameters>._, null))
                .Returns((new List<task>(), 0));

            // Act
            await _sut.GetAllAsync(query);

            // Assert — verify the repository was called with the *normalized* query, not the raw input
            A.CallTo(() => _taskRepository.GetAllAsync(
                    A<TaskQueryParameters>.That.Matches(q => q.Page == 1 && q.Limit == 20),
                    null))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetAllAsync_WhenProjectIdProvided_PassesItThroughToRepository()
        {
            // Arrange
            var query = new TaskQueryParameters { Page = 1, Limit = 10 };
            A.CallTo(() => _taskRepository.GetAllAsync(query, 7))
                .Returns((new List<task>(), 0));

            // Act
            await _sut.GetAllAsync(query, projectId: 7);

            // Assert — this is the scenario behind GET /api/projects/{id}/tasks
            A.CallTo(() => _taskRepository.GetAllAsync(query, 7)).MustHaveHappenedOnceExactly();
        }
    }
}
