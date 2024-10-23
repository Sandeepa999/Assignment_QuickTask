using System.ComponentModel.DataAnnotations;

namespace QuickTask.Web.Models.Domain
{
    public class Job
    {
        [Key]
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
