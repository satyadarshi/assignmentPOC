using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskDetail = TaskManager.Domain.Entities.TaskDetail;

namespace TaskManager.Data.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskDetail>> GetTasksAsync();
        Task<TaskDetail> GetTaskByIdAsync(int id);
        Task<int>  AddTaskAsync(TaskDetail task);
        Task<TaskDetail> UpdateTaskAsync(TaskDetail task);
        Task<TaskDetail> DeleteTaskAsync(int id);
        Task<IEnumerable<TaskDetail>> SearchTasksAsync(string taskName, List<string>? tags, DateTime? startDate, DateTime? endDate, List<string>? statuses);
    }
}
