using System.Threading.Tasks;
using EmployeesInformationManager.Data;
using EmployeesInformationManager.Models;
using EmployeesInformationManager.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeesInformationManager.Services
{
    ///<remarks>Provides a simple interface to a database services which contains lots of moving parts.
    /// it includes only those features that clients really care about.</remarks>
    public class DBServicesFacade
    {
        private readonly EmployeeServices employeeServices;
        private readonly SkillServices skillServices;
        private readonly EmployeeSkillServices employeeSkillServices;

        public DBServicesFacade(EmployeesInformationManagerContext context)
        {
            this.employeeServices = new EmployeeServices(context);
            this.skillServices = new SkillServices(context);
            this.employeeSkillServices = new EmployeeSkillServices(context);
        }
        
        ///<summary>Extracts data from the form,
        /// saves new inserted skills to skills table,
        /// and inserts employee skills to database</summary>
        ///<returns>Returns Employee after modifications
        /// to be added to or updated in the database</returns>
        private Employee ExtractData(EmployeeModelView employee)
        {
            string cleanedString = employee.EmployeeSkills ?? "";
            string[] inputSkills = cleanedString.Split(',');
            foreach(string inputSkill in inputSkills)
            {
                Skill skill = skillServices.GetByName(inputSkill);
                if(skill == null)
                {
                    skill = new Skill {Name = inputSkill};
                    skillServices.Add(skill);
                }
                EmployeeSkill employeeSkill = employeeSkillServices
                .GetById(new Tuple<int, int>(employee.Id,skill.Id));

                if(employeeSkill == null)
                {
                    employeeSkill = new EmployeeSkill();
                    employeeSkill.EmployeeId = employee.Id;
                    employeeSkill.SkillId = skill.Id;
                    employee.EmployeesSkills.Add(employeeSkill);
                    skill.EmployeesSkills.Add(employeeSkill);
                    
                    employeeSkillServices.Add(employeeSkill);
                }
            }
            return employee;
        }

        ///<summary>Adds new employee to database,
        /// saves new inserted skills to skills table,
        /// and inserts employee skills to database asynchronously</summary>
        public async Task AddViewDataAsync(EmployeeModelView employeeModelView)
        {
            Employee employee = ExtractData(employeeModelView);
            employeeServices.Add(employee);
            await employeeServices.SaveAsync();
        }
        
        ///<summary>Updates employee's data,
        /// saves new inserted skills to skills table asynchronously</summary>
        public async Task UpdateFromViewAsync(EmployeeModelView employeeModelView)
        {
            await employeeSkillServices.DeleteSkillsAsync(employeeModelView.Id);
            Employee employee = ExtractData(employeeModelView);
            employeeServices.Update(employee);
            await employeeServices.SaveAsync();
        }

        ///<returns>Returns employee from database using it's id</returns>
        public Employee GetEmployee(int id)
        {
            return employeeServices.GetById(id);
        }
        
        ///<returns>Returns all employees in the database as List</returns>
        public List<Employee> GetEmployeeList()
        {
            return employeeServices.GetList(es => true).ToList();
        }

        
        ///<returns>Returns EmployeeModelView after filling all view required data</returns>
        public EmployeeModelView GetModelView(int employeeId)
        {
            EmployeeModelView emv = employeeServices.GetAsModelView(employeeId);
            emv.SetEmployeeSkills(
                employeeSkillServices.GetSkillsNames(employeeId)
            );
            emv.Employees = employeeServices.GetList(e=>true).ToList();
            emv.SuggestedSkills = skillServices.GetAllAsArrayString();
            return emv;
        }

        ///<returns>Returns new created EmployeeModelView after 
        ///filling all view required data</returns>
        public EmployeeModelView GetNewModelView()
        {
            EmployeeModelView emv = new EmployeeModelView();
            emv.SuggestedSkills = skillServices.GetAllAsArrayString();
            emv.Employees = employeeServices.GetList(e=>true).ToList();
            return emv;
        }

        ///<summary>Deletes employee from database using it's id asynchronously</summary>
        public async Task DeleteEmployeeAsync(int id)
        {
            await employeeServices.DeleteAsync(id);
        }
    }
}
