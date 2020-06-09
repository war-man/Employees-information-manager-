using Microsoft.Extensions.Configuration;
using EmployeesInformationManager.Data;	
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;	
namespace EmployeesInformationManager.Startup_Strategies
{
    public class Development_Strategy : Abstract_Strategy
    {
        public Development_Strategy(IConfiguration configuration) 
            : base(configuration)
        {
        }
        override
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);
            services.AddDbContext<EmployeesInformationManagerContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlite(connectionString);
            });
        }

        override
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            ConfigureCommon(app);
        }
    }
}
