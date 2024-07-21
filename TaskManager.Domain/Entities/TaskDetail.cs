 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class TaskDetail
    {
        [Key]
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime DueDate { get; set; }
        public string Color { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; } = "PENDING";
        public bool IsDeleted { get; set; }
        public bool IsMailSent { get; set; }
        public virtual ICollection<Tag> TagList { get; set; }
    }
}
