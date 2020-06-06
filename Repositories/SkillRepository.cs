using EmployeesInformationManager.Models;
using EmployeesInformationManager.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmployeesInformationManager.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private EmployeesInformationManagerContext context;

        public SkillRepository(EmployeesInformationManagerContext context)
        {
            this.context = context;
        }

        public List<Skill> GetAll()
        {
            return context.Skill.ToList();
        }

        public Skill GetByID(int? skillId)
        {
            return context.Skill.Find(skillId);
        }

        public Skill GetByName(string name)
        {
            return context.Skill
                .Where(s => s.Name == name)
                .FirstOrDefault();
        }

        public void Add(Skill skill)
        {
            context.Skill.Add(skill);
        }
        public void Delete(int? skillId)
        {
            Skill skill = context.Skill.Find(skillId);
            context.Skill.Remove(skill);
        }
        public void Update(Skill skill)
        {
            context.Skill.Update(skill);
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}