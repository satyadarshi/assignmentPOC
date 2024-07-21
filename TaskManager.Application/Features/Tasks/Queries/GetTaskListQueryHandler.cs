using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Repositories;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Tasks.Queries
{
    public class GetTaskListQueryHandler : IRequestHandler<GetTaskListQuery, List<GetTaskListViewModel>>
    {
        private readonly ITaskRepository taskRepository;
        private readonly IActivityRepository activityRepository;
        private readonly ITagRepository tagRepository;

        public GetTaskListQueryHandler(
            ITaskRepository _taskRepository, IActivityRepository _activityRepository, ITagRepository _tagRepository)
        {
            taskRepository = _taskRepository;
            activityRepository = _activityRepository;
            tagRepository = _tagRepository;
        }

        public async Task<List<GetTaskListViewModel>> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
        {
            List<GetTaskListViewModel> lstTasks=new List<GetTaskListViewModel> ();
            GetTaskListViewModel vm;
            GetTagListViewModel vmTag;
            List <GetTagListViewModel> lstvmTag;
            GetActivityListViewModel vmActivity;
            List<GetActivityListViewModel> lstvmActivity;

            var allTasks = await taskRepository.GetTasksAsync();
            foreach (var _task in allTasks.Where(x=>x.IsDeleted==false))
            {
                lstvmTag=new List<GetTagListViewModel> ();
                lstvmActivity=new List<GetActivityListViewModel> ();
                vm = new GetTaskListViewModel();
                vm.Id= _task.Id;
                vm.TaskName = _task.TaskName;
                vm.DueDate = _task.DueDate;
                vm.Color = _task.Color;
                vm.Status = _task.Status;
                vm.AssignedTo=_task.AssignedTo;
                //Added Tags to the list
                var lstTags = await tagRepository.GetActivityAsync();
                foreach (var item in lstTags.Where(x=>x.TaskDetailId== _task.Id && x.IsDeleted == false))
                {
                    vmTag=new GetTagListViewModel();
                    vmTag.Id= item.Id;
                    vmTag.Name= item.Name;
                    lstvmTag.Add(vmTag);
                }
                vm.Tags = lstvmTag;
                //Added activites to the list
                var lstActivities = await activityRepository.GetActivityAsync();
                foreach (var item in lstActivities.Where(x => x.TaskDetailId == _task.Id && x.IsDeleted==false))
                {
                    vmActivity = new GetActivityListViewModel();
                    vmActivity.Id = item.Id;
                    vmActivity.ActivityDate = item.ActivityDate;
                    vmActivity.Description = item.Description;
                    vmActivity.DoneBy = item.DoneBy;
                    lstvmActivity.Add(vmActivity);
                }
                vm.Tags = lstvmTag;
                vm.Activities = lstvmActivity;
                lstTasks.Add(vm);
            }
            return lstTasks;
        }
    }
}
