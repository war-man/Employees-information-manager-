using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace EmployeesInformationManager.Data
{  
    public class ApplicationContextDesignFactory : DesignTimeDbContextFactoryBase<EmployeesInformationManagerContext>
    {
        public ApplicationContextDesignFactory() : base("DefaultConnection", typeof(Startup).GetTypeInfo().Assembly.GetName().Name)
        { }
        protected override EmployeesInformationManagerContext CreateNewInstance(DbContextOptions<EmployeesInformationManagerContext> options)
        {
            return new EmployeesInformationManagerContext(options);
        }
    }
}