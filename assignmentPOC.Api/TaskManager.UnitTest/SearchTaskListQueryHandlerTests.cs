using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TaskManager.Application.Features.Tasks.Queries;
using TaskManager.Data.Repositories;
using TaskManager.Domain.Entities;
using Xunit;

namespace TaskManager.UnitTest
{
    public class SearchTaskListQueryHandlerTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<IActivityRepository> _activityRepositoryMock;
        private readonly Mock<ITagRepository> _tagRepositoryMock;
        private readonly SearchTaskListQueryHandler _handler;

        public SearchTaskListQueryHandlerTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _activityRepositoryMock = new Mock<IActivityRepository>();
            _tagRepositoryMock = new Mock<ITagRepository>();
            _handler = new SearchTaskListQueryHandler(
                _taskRepositoryMock.Object, _activityRepositoryMock.Object, _tagRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsExpectedTaskList()
        {
            // Arrange
            var query = new SearchTaskListQuery
            {
                taskName = "Test Task",
                startDueDate = DateTime.Now.AddDays(-1),
                endDueDate = DateTime.Now.AddDays(1),
                status = new List<string> { "PENDING" }
            };

            var tasks = new List<TaskDetail>
            {
                new TaskDetail { Id = 1, TaskName = "Test Task", DueDate = DateTime.Now, Color = "Red", Status = "PENDING", AssignedTo = "User1", IsDeleted = false }
            };

            var tags = new List<Tag>
            {
                new Tag { Id = 1, Name = "Tag1", TaskDetailId = 1, IsDeleted = false }
            };

            var activities = new List<ActivityDetail>
            {
                new ActivityDetail { Id = 1, ActivityDate = DateTime.Now, Description = "Activity1", DoneBy = "User1", TaskDetailId = 1, IsDeleted = false }
            };

            _taskRepositoryMock.Setup(repo => repo.SearchTasksAsync(It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<List<string>>()))
                .ReturnsAsync(tasks);
            _tagRepositoryMock.Setup(repo => repo.GetActivityAsync())
                .ReturnsAsync(tags);
            _activityRepositoryMock.Setup(repo => repo.GetActivityAsync())
                .ReturnsAsync(activities);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Single(result);
            var task = result.First();
            Assert.Equal(1, task.Id);
            Assert.Equal("Test Task", task.TaskName);
            Assert.Equal("Red", task.Color);
            Assert.Equal("PENDING", task.Status);
            Assert.Equal("User1", task.AssignedTo);
            Assert.Single(task.Tags);
            Assert.Equal("Tag1", task.Tags.First().Name);
            Assert.Single(task.Activities);
            Assert.Equal("Activity1", task.Activities.First().Description);
        }
    }
}