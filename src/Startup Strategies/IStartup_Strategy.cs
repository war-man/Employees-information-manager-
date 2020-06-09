using Microsoft.AspNetCore.Builder;	
using Microsoft.Extensions.DependencyInjection;
namespace EmployeesInformationManager.Startup_Strategies
{
    interface IStartup_Strategy
    {
        public void ConfigureServices(IServiceCollection services);

        public void Configure(IApplicationBuilder app);
    }
}
