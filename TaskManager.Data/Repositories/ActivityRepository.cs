using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Context;
using TaskManager.Domain.Entities;

namespace TaskManager.Data.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddActivityAsync(ActivityDetail task)
        {
            _context.Activities.Add(task);
             await _context.SaveChangesAsync();
             return task.Id;
        }

        public async Task<ActivityDetail> DeleteActivityAsync(int id)
        {
            var task = await _context.Activities.FindAsync(id);
            if (task != null)
            {
                _context.Activities.Remove(task);
                await _context.SaveChangesAsync();
            }
            return task;
        }

        public async Task<IEnumerable<ActivityDetail>> GetActivityAsync()
        {
            return await _context.Activities.ToListAsync();
        }

        public async Task<ActivityDetail> UpdateActivityAsync(ActivityDetail task)
        {
            _context.Activities.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<ActivityDetail> GetActivityByIdAsync(int id)
        {
            return await _context.Activities.FindAsync(id);
        }

    }
}
