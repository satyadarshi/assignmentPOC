using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using ActivityDetail = TaskManager.Domain.Entities.ActivityDetail;
using TaskDetail = TaskManager.Domain.Entities.TaskDetail;

namespace TaskManager.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TaskDetail> Tasks { get; set; }
        public DbSet<ActivityDetail> Activities { get; set; }
        public DbSet<Tag> Tags { get; set; }
        
    }
}
