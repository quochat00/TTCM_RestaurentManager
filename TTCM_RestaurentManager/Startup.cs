using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Session;
using System.Web;
namespace TTCM_RestaurentManager
{
	public class Startup
	{
		public void Configure(IApplicationBuilder app)
		{
			app.UseSession();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(5);
			});
		}
	}
}
