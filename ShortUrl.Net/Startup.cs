
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ShortUrlNet.Models;
using ShortUrlNet.Services;

namespace ShortUrlNet
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddSingleton<IRepository, Repository>();
            services.AddSingleton<ISecurityService, SecurityService>();
            services.AddSingleton<IUrlService, UrlService>();
            services.AddSingleton<TestDataService>();
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.ApplicationServices.GetRequiredService<TestDataService>().LoadData();

            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
