using System.Threading.Tasks;
using EmployeesInformationManager.Data;
using EmployeesInformationManager.Models;
using EmployeesInformationManager.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeesInformationManager.Services
{
    public class DBServicesFacade
    {
        private readonly EmployeeServices employeeServices;
        private readonly SkillServices skillServices;
        private readonly EmployeeSkillServices employeeSkillServices;

        public DBServicesFacade(EmployeesInformationManagerContext context)
        {
            this.employeeServices = new EmployeeServices(context);
            this.skillServices = new SkillServices(context);
            this.employeeSkillServices = new EmployeeSkillServices(context);
        }
        
        private Employee ExtractData(EmployeeModelView employee)
        {
            string cleanedString = employee.EmployeeSkills ?? "";
            string[] inputSkills = cleanedString.Split(',');
            foreach(string inputSkill in inputSkills)
            {
                Skill skill = skillServices.GetByName(inputSkill);
                if(skill == null)
                {
                    skill = new Skill {Name = inputSkill};
                    skillServices.Add(skill);
                }
                EmployeeSkill employeeSkill = employeeSkillServices
                .GetById(new Tuple<int, int>(employee.Id,skill.Id));

                if(employeeSkill == null)
                {
                    employeeSkill = new EmployeeSkill();
                    employeeSkill.EmployeeId = employee.Id;
                    employeeSkill.SkillId = skill.Id;
                    employee.EmployeesSkills.Add(employeeSkill);
                    skill.EmployeesSkills.Add(employeeSkill);
                    
                    employeeSkillServices.Add(employeeSkill);
                }
            }
            return employee;
        }
        public async Task AddViewDataAsync(EmployeeModelView employeeModelView)
        {
            Employee employee = ExtractData(employeeModelView);
            employeeServices.Add(employee);
            await employeeServices.SaveAsync();
        }
        private async Task DeleteSkillsAsync(int employeeId)
        {
            employeeSkillServices.DeleteRange(es => es.EmployeeId == employeeId);
            await employeeSkillServices.SaveAsync();
        }

        public async Task UpdateFromViewAsync(EmployeeModelView employeeModelView)
        {
            await DeleteSkillsAsync(employeeModelView.Id);
            Employee employee = ExtractData(employeeModelView);
            employeeServices.Update(employee);
            await employeeServices.SaveAsync();
        }

        public Employee GetEmployee(int id)
        {
            return employeeServices.GetById(id);
        }
        public List<Employee> GetEmployeeList()
        {
            return employeeServices.GetList(es => true).ToList();
        }

        public EmployeeModelView GetModelView(int employeeId)
        {
            EmployeeModelView emv = employeeServices.GetAsModelView(employeeId);
            emv.SetEmployeeSkills(
                employeeSkillServices.GetSkillsNames(employeeId)
            );
            emv.SuggestedSkills = skillServices.GetAllAsArrayString();
            return emv;
        }

        public EmployeeModelView GetNewModelView()
        {
            EmployeeModelView emv = new EmployeeModelView();
            emv.SuggestedSkills = skillServices.GetAllAsArrayString();
            return emv;
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await employeeServices.DeleteAsync(id);
        }
    }
}
