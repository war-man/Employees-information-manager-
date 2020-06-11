using System.Linq;
using EmployeesInformationManager.Repositories;
using EmployeesInformationManager.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeesInformationManager.Services
{
    public class EmployeeSkillServices:EmployeeSkillRepository
    {
        public EmployeeSkillServices(EmployeesInformationManagerContext context)
        :base(context)
        {
        }
        public List<string> GetSkillsNames(int employeeId)
        {
            return this.GetList(es => es.EmployeeId == employeeId)
            .Include(es => es.Skill)
            .Select(es => es.Skill)
            .Select(s => s.Name).ToList();
        }

        private async Task DeleteSkillsAsync(int employeeId)
        {
            this.DeleteRange(es => es.EmployeeId == employeeId);
            await this.SaveAsync();
        }
    }
}
