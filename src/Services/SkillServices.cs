using System.Linq;
using EmployeesInformationManager.Repositories;
using EmployeesInformationManager.Data;
using System.Collections.Generic;

namespace EmployeesInformationManager.Services
{
    public class SkillServices
    {
        private readonly SkillRepository skillRepo;

        public SkillServices(EmployeesInformationManagerContext context)
        {
            this.skillRepo = new SkillRepository(context);
        }
        public string GetAllAsArrayString()
        {
            List<string> skills = skillRepo.GetAll()
                                 .Select(s => s.Name).ToList();
            string skillsJoined = string.Join(",",skills);
            return "['"+skillsJoined.Replace(",", "','")+"']";
        }

    }
}
