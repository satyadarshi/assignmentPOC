using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Features.Tasks.Queries
{
    public class GetActivityListViewModel
    {
        public int Id { get; set; }
        public DateTime? ActivityDate { get; set; }
        public string DoneBy { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
