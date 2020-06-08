using EmployeesInformationManager.Models;
using EmployeesInformationManager.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmployeesInformationManager.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeesInformationManagerContext context;

        public EmployeeRepository(EmployeesInformationManagerContext context)
        {
            this.context = context;
        }

        public List<Employee> GetAll()
        {
            return  context.Employee.ToList();
        }

        public Employee GetByID(int? employeeId)
        {
            return context.Employee.Find(employeeId);
        }

        public List<Skill> GetSkills(int? employeeId){
            return  context.EmployeeSkill
            .Where(es => es.EmployeeId == employeeId)
            .Include(es => es.Skill)
            .Select(es => es.Skill).ToList();
        }

        
        public void AddEmployeeSkill(Employee employee,Skill skill){
            EmployeeSkill employeeSkill = context.EmployeeSkill
                .Where(es => es.SkillId == skill.Id
                && es.EmployeeId == employee.Id)
                .FirstOrDefault();
            if(employeeSkill == null)
            {
                employeeSkill = new EmployeeSkill();
                employeeSkill.EmployeeId = employee.Id;
                employeeSkill.SkillId = skill.Id;
                context.EmployeeSkill.Add(employeeSkill);
                employee.EmployeesSkills.Add(employeeSkill);
                skill.EmployeesSkills.Add(employeeSkill); 
            }
        }

        public void Add(Employee employee)
        {
             context.Employee.Add(employee);
        }
        public  void Delete(int? employeeId)
        {
            Employee employee =  context.Employee.Find(employeeId);
            context.Employee.Remove(employee);
        }

        public void DeleteSkills(int? employeeId)
        {
            context.EmployeeSkill.
            RemoveRange(context.EmployeeSkill
                .Where(es => es.EmployeeId == employeeId)
            );
        }
        public void Update(Employee employee)
        {
            context.Employee.Update(employee);
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
        
    }
}