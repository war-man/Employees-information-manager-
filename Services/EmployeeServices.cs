using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeesInformationManager.Data;
using EmployeesInformationManager.Models;
using EmployeesInformationManager.Proxies;
using System.Collections.Generic;

namespace EmployeesInformationManager.Services
{
    public class EmployeeServices
    {
        private readonly EmployeesInformationManagerContext _context;

        public EmployeeServices(EmployeesInformationManagerContext context)
        {
            _context = context;
        }

        public async Task<Employee> Get(int? id)
        {
            return await _context.Employee.FindAsync(id);
        }
        public async Task<List<Employee>> GetAll()
        {
            return await _context.Employee.ToListAsync();
        }

        public async Task Delete(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }

        public List<string> GetSkillsNames(int? employeeId)
        {
            return  _context.EmployeeSkill
            .Where(es => es.EmployeeId == employeeId)
            .Include(es => es.Skill)
            .Select(es => es.Skill.Name).ToList();
        }

        private async Task DeleteSkills(int employeeId)
        {
            _context.EmployeeSkill.
                     RemoveRange(_context.EmployeeSkill
                     .Where(es => es.EmployeeId == employeeId));
            await _context.SaveChangesAsync();
        }

        private Employee ExtractData(EmployeeModelView employeeModelView)
        {
            Employee employee = employeeModelView.ToEmployee();
            string cleanedString = employeeModelView.EmployeeSkills ?? "";
            string[] inputSkills = cleanedString.Split(',');
            foreach(string inputSkill in inputSkills)
            {
                Skill skill = _context.Skill
                .Where(s => s.Name == inputSkill)
                .FirstOrDefault();
                if(skill == null)
                {
                    skill = new Skill {Name = inputSkill};
                    _context.Skill.Add(skill);
                }
                
                EmployeeSkill employeeSkill = _context.EmployeeSkill
                .Where(es => es.SkillId == skill.Id
                && es.EmployeeId == employee.Id)
                .FirstOrDefault();
                if(employeeSkill == null)
                {
                    employeeSkill = new EmployeeSkill();
                    employeeSkill.EmployeeId = employee.Id;
                    employeeSkill.SkillId = skill.Id;
                    _context.EmployeeSkill.Add(employeeSkill);
                    employee.EmployeesSkills.Add(employeeSkill);
                    skill.EmployeesSkills.Add(employeeSkill); 
                }
            }
            return employee;
        }

        public async Task Add(EmployeeModelView employeeModelView)
        {
            Employee employee = ExtractData(employeeModelView);
            _context.Add(employee);
            await _context.SaveChangesAsync();
        }

        
        public async Task Update(EmployeeModelView employeeModelView)
        {
            await DeleteSkills(employeeModelView.Id);
            Employee employee = ExtractData(employeeModelView);
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }


    }
}
