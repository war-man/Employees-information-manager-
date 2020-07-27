using EmployeesInformationManager.Models;
using EmployeesInformationManager.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace EmployeesInformationManager.Repositories
{
    public class EmployeeRepository : IRepository<Employee,int>
    {
        private EmployeesInformationManagerContext context;

        public EmployeeRepository(EmployeesInformationManagerContext context)
        {
            this.context = context;
        }

        public Employee GetById(int employeeId)
        {
            return context.Employee.Find(employeeId);
        }
        
        public IQueryable<Employee> GetList(Expression<Func<Employee,Boolean>> filter){
            return context.Employee.
                   Where(filter);
        }

        public void Add(Employee employee)
        {
             context.Employee.Add(employee);
        }
        public void Delete(int employeeId)
        {
            Employee employee =  context.Employee.Find(employeeId);
            context.Employee.Remove(employee);
        }
        
        public void DeleteRange(Expression<Func<Employee,Boolean>> filter){
            context.Employee.
            RemoveRange(context.Employee
                .Where(filter)
            );
        }
        public void Update(Employee employee)
        {
            context.Employee.Update(employee);
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
        
    }
}