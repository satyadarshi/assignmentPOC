using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TaskDetailId { get; set; }
        [ForeignKey("TaskDetailId")]
        public virtual TaskDetail TaskDetail { get; set; }
        public bool IsDeleted { get; set; }
    }
}
