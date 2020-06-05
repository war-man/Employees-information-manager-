using System;
using System.ComponentModel.DataAnnotations;
using EmployeesInformationManager.Models;
using System.Collections.Generic;

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

        public void SetEmployee(Employee employee,List<string> skillsNames)
        {
            this.Id = employee.Id;
            this.FullName = employee.FullName;
            this.Email = employee.Email;
            this.EmployeeSkills = string.Join(",", skillsNames);
        }
    }
}