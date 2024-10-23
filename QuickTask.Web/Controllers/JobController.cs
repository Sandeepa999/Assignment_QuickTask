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

            return View();
        }

        [HttpGet]
        public IActionResult TaskList() 
        {
            var jobs = quickTaskDbContext.Jobs.ToList();
            return View(jobs);
        }


    }
}
