using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ToDoTask.Models
{
    public class Task
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public string Note { get; set; }
    }

    public class TaskDBContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
    }


}