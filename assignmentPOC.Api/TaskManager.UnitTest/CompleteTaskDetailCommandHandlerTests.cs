using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TaskManager.Data.Repositories;
using TaskManager.Application.Features;
using Xunit;
using TaskManager.Application.Features.Tasks.Queries;
using TaskManager.Domain.Entities;
using TaskManager.Application.Features.Tasks.Command;

namespace TaskManager.UnitTest
{
    public class CompleteTaskDetailCommandHandlerTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<IActivityRepository> _activityRepositoryMock;
        private readonly CompleteTaskDetailCommandHandler _handler;

        public CompleteTaskDetailCommandHandlerTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _activityRepositoryMock = new Mock<IActivityRepository>();
            _handler = new CompleteTaskDetailCommandHandler(
                _taskRepositoryMock.Object,
                _activityRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_TaskExists_CompleteTask()
        {
            // Arrange
            var command = new CompleteTaskDetailCommand
            {
                
                    TaskDetailId = 1,
                  
            };

            var task = new TaskDetail { Id = 1, Status = "Pending" };

            _taskRepositoryMock.Setup(repo => repo.GetTaskByIdAsync(1)).ReturnsAsync(task);
            _taskRepositoryMock.Setup(repo => repo.UpdateTaskAsync(It.IsAny<TaskDetail>()));
            _activityRepositoryMock.Setup(repo => repo.AddActivityAsync(It.IsAny<ActivityDetail>()));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _taskRepositoryMock.Verify(repo => repo.GetTaskByIdAsync(1), Times.Once);
            _taskRepositoryMock.Verify(repo => repo.UpdateTaskAsync(It.Is<TaskDetail>(t => t.Status == "Completed")), Times.Once);
            _activityRepositoryMock.Verify(repo => repo.AddActivityAsync(It.Is<ActivityDetail>(a =>
                a.TaskDetailId == 1 &&
                a.Description == "Completed the task" &&
                a.DoneBy == "User1")), Times.Once);
        }

        [Fact]
        public async Task Handle_TaskDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var command = new CompleteTaskDetailCommand
            {
                TaskDetailId = 1,
            };

            _taskRepositoryMock.Setup(repo => repo.GetTaskByIdAsync(1)).ReturnsAsync((TaskDetail)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _taskRepositoryMock.Verify(repo => repo.GetTaskByIdAsync(1), Times.Once);
            _taskRepositoryMock.Verify(repo => repo.UpdateTaskAsync(It.IsAny<TaskDetail>()), Times.Never);
            _activityRepositoryMock.Verify(repo => repo.AddActivityAsync(It.IsAny<ActivityDetail>()), Times.Never);
        }
    }
}
