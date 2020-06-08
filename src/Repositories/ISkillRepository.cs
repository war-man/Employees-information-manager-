using EmployeesInformationManager.Models;

namespace EmployeesInformationManager.Repositories
{
    public interface ISkillRepository:IRepository<Skill>
    {        
        Skill GetByName(string name);
    }
}