using Microsoft.AspNetCore.Builder;	
using Microsoft.Extensions.DependencyInjection;
namespace EmployeesInformationManager.Startup_Strategies
{
    /// <remarks> Used to declare the startup strategy in different enviroments
    public interface IStartup_Strategy
    {
        /// <summary> Configures the services used by the program like the database context.</summary>
        public void ConfigureServices(IServiceCollection services);


        /// <summary> Configures the app features like routing , authorization and endpoints used by the program.</summary>
        public void Configure(IApplicationBuilder app);
    }
}
