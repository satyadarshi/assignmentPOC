using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Context;
using TaskManager.Domain.Entities;
using TaskDetail = TaskManager.Domain.Entities.TaskDetail;

namespace TaskManager.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskDetail>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskDetail> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<int> AddTaskAsync(TaskDetail task)
        {
            _context.Tasks.Add(task);
             await _context.SaveChangesAsync();
            return task.Id;
        }

        public async Task<TaskDetail> UpdateTaskAsync(TaskDetail task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskDetail> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return task;
        }

        public async Task<IEnumerable<TaskDetail>> SearchTasksAsync(string taskName, List<string>? tags, DateTime? startDate, DateTime? endDate, List<string>? status)
        {
            var query = _context.Tasks.AsQueryable();

           
            if (!string.IsNullOrEmpty(taskName))
            {
                query = query.Where(t => EF.Functions.Like(t.TaskName, taskName));
            }

            //if (tags != null && tags.Any())
            //{
            //     query = query.Where(t => status.Contains(t.Status));
            //}

            if (startDate.HasValue)
            {
                query = query.Where(t => t.DueDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.DueDate <= endDate.Value);
            }

            if (status != null && status.Count > 0)
            {
                query = query.Where(t => status.Contains(t.Status));
               
            }

            return await query.ToListAsync();
        }

       
    }
}
