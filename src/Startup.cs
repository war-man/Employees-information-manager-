using Microsoft.AspNetCore.Builder;	
using Microsoft.AspNetCore.Hosting;	
using Microsoft.Extensions.Configuration;	
using Microsoft.Extensions.DependencyInjection;	
using EmployeesInformationManager.Startup_Strategies;
using Microsoft.Extensions.Hosting;

namespace EmployeesInformationManager
{
    public class Startup
    {
        private IStartup_Strategy startup_Strategy { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            StartupStrategyFactory startupStrategyFactory = new StartupStrategyFactory(configuration);
            startup_Strategy = startupStrategyFactory.CreateStartupStrategy(env);
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            startup_Strategy.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            startup_Strategy.Configure(app);
        }
    }
}