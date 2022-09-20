using Microsoft.EntityFrameworkCore;
using ForestInteractiveTestApp.Models;

namespace ForestInteractiveTestApp.Core
{
    public class ContextCore : DbContext
    {
        public ContextCore(DbContextOptions<ContextCore> options) : base(options)
        { }

        public DbSet<Schedule> Schedule { get; set; }
    }
}
