using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EmployeesInformationManager.Models;
using System;
using System.Linq;

namespace EmployeesInformationManager.Data
{
    ///<remarks>Inserts the following skills in the database: PHP, ASP.NET, iOS, Android
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EmployeesInformationManagerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<EmployeesInformationManagerContext>>()))
            {
                // Look for any movies.
                if (context.Skill.Any())
                {
                    return;   // DB has been seeded
                }
                context.Skill.AddRange(
                    new Skill {Name = "PHP"},
                    new Skill {Name = "ASP.NET"},
                    new Skill {Name = "iOS"},
                    new Skill {Name = "Android"}
                );
                context.SaveChanges();
            }
        }
    }
}