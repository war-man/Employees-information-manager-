using System.Linq;
using EmployeesInformationManager.Data;
using System.Collections.Generic;

namespace EmployeesInformationManager.Services
{
    public class SkillServices
    {
        private readonly EmployeesInformationManagerContext _context;

        public SkillServices(EmployeesInformationManagerContext context)
        {
            _context = context;
        }
        public string GetAllAsArrayString()
        {
            List<string> skills = _context.Skill.Select(s => s.Name).ToList();
            string skillsJoined = string.Join(",",skills);
            return "['"+skillsJoined.Replace(",", "','")+"']";
        }

    }
}
