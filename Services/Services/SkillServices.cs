using System.Linq;
using EmployeesInformationManager.Repositories;
using EmployeesInformationManager.Data;
using EmployeesInformationManager.Models;
using System.Collections.Generic;

namespace EmployeesInformationManager.Services
{
    
    ///<remarks> Provides both low and high level queries
    /// to create, read, update or delete rows from Skill database table</remarks>
    public class SkillServices : SkillRepository
    {
        public SkillServices(EmployeesInformationManagerContext context)
        :base(context)
        {
        }

        ///<summary>Get all skills names from database to be used for auto-complete</summary>
        ///<returns>Returns string of javascript string array</returns>
        public string GetAllAsArrayString()
        {
            List<string> skills = this.GetList(s => true)
                                 .Select(s => s.Name).ToList();
            string skillsJoined = string.Join(",",skills);
            return "['"+skillsJoined.Replace(",", "','")+"']";
        }

        ///<returns>Returns Skill using it's name from database</returns>
        public Skill GetByName(string name)
        {
            return this.GetList(s => s.Name == name)
            .FirstOrDefault();
        }

    }
}
