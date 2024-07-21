using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Features.Tasks.Queries
{
    public class GetTaskListViewModel
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public List<GetTagListViewModel> Tags { get; set; }
        public DateTime DueDate { get; set; }
        public string Color { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; } = "PENDING";

        public List<GetActivityListViewModel> Activities { get; set; }
    }
}
