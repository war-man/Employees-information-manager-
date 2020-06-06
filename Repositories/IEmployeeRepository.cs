using EmployeesInformationManager.Models;
using System.Collections.Generic;

namespace EmployeesInformationManager.Repositories
{
    public interface IEmployeeRepository:IRepository<Employee>
    {        
        List<Skill> GetSkills(int? id);
        void DeleteSkills(int? id);

        void AddEmployeeSkill(Employee employee,Skill skill);
    }
}