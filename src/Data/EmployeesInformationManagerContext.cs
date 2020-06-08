using Microsoft.EntityFrameworkCore;
using EmployeesInformationManager.Models;

namespace EmployeesInformationManager.Data
{
    public class EmployeesInformationManagerContext : DbContext
    {
        public EmployeesInformationManagerContext (DbContextOptions<EmployeesInformationManagerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeSkill>().HasKey(es => new { es.EmployeeId, es.SkillId });
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkill { get; set; }
    }
}