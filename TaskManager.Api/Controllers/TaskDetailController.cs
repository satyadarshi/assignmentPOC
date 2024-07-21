using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Tasks.Command;
using TaskManager.Application.Features.Tasks.Queries;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/taskmanager/taskdetail/[action]")]
    public class TaskDetailController : BaseController
    {

       
        [HttpPost(Name = "AddTask")]
        public async Task<ActionResult<bool>> Create([FromBody] CreateTaskDetailCommand createTaskCommand)
        {
            var response = await Mediator.Send(createTaskCommand);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            var dtos = await Mediator.Send(new GetTaskListQuery());
            return Ok(dtos);
        }

        [HttpPut(Name = "UpdateTask")]
        public async Task<ActionResult<bool>> Update([FromBody] UpdateTaskDetailCommand updateTaskCommand)
        {
            var response = await Mediator.Send(updateTaskCommand);
            return Ok(response);
        }
        [HttpDelete(Name = "DeleteTask")]
        public async Task<ActionResult<bool>> Delete([FromBody] DeleteTaskDetailCommand deleteTaskCommand)
        {
            var response = await Mediator.Send(deleteTaskCommand);
            return Ok(response);
        }
        [HttpPost(Name = "CompleteTask")]
        public async Task<ActionResult<bool>> CompleteTask([FromBody] CompleteTaskDetailCommand Command)
        {
            var response = await Mediator.Send(Command);
            return Ok(response);
        }
        [HttpPost(Name = "AddActivity")]
        public async Task<ActionResult<bool>> CreateActivity([FromBody] AddActivityDetailCommand Command)
        {
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
