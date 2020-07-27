using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace EmployeesInformationManager.Startup_Strategies
{
	public class StartupStrategyFactory
	{

		private IConfiguration configuration;

		public StartupStrategyFactory(IConfiguration _configuration)
		{
			configuration = _configuration;
		}

		public IStartup_Strategy CreateStartupStrategy(IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				return  new Development_Strategy(configuration);
			return new Production_Strategy(configuration);
		}
	}
}
