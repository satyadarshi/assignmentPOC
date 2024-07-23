using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Tasks.Command;
using TaskManager.Application.Features.Tasks.Queries;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/taskmanager/taskdetail/[action]")]
    public class TaskDetailController : BaseController
    {
        private readonly ILogger _logger;

        public TaskDetailController(ILogger<TaskDetailController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "AddTask")]
        public async Task<ActionResult<bool>> Create([FromBody] CreateTaskDetailCommand createTaskCommand)
        {
            _logger.LogDebug("Starting to Creat a Task");
            var response = await Mediator.Send(createTaskCommand);
            _logger.LogInformation("End Task");
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            _logger.LogInformation("Starting to Get all the tasks");
            var dtos = await Mediator.Send(new GetTaskListQuery());
            _logger.LogInformation("End tasks");
            return Ok(dtos);
        }

        [HttpPut(Name = "UpdateTask")]
        public async Task<ActionResult<bool>> Update([FromBody] UpdateTaskDetailCommand updateTaskCommand)
        {
            _logger.LogInformation("Updating task");
            var response = await Mediator.Send(updateTaskCommand);
            return Ok(response);
        }
        [HttpDelete(Name = "DeleteTask")]
        public async Task<ActionResult<bool>> Delete([FromBody] DeleteTaskDetailCommand deleteTaskCommand)
        {
            _logger.LogInformation("deleteing task");
            var response = await Mediator.Send(deleteTaskCommand);
            return Ok(response);
        }
        [HttpPost(Name = "CompleteTask")]
        public async Task<ActionResult<bool>> CompleteTask([FromBody] CompleteTaskDetailCommand Command)
        {
            _logger.LogInformation("Compleeting  task");
            var response = await Mediator.Send(Command);
            return Ok(response);
        }
        [HttpPost(Name = "AddActivity")]
        public async Task<ActionResult<bool>> CreateActivity([FromBody] AddActivityDetailCommand Command)
        {
            _logger.LogInformation("Crearing a  new activity");
            var response = await Mediator.Send(Command);
            return Ok(response);
        }
        [HttpPost("search")]
        public async Task<IActionResult> SearchTasks([FromBody] SearchTaskListQuery criteria)
        {
            var response = await Mediator.Send(criteria);
            return Ok(response);
        }
    }
}
