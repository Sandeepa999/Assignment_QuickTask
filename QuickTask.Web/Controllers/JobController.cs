using Microsoft.AspNetCore.Mvc;
using QuickTask.Web.Data;
using QuickTask.Web.Models.Domain;
using QuickTask.Web.Models.ViewModels;

namespace QuickTask.Web.Controllers
{
    public class JobController : Controller
    {
        private readonly QuickTaskDbContext quickTaskDbContext;

        public JobController(QuickTaskDbContext quickTaskDbContext)
        {
            this.quickTaskDbContext = quickTaskDbContext;
        }

        [HttpGet]
        public IActionResult AddTask()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTask(AddTaskRequest addTaskRequest)
        {
            var job = new Job
            {
                Title = addTaskRequest.Title,
                Description = addTaskRequest.Description
            };
            quickTaskDbContext.Jobs.Add(job);
            quickTaskDbContext.SaveChanges();

            return RedirectToAction("TaskList");
        }

        [HttpGet]
        public IActionResult TaskList()
        {
            var jobs = quickTaskDbContext.Jobs.ToList();
            return View(jobs);
        }

        [HttpGet]
        public IActionResult TaskEdit(Guid id)
        {
            var job = quickTaskDbContext.Jobs.FirstOrDefault(x => x.TaskId == id);
            if (job != null)
            {
                var editTaskRequest = new EditTaskRequest
                {
                    TaskId = job.TaskId,
                    Title = job.Title,
                    Description = job.Description
                };
                return View(editTaskRequest);
            }
            return View(null);
        }

        [HttpPost]
        public IActionResult TaskEdit(EditTaskRequest editTaskRequest)
        {
            var job = new Job
            {
                TaskId = editTaskRequest.TaskId,
                Title = editTaskRequest.Title,
                Description = editTaskRequest.Description
            };
            var existingTask = quickTaskDbContext.Jobs.Find(job.TaskId);

            if (existingTask != null)
            {
                existingTask.Title = job.Title;
                existingTask.Description = job.Description;

                quickTaskDbContext.SaveChanges();
                return RedirectToAction("TaskList");
            }

            return View("Edit");
        }

        [HttpPost]
        public IActionResult TaskDelete(Guid id)
        {
            var job = quickTaskDbContext.Jobs.FirstOrDefault(x => x.TaskId == id);

            if (job != null)
            {
                quickTaskDbContext.Jobs.Remove(job);
                quickTaskDbContext.SaveChanges();
                return RedirectToAction("TaskList");
            }
            return RedirectToAction("TaskList");

        }
    }
}
