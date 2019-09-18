using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoTask.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index(string taskName)
        {
            ViewBag.Task = "Task is " + taskName;
            return View();
        }
    }
}