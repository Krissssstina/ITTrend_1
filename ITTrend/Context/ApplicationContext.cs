using ITTrend.Models;
using Microsoft.EntityFrameworkCore;

namespace ITTrend.Context
{
    public class ApplicationContext : DbContext
    {
        private const string _workDB =
        $"Server=.\\; Database=TaskBoard; Trusted_Connection=True; MultipleActiveResultSets=True; TrustServerCertificate=True";

        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
       : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sprint> Sprints { get; set; }
        public virtual DbSet<Models.Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_workDB);
            }
        }

    }
}
