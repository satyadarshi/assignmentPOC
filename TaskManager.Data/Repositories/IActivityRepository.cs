using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Data.Repositories
{
    public interface IActivityRepository
    {
        Task<IEnumerable<ActivityDetail>> GetActivityAsync();
        Task<int> AddActivityAsync(ActivityDetail task);
        Task<ActivityDetail> UpdateActivityAsync(ActivityDetail task);
        Task<ActivityDetail> DeleteActivityAsync(int id);
        Task<ActivityDetail> GetActivityByIdAsync(int id);
    }
}
