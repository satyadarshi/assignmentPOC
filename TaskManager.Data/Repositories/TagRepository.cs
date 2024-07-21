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
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddTagAsync(Tag task)
        {
            _context.Tags.Add(task);
            await _context.SaveChangesAsync();
            return task.Id;
        }

        public async Task<Tag> DeleteTagAsync(int id)
        {
            var task = await _context.Tags.FindAsync(id);
            if (task != null)
            {
                _context.Tags.Remove(task);
                await _context.SaveChangesAsync();
            }
            return task;
        }

        public async Task<IEnumerable<Tag>> GetActivityAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> UpdateTagAsync(Tag task)
        {
            _context.Tags.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
