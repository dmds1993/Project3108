using Domain.Entities;
using Domain.Factories;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.Models;
using Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock = new();
        private readonly Mock<IPBIRepository> _pbiRepositoryMock = new();
        private readonly ITaskService _taskService;

        public TaskServiceTests()
        {
            _taskService = new TaskService(_taskRepositoryMock.Object, _pbiRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ValidTaskModels_SuccessResponse()
        {
            var taskModels = new List<TaskModel>
            {
                new TaskModel { Name = "Task 1", PBIId = 1, Description = "Teste", Image = "Image", Deadline = DateTime.Now },
                new TaskModel { Name = "Task 2", PBIId = 1, Description = "Teste Description", Image = "image", Deadline = DateTime.Now }
            };
            _pbiRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new PBIEntity());

            var response = await _taskService.CreateAsync(taskModels);

            Assert.True(response.Success);
            Assert.Equal("Tasks created successfully", response.Message);
        }

        [Fact]
        public async Task CreateAsync_InvalidTaskModel_FailureResponse()
        {
            var taskModels = new List<TaskModel>
            {
                new TaskModel { Name = null, PBIId = 1 }
            };

            var response = await _taskService.CreateAsync(taskModels);

            Assert.False(response.Success);
            Assert.Equal("Validation error in Task model", response.Message);
            Assert.NotNull(response.Errors);
            Assert.NotEmpty(response.Errors);
        }

        [Fact]
        public async Task CreateAsync_PBINotFound_FailureResponse()
        {
            var taskModels = new List<TaskModel>
            {
                new TaskModel { Name = "Task 1", PBIId = 1 }
            };
            _pbiRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PBIEntity)null);

            var response = await _taskService.CreateAsync(taskModels);

            Assert.False(response.Success);
            Assert.Equal("Validation error in Task model", response.Message);
        }

        [Fact]
        public async Task DeleteAsync_ExistingTask_SuccessResponse()
        {
            var taskId = 1;
            var existingTask = new TaskEntity { Id = taskId, Name = "Task 1" };
            _taskRepositoryMock.Setup(repo => repo.GetTaskByIdAsync(taskId)).ReturnsAsync(existingTask);

            var response = await _taskService.DeleteAsync(taskId);

            Assert.True(response.Success);
            Assert.Equal("Task deleted successfully", response.Message);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingTask_FailureResponse()
        {
            var taskId = 1;
            _taskRepositoryMock.Setup(repo => repo.GetTaskByIdAsync(taskId)).ReturnsAsync((TaskEntity)null);

            var response = await _taskService.DeleteAsync(taskId);

            Assert.False(response.Success);
            Assert.Equal($"Task with ID {taskId} not found.", response.Message);
        }
    }
}
