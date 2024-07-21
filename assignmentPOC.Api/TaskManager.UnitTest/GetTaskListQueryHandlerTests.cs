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

namespace TaskManager.UnitTest
{
    public class GetTaskListQueryHandlerTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<IActivityRepository> _activityRepositoryMock;
        private readonly Mock<ITagRepository> _tagRepositoryMock;
        private readonly GetTaskListQueryHandler _handler;

        public GetTaskListQueryHandlerTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _activityRepositoryMock = new Mock<IActivityRepository>();
            _tagRepositoryMock = new Mock<ITagRepository>();
            _handler = new GetTaskListQueryHandler(
                _taskRepositoryMock.Object,
                _activityRepositoryMock.Object,
                _tagRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsTaskList()
        {
            // Arrange
            var tasks = new List<TaskDetail>
            {
                new TaskDetail { Id = 1, TaskName = "Task 1", DueDate = DateTime.Now, Color = "Red", Status = "Pending", AssignedTo = "User1", IsDeleted = false },
                new TaskDetail { Id = 2, TaskName = "Task 2", DueDate =  DateTime.Now, Color = "Blue", Status = "Completed", AssignedTo = "User2", IsDeleted = false }
            };

            var tags = new List<Tag>
            {
                new Tag { Id = 1, Name = "Tag1", TaskDetailId = 1, IsDeleted = false },
                new Tag { Id = 2, Name = "Tag2", TaskDetailId = 1, IsDeleted = false }
            };

            var activities = new List<ActivityDetail>
            {
                new ActivityDetail { Id = 1, ActivityDate = DateTime.Now, Description = "Activity 1", DoneBy = "User1", TaskDetailId = 1, IsDeleted = false },
                new ActivityDetail { Id = 2, ActivityDate = DateTime.Now, Description = "Activity 2", DoneBy = "User2", TaskDetailId = 1, IsDeleted = false }
            };

            _taskRepositoryMock.Setup(repo => repo.GetTasksAsync()).ReturnsAsync(tasks);
            _tagRepositoryMock.Setup(repo => repo.GetActivityAsync()).ReturnsAsync(tags);
            _activityRepositoryMock.Setup(repo => repo.GetActivityAsync()).ReturnsAsync(activities);

            var query = new GetTaskListQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            var task1 = result.FirstOrDefault(t => t.Id == 1);
            Assert.NotNull(task1);
            Assert.Equal("Task 1", task1.TaskName);
            Assert.Equal(2, task1.Tags.Count);
            Assert.Equal(2, task1.Activities.Count);

            var task2 = result.FirstOrDefault(t => t.Id == 2);
            Assert.NotNull(task2);
            Assert.Equal("Task 2", task2.TaskName);
            Assert.Empty(task2.Tags);
            Assert.Empty(task2.Activities);
        }
    }
}
