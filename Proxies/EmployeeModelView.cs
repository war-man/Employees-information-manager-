using EmployeesInformationManager.Models;
using System.Collections.Generic;

namespace EmployeesInformationManager.Proxies
{
    public class EmployeeModelView : Employee
    {
        public string EmployeeSkills { get; set; }
        public string SuggestedSkills { get; set; }
        public void SetEmployeeSkills(List<string> skillsNames)
        {
            this.EmployeeSkills = string.Join(",", skillsNames);
        }
    }
}