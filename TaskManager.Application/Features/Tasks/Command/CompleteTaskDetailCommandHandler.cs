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
    public class CompleteTaskDetailCommandHandler : IRequestHandler<CompleteTaskDetailCommand, bool>
    {
        private readonly ITaskRepository taskRepository;
        private readonly IActivityRepository activityRepository;

        public CompleteTaskDetailCommandHandler(
            ITaskRepository _taskRepository, IActivityRepository _activityRepository)
        {
            taskRepository = _taskRepository;
            activityRepository = _activityRepository;
        }

        public async Task<bool> Handle(CompleteTaskDetailCommand request,
            CancellationToken cancellationToken)
        {
            bool success = false;
            TaskDetail task = await taskRepository.GetTaskByIdAsync(request.TaskDetailId);
            if (task != null)
            {
                task.Status = "Completed";
                await taskRepository.UpdateTaskAsync(task);
                success = true;
            }

            return success;
        }
    }
}
