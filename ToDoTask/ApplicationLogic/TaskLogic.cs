using System.Collections.Generic;
using ToDoTask.Models;

namespace ToDoTask.ApplicationLogic
{
    public class TaskLogic
    {

        public Task GetDetail(List<Task> tasks, int id)
        {
            return tasks.Find(task => task.ID == id);
        }

    }
}