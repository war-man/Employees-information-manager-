using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EmployeesInformationManager.Models
{
    ///<remarks>Defines the Employee database table
    public class Employee
    {
        public Employee()
        {
            this.EmployeesSkills = new HashSet<EmployeeSkill>();
        }
        public int Id { get; set; }
        
        [DisplayName("Full name")]
        [Required]
        public string FullName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public virtual ICollection<EmployeeSkill> EmployeesSkills { get; set; }
    }
}