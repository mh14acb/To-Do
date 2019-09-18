using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoTask.ApplicationLogic;
using ToDoTask.Models;
using ToDoTask.Repository;

namespace ToDoTask.Controllers
{
    public class TasksController : Controller
    {
        private TaskRepository _taskRepository;
        private TaskLogic _taskLogic;

        public TasksController()
        {
            _taskRepository = new TaskRepository();
            _taskLogic = new TaskLogic();
        }
        private TaskDBContext db = new TaskDBContext();

        // GET: Tasks
        public ActionResult Index()
        {
            if (Session["Tasks"] == null)
            {
                Session["Tasks"] = _taskRepository.GetAllTasks();
            }
            return View(Session["Tasks"]); //Database calls are expensive: in order to make it more efficient we save the data os a session to save making multiple calls
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = _taskLogic.GetDetail(Session["Tasks"] as List<Task>, id.Value);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Completed,Note")] Task task)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.AddNewTask(task);
                //in order to not call the database again to get an updated task list
                var tasks = Session["Tasks"] as List<Task>; // gathering the existing task list from the session, that doesn't include the new task to be added
                tasks.Add(task); // using list function, ie the tasks.Add(), to add the new task in the tasks variable
                Session["Tasks"] = tasks; // updating the session to include the new task thats being added to the database in order to use it without calling the database to get an updated list
                return RedirectToAction("Index");
            }

            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = _taskLogic.GetDetail(Session["Tasks"] as List<Task>, id.Value);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Completed,Note")] Task task)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.UpdateExistingTask(task);
                var existingTasks = Session["Tasks"] as List<Task>;
                var indexForTaskToBeModified = existingTasks.FindIndex(x => x.ID == task.ID);
                existingTasks[indexForTaskToBeModified] = task;
                Session["Tasks"] = existingTasks;
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = _taskLogic.GetDetail(Session["Tasks"] as List<Task>, id.Value); ;
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _taskRepository.RemoveTask(id);
            var tasksFromSession = Session["Tasks"] as List<Task>;
            var indexOfTaskToBeRemoved = tasksFromSession.FindIndex(x => x.ID == id);
            tasksFromSession.RemoveAt(indexOfTaskToBeRemoved);
            Session["Tasks"] = tasksFromSession;
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
