using EmployeesInformationManager.Models;
using EmployeesInformationManager.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace EmployeesInformationManager.Repositories
{
    public class SkillRepository : IRepository<Skill,int>
    {
        private EmployeesInformationManagerContext context;

        public SkillRepository(EmployeesInformationManagerContext context)
        {
            this.context = context;
        }

        public Skill GetById(int skillId)
        {
            return context.Skill.Find(skillId);
        }

        public IQueryable<Skill> GetList(Expression<Func<Skill,Boolean>> filter)
        {
            return context.Skill
            .Where(filter);
        }

        public void Add(Skill skill)
        {
            context.Skill.Add(skill);
        }
        public void Delete(int skillId)
        {
            Skill skill = context.Skill.Find(skillId);
            context.Skill.Remove(skill);
        }
        
        public void DeleteRange(Expression<Func<Skill,Boolean>> filter){
            context.Skill.
            RemoveRange(context.Skill
                .Where(filter)
            );
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