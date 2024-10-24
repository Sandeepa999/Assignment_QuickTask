using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTask.Web.Data;
using QuickTask.Web.Models.Domain;
using QuickTask.Web.Models.ViewModels;

namespace QuickTask.Web.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> AddTask(AddTaskRequest addTaskRequest)
        {
            var job = new Job
            {
                Title = addTaskRequest.Title,
                Description = addTaskRequest.Description
            };
            await quickTaskDbContext.Jobs.AddAsync(job);
            await quickTaskDbContext.SaveChangesAsync();

            return RedirectToAction("TaskList");
        }

        [HttpGet]
        public async Task<IActionResult> TaskList()
        {
            var jobs = await quickTaskDbContext.Jobs.ToListAsync();
            return View(jobs);
        }

        [HttpGet]
        public async Task<IActionResult> TaskEdit(Guid id)
        {
            var job = await quickTaskDbContext.Jobs.FirstOrDefaultAsync(x => x.TaskId == id);
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
        public async Task<IActionResult> TaskEdit(EditTaskRequest editTaskRequest)
        {
            var job = new Job
            {
                TaskId = editTaskRequest.TaskId,
                Title = editTaskRequest.Title,
                Description = editTaskRequest.Description
            };
            var existingTask = await quickTaskDbContext.Jobs.FindAsync(job.TaskId);

            if (existingTask != null)
            {
                existingTask.Title = job.Title;
                existingTask.Description = job.Description;

                await quickTaskDbContext.SaveChangesAsync();
                return RedirectToAction("TaskList");
            }

            return View("Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskDelete(Guid id)
        {
            var job = await quickTaskDbContext.Jobs.FirstOrDefaultAsync(x => x.TaskId == id);

            if (job != null)
            {
                quickTaskDbContext.Jobs.Remove(job);
                await quickTaskDbContext.SaveChangesAsync();
                return RedirectToAction("TaskList");
            }

            return RedirectToAction("TaskList");
        }

    }
}
