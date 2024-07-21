using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class ActivityDetail
    {
        [Key]
        public int Id { get; set; }
        
        public int TaskDetailId { get; set; }
        [ForeignKey("TaskDetailId")]
        public virtual TaskDetail TaskDetail { get; set; }
        public DateTime ? ActivityDate { get; set; }
        public string DoneBy { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}
