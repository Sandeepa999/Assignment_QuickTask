using Microsoft.EntityFrameworkCore;
using QuickTask.Web.Models.Domain;

namespace QuickTask.Web.Data
{
    public class QuickTaskDbContext : DbContext
    {
        public QuickTaskDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Job> Jobs { get; set; }
    }
}
