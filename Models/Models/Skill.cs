using System.Collections.Generic;

namespace EmployeesInformationManager.Models
{
    ///<remarks>Defines the Skill database table
    public class Skill
    {
        public Skill()
        {
            this.EmployeesSkills = new HashSet<EmployeeSkill>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EmployeeSkill> EmployeesSkills { get; set; }
    }
}