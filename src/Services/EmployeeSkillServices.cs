using System.Linq;
using EmployeesInformationManager.Repositories;
using EmployeesInformationManager.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeesInformationManager.Services
{
    
    ///<remarks> Provides both low and high level queries
    /// to create, read, update or delete rows from EmployeeSkill database table</remarks>
    public class EmployeeSkillServices:EmployeeSkillRepository
    {
        public EmployeeSkillServices(EmployeesInformationManagerContext context)
        :base(context)
        {
        }

        ///<returns>Returns all skills names for a certain employee from database
        /// using it's id as List of string</returns>
        public List<string> GetSkillsNames(int employeeId)
        {
            return this.GetList(es => es.EmployeeId == employeeId)
            .Include(es => es.Skill)
            .Select(es => es.Skill)
            .Select(s => s.Name).ToList();
        }

        ///<summary>Deletes all skills for a certain employee from database
        /// using it's id asynchronously</summary>
        public async Task DeleteSkillsAsync(int employeeId)
        {
            this.DeleteRange(es => es.EmployeeId == employeeId);
            await this.SaveAsync();
        }
    }
}
