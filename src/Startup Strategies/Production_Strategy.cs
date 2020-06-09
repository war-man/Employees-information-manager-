using Microsoft.Extensions.Configuration;
using EmployeesInformationManager.Data;
using Microsoft.AspNetCore.Builder;	
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
namespace EmployeesInformationManager.Startup_Strategies
{
    public class Production_Strategy : Abstract_Strategy
    {
        public Production_Strategy(IConfiguration configuration) 
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
                options.UseSqlServer(connectionString);
            });
        }

        override
        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
            ConfigureCommon(app);
        }
    }
}
