using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Project>()
            .HasMany(c => c.Employees)
            .WithMany(s => s.Projects)
            .UsingEntity<ProjectEmplyee>(
               j => j
                .HasOne(pt => pt.Employee)
                .WithMany(t => t.ProjectEmplyees)
                .HasForeignKey(pt => pt.EmployeeID),
               j => j
                .HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectEmplyees)
                .HasForeignKey(pt => pt.ProjectID),
               j =>
               {
                    j.Property(pt => pt.IsManager).HasDefaultValue(false);
                    j.HasKey(t => new { t.ProjectID, t.EmployeeID });
                    j.ToTable("ProjectEmplyee");
               }
            );

            
        }

        
        public DbSet<Project> Project { get; set; } = null!;
        public DbSet<Employee> Employee { get; set; } = null!;
        public DbSet<WebApplication7.Models.ProjectEmplyee> ProjectEmplyee { get; set; }
    }
}
