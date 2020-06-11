using System.Linq;
using EmployeesInformationManager.Repositories;
using EmployeesInformationManager.Data;
using EmployeesInformationManager.Models;
using System.Collections.Generic;

namespace EmployeesInformationManager.Services
{
    public class SkillServices : SkillRepository
    {
        public SkillServices(EmployeesInformationManagerContext context)
        :base(context)
        {
        }
        public string GetAllAsArrayString()
        {
            List<string> skills = this.GetList(s => true)
                                 .Select(s => s.Name).ToList();
            string skillsJoined = string.Join(",",skills);
            return "['"+skillsJoined.Replace(",", "','")+"']";
        }

        public Skill GetByName(string name)
        {
            return this.GetList(s => s.Name == name)
            .FirstOrDefault();
        }

    }
}
