using EmployeesInformationManager.Models;
using EmployeesInformationManager.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace EmployeesInformationManager.Repositories
{
    public class EmployeeSkillRepository : IRepository<EmployeeSkill,Tuple<int,int>>
    {
        private EmployeesInformationManagerContext context;

        public EmployeeSkillRepository(EmployeesInformationManagerContext context)
        {
            this.context = context;
        }

        public IQueryable<EmployeeSkill> GetList(Expression<Func<EmployeeSkill,Boolean>>  filter){
            return context.EmployeeSkill.
                   Where(filter);
        }

        public EmployeeSkill GetById(Tuple<int,int> Id)
        {
            return context.EmployeeSkill
            .Where(es => es.EmployeeId == Id.Item1 
            && es.SkillId == Id.Item2).FirstOrDefault();
        }

        public void Add(EmployeeSkill employeeSkill)
        {
            context.EmployeeSkill.Add(employeeSkill); 
        }
        public void Delete(Tuple<int,int> Id)
        {
            context.EmployeeSkill.
            RemoveRange(context.EmployeeSkill
                .Where(es => es.EmployeeId == Id.Item1 
                && es.SkillId == Id.Item2)
            );
        }

        public void DeleteRange(Expression<Func<EmployeeSkill,Boolean>> filter){
            context.EmployeeSkill.
            RemoveRange(context.EmployeeSkill
                .Where(filter)
            );
        }
        public void Update(EmployeeSkill employeeSkill)
        {
            context.EmployeeSkill.Update(employeeSkill);
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
        
    }
}