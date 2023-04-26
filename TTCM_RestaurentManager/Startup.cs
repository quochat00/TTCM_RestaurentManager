using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
namespace TTCM_RestaurentManager
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // ...
            services.AddSingleton<IWebHostEnvironment>(_env);
            // ...
        }


        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            // ...
        }
    }
}
