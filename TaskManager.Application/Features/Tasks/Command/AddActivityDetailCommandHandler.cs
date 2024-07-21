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
    public class AddActivityDetailCommandHandler : IRequestHandler<AddActivityDetailCommand, bool>
    {
        private readonly ITaskRepository taskRepository;
        private readonly IActivityRepository activityRepository;

        public AddActivityDetailCommandHandler(
            ITaskRepository _taskRepository, IActivityRepository _activityRepository)
        {
            taskRepository = _taskRepository;
            activityRepository = _activityRepository;
        }

        public async Task<bool> Handle(AddActivityDetailCommand request,
            CancellationToken cancellationToken)
        {
            bool success = false;
            foreach (var item in request.ActivityTasks)
            {
                TaskDetail task = await taskRepository.GetTaskByIdAsync(item.TaskDetailId);
                if (task != null)
                {
                    ActivityDetail activityDetail = new ActivityDetail();
                    // Need clarification on for activity creation
                    //ActivityDetail activityDetail = await activityRepository.GetActivityByIdAsync(request.CompleteTask.Id);
                    //if(activityDetail != null)
                    //{
                    activityDetail.TaskDetailId = item.TaskDetailId;
                    activityDetail.Description = item.Description;
                    activityDetail.ActivityDate = item.ActivityDate;
                    activityDetail.DoneBy = item.DoneBy;
                    activityDetail.IsDeleted = false;
                    activityRepository.AddActivityAsync(activityDetail);
                    // }
                    success = true;
                }
            }
            

            return success;
        }
    }
}
