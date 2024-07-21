using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Features.Tasks.Queries
{
    public class SearchTaskListQuery: IRequest<List<GetTaskListViewModel>>
    {
        public string? taskName { get; set; }
        public DateTime startDueDate { get; set; } = DateTime.Now.AddDays(-90);
        public DateTime endDueDate { get; set; } = DateTime.Now;
        public List<string> ? status { get; set; }
    }
}
