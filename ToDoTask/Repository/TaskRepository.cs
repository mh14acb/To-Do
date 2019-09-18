using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ToDoTask.Models;

namespace ToDoTask.Repository
{
    public class TaskRepository
    {
        private TaskDBContext _db;
        public TaskRepository()
        {
            _db = new TaskDBContext();
        }
        public List<Task> GetAllTasks()
        {
            return _db.Tasks.ToList();
        }

        public void AddNewTask(Task task)
        {
            _db.Tasks.Add(task);
            _db.SaveChanges();
        }

        public void UpdateExistingTask(Task task)
        {
            _db.Entry(task).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void RemoveTask(int id)
        {
            Task task = _db.Tasks.Find(id);
            _db.Tasks.Remove(task);
            _db.SaveChanges();
        }
    }

}