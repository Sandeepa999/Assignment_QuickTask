namespace QuickTask.Web.Models.ViewModels
{
    public class EditTaskRequest
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
