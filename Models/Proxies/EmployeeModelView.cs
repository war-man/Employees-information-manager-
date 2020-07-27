using EmployeesInformationManager.Models;
using System.Collections.Generic;

namespace EmployeesInformationManager.Proxies
{
    ///<remarks> Used to define the proxy model that appears in the view
    public class EmployeeModelView : Employee
    {
        public string EmployeeSkills { get; set; }
        public string SuggestedSkills { get; set; }

        public List<Employee> Employees { get; set; }

        ///<summary>Converts the skills names list into string
        /// and use it to set the value of Employee Skills 
        /// to show them in the form at the view</summary>
        public void SetEmployeeSkills(List<string> skillsNames)
        {
            this.EmployeeSkills = string.Join(",", skillsNames);
        }
    }
}