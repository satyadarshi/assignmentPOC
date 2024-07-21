using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Tasks.Command
{
    public class UpdateTaskDetailDto
    {
        public int Id { get; set; }
        public string task_name { get; set; }
        public List<string> tags { get; set; }
        public DateTime due_date { get; set; }
        public string color { get; set; }
        public string assigned_to { get; set; }
        public string status { get; set; } = "PENDING";
    }
}
