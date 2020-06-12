using Microsoft.AspNetCore.Builder;	
using Microsoft.Extensions.Configuration;	
using Microsoft.Extensions.DependencyInjection;	

namespace EmployeesInformationManager.Startup_Strategies
{
    /// <remarks> Used to implement the startup strategy
    /// common methods between all enviroments.</remarks>
    public abstract class Abstract_Strategy:IStartup_Strategy
    {

        protected IConfiguration Configuration { get; }
        public Abstract_Strategy(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public abstract void ConfigureServices(IServiceCollection services);

        public abstract void Configure(IApplicationBuilder app);
        
        protected void ConfigureCommonServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }
        
        protected void ConfigureCommon(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Employee}/{action=Index}/{id?}");
            });
        }
    }
}
