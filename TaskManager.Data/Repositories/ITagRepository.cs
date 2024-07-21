using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Data.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetActivityAsync();
        Task<int> AddTagAsync(Tag task);
        Task<Tag> UpdateTagAsync(Tag task);
        Task<Tag> DeleteTagAsync(int id);
    }
}
