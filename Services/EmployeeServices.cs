using System.Linq;
using System.Threading.Tasks;
using EmployeesInformationManager.Repositories;
using EmployeesInformationManager.Data;
using EmployeesInformationManager.Models;
using EmployeesInformationManager.Proxies;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EmployeesInformationManager.Services
{
    public class EmployeeServices
    {
        private readonly EmployeeRepository employeeRepo;
        private readonly SkillRepository skillRepo;

        public EmployeeServices(EmployeesInformationManagerContext context)
        {
            this.employeeRepo = new EmployeeRepository(context);
            this.skillRepo = new SkillRepository(context);
        }

        public Employee Get(int? id)
        {
            return employeeRepo.GetByID(id);
        }
        public List<Employee> GetAll()
        {
            return employeeRepo.GetAll();
        }

        public EmployeeModelView GetAsModelView(int? id)
        {
            
            var serializedParent = JsonConvert.SerializeObject(Get(id)); 
            return JsonConvert.DeserializeObject
            <EmployeeModelView>(serializedParent);
        }

        public async Task Delete(int id)
        {
            employeeRepo.Delete(id);
            await employeeRepo.SaveAsync();
        }

        public bool Exists(int id)
        {
            return this.Get(id) != null;
        }

        public List<string> GetSkillsNames(int? employeeId)
        {
            return employeeRepo.GetSkills(employeeId).Select(s => s.Name).ToList();
        }

        private async Task DeleteSkillsAsync(int employeeId)
        {
            employeeRepo.DeleteSkills(employeeId);
            await employeeRepo.SaveAsync();
        }

        private Employee ExtractData(EmployeeModelView employee)
        {
            string cleanedString = employee.EmployeeSkills ?? "";
            string[] inputSkills = cleanedString.Split(',');
            foreach(string inputSkill in inputSkills)
            {
                Skill skill = skillRepo.GetByName(inputSkill);
                if(skill == null)
                {
                    skill = new Skill {Name = inputSkill};
                    skillRepo.Add(skill);
                }
                employeeRepo.AddEmployeeSkill(employee,skill);
            }
            return employee;
        }

        public async Task AddFromViewAsync(EmployeeModelView employeeModelView)
        {
            Employee employee = ExtractData(employeeModelView);
            employeeRepo.Add(employee);
            await employeeRepo.SaveAsync();
        }

        
        public async Task UpdateFromViewAsync(EmployeeModelView employeeModelView)
        {
            await DeleteSkillsAsync(employeeModelView.Id);
            Employee employee = ExtractData(employeeModelView);
            employeeRepo.Update(employee);
            await employeeRepo.SaveAsync();
        }


    }
}
