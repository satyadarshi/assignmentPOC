using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Repositories;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Tasks.Command
{
    public class UpdateTaskDetailCommandHandler : IRequestHandler<UpdateTaskDetailCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskDetailCommandHandler(
            ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> Handle(UpdateTaskDetailCommand request,
            CancellationToken cancellationToken)
        {
            bool success = false;
            TaskDetail task= await _taskRepository.GetTaskByIdAsync(request.UpdateDetails.Id);
            if(task != null)
            {
                task.Id = request.UpdateDetails.Id;
                task.TaskName = request.UpdateDetails.task_name;
                task.DueDate = request.UpdateDetails.due_date;
                task.Color = request.UpdateDetails.color;
                task.AssignedTo = request.UpdateDetails.assigned_to;
                task.Status = request.UpdateDetails.status;
                await _taskRepository.UpdateTaskAsync(task);
                success = true;
            }
            
            return success;
        }
    }
}