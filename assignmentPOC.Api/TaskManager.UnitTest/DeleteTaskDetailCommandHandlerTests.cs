using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Features.Tasks.Command;
using TaskManager.Data.Repositories;
using TaskManager.Domain.Entities;

namespace TaskManager.UnitTest
{
    public class DeleteTaskDetailCommandHandlerTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<IActivityRepository> _activityRepositoryMock;
        private readonly Mock<ITagRepository> _tagRepositoryMock;
        private readonly DeleteTaskDetailCommandHandler _handler;

        public DeleteTaskDetailCommandHandlerTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _activityRepositoryMock = new Mock<IActivityRepository>();
            _tagRepositoryMock = new Mock<ITagRepository>();
            _handler = new DeleteTaskDetailCommandHandler(
                _taskRepositoryMock.Object,
                _activityRepositoryMock.Object,
                _tagRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_TaskExists_SetsIsDeletedTrue()
        {
            // Arrange
            var command = new DeleteTaskDetailCommand { Id = 1 };
            var task = new TaskDetail { Id = 1, IsDeleted = false };

            _taskRepositoryMock.Setup(repo => repo.GetTaskByIdAsync(1)).ReturnsAsync(task);
            _taskRepositoryMock.Setup(repo => repo.UpdateTaskAsync(It.IsAny<TaskDetail>()));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _taskRepositoryMock.Verify(repo => repo.GetTaskByIdAsync(1), Times.Once);
            _taskRepositoryMock.Verify(repo => repo.UpdateTaskAsync(It.Is<TaskDetail>(t => t.IsDeleted)), Times.Once);
        }

        [Fact]
        public async Task Handle_TaskDoesNotExist_ReturnsTrue()
        {
            // Arrange
            var command = new DeleteTaskDetailCommand { Id = 1 };

            _taskRepositoryMock.Setup(repo => repo.GetTaskByIdAsync(1)).ReturnsAsync((TaskDetail)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _taskRepositoryMock.Verify(repo => repo.GetTaskByIdAsync(1), Times.Once);
            _taskRepositoryMock.Verify(repo => repo.UpdateTaskAsync(It.IsAny<TaskDetail>()), Times.Never);
        }
    }
}