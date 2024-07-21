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
    public class DeleteTaskDetailCommandHandler : IRequestHandler<DeleteTaskDetailCommand, bool>
    {
        private readonly ITaskRepository taskRepository;
        private readonly IActivityRepository activityRepository;
        private readonly ITagRepository tagRepository;

        public DeleteTaskDetailCommandHandler(
            ITaskRepository _taskRepository, IActivityRepository _activityRepository, ITagRepository _tagRepository)
        {
            taskRepository = _taskRepository;
            activityRepository = _activityRepository;
            tagRepository = _tagRepository;
        }

        public async Task<bool> Handle(DeleteTaskDetailCommand request,
            CancellationToken cancellationToken)
        {
            TaskDetail task = await taskRepository.GetTaskByIdAsync(request.TaskDetailId);
            if (task != null)
            {
                task.IsDeleted = true;
                await taskRepository.UpdateTaskAsync(task);
            }

            return true;
        }
    }
}
