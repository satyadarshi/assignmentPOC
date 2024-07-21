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
    public class CreateTaskDetailCommandHandler : IRequestHandler<CreateTaskDetailCommand, bool>
    {
        private readonly ITaskRepository taskRepository;
        private readonly IActivityRepository activityRepository;
        private readonly ITagRepository tagRepository;

        public CreateTaskDetailCommandHandler(
            ITaskRepository _taskRepository, IActivityRepository _activityRepository, ITagRepository _tagRepository)
        {
            taskRepository = _taskRepository;
            activityRepository = _activityRepository;
            tagRepository = _tagRepository;
        }

        public async Task<bool> Handle(CreateTaskDetailCommand request,
            CancellationToken cancellationToken)
        {
            var createTaskDetailCommandResponse = new CreateTaskDetailCommandResponse();
            TaskDetail task;
            foreach (var obj in request.Details)
            {
                task = new TaskDetail();
                task.TaskName = obj.task_name;
                task.DueDate = obj.due_date;
                task.Color = obj.color;
                task.AssignedTo = obj.assigned_to;
                task.Status = obj.status;
                task.IsDeleted = false;
                task.IsMailSent = false;
                int result = await taskRepository.AddTaskAsync(task);
                if(result > 0)
                {
                    // Need clarification on for activity creation part. Commented the code for now.
                    //await InsertActivityDetails(result);
                     await InsertTags(obj.tags, result);
                }
                createTaskDetailCommandResponse.Success = true;
            }
            
            return true;
        }
        private async Task<int> InsertActivityDetails(int taskId)
        {
            bool result = false;
            ActivityDetail activityDetail = new ActivityDetail();
            activityDetail.TaskDetailId = taskId;
            return await activityRepository.AddActivityAsync(activityDetail);

        }
        private async Task<int> InsertTags(List<string> tags, int taskId)
        {
            int sucesss = 0;
            Tag tag;
            foreach (var _tag in tags)
            {
                tag = new Tag();
                tag.Name = _tag;
                tag.TaskDetailId = taskId;
                tag.IsDeleted = false;
                sucesss= await tagRepository.AddTagAsync(tag);
            }
            return sucesss;
        }
    }
}
