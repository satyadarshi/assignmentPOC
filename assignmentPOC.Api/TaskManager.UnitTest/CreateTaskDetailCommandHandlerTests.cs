using MediatR;
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
    public class CreateTaskDetailCommandHandlerTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<IActivityRepository> _activityRepositoryMock;
        private readonly Mock<ITagRepository> _tagRepositoryMock;
        private readonly CreateTaskDetailCommandHandler _handler;

        public CreateTaskDetailCommandHandlerTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _activityRepositoryMock = new Mock<IActivityRepository>();
            _tagRepositoryMock = new Mock<ITagRepository>();
            _handler = new CreateTaskDetailCommandHandler(
                _taskRepositoryMock.Object,
                _activityRepositoryMock.Object,
                _tagRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsTrue()
        {
            // Arrange
            var command = new CreateTaskDetailCommand
            {
                Details = new List<CreateTaskDetailDto>
                {
                    new CreateTaskDetailDto
                    {
                        task_name = "Test Task",
                        due_date = DateTime.Now,
                        color = "Red",
                        assigned_to = "User1",
                        status = "Pending",
                        tags = new List<string> { "tag1", "tag2" }
                    }
                }
            };

            _taskRepositoryMock.Setup(repo => repo.AddTaskAsync(It.IsAny<TaskDetail>())).ReturnsAsync(1);
            _activityRepositoryMock.Setup(repo => repo.AddActivityAsync(It.IsAny<ActivityDetail>())).ReturnsAsync(1);
            _tagRepositoryMock.Setup(repo => repo.AddTagAsync(It.IsAny<Tag>())).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _taskRepositoryMock.Verify(repo => repo.AddTaskAsync(It.IsAny<TaskDetail>()), Times.Once);
            _activityRepositoryMock.Verify(repo => repo.AddActivityAsync(It.IsAny<ActivityDetail>()), Times.Once);
            _tagRepositoryMock.Verify(repo => repo.AddTagAsync(It.IsAny<Tag>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_RepositoryAddTaskFails_ReturnsFalse()
        {
            // Arrange
            var command = new CreateTaskDetailCommand
            {
                Details = new List<CreateTaskDetailDto>
                {
                    new CreateTaskDetailDto
                    {
                        task_name = "Test Task",
                        due_date = DateTime.Now,
                        color = "Red",
                        assigned_to = "User1",
                        status = "Pending",
                        tags = new List<string> { "tag1", "tag2" }
                    }
                }
            };

            _taskRepositoryMock.Setup(repo => repo.AddTaskAsync(It.IsAny<TaskDetail>())).ReturnsAsync(0);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _taskRepositoryMock.Verify(repo => repo.AddTaskAsync(It.IsAny<TaskDetail>()), Times.Once);
            _activityRepositoryMock.Verify(repo => repo.AddActivityAsync(It.IsAny<ActivityDetail>()), Times.Never);
            _tagRepositoryMock.Verify(repo => repo.AddTagAsync(It.IsAny<Tag>()), Times.Never);
        }
    }
}
