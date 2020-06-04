using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EmployeesInformationManager.Models;
using EmployeesInformationManager.Data;
using System.Linq;

namespace EmployeesInformationManager.Proxies
{
    public class EmployeeModelView
    {
        public int Id { get; set; }
        
        [Required]
        public string FullName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        public string EmployeeSkills { get; set; }
        public string SuggestedSkills { get; set; }

        public Employee ToEmployee()
        {
            Employee employee = new Employee();
            employee.Id = this.Id;
            employee.FullName = this.FullName;
            employee.Email = this.Email;
            return employee;
        }

        public void SetEmployee(Employee employee)
        {
            this.Id = employee.Id;
            this.FullName = employee.FullName;
            this.Email = employee.Email;
        }

        public void setSkillsAsync(EmployeesInformationManagerContext context)
        {
            this.EmployeeSkills = string.Join(",", new List<string>(){"jQuery", "Script",".NET"});
            List<string> skills = context.Skill.Select(s => s.Name).ToList();
            var temp = string.Join(",",skills);
            this.SuggestedSkills = "['"+temp.Replace(",", "','")+"']";
        }
    }
}