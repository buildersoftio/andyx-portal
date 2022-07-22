using Andy.X.Portal.Configurations;
using Andy.X.Portal.Services.Components;
using Andy.X.Portal.Services.Consumers;
using Andy.X.Portal.Services.Producers;
using Andy.X.Portal.Services.Products;
using Andy.X.Portal.Services.Storages;
using Andy.X.Portal.Services.Tenants;
using Andy.X.Portal.Services.Topics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Andy.X.Portal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var xNodeConfiguration = new XNodeConfiguration();
            Configuration.Bind("XNode", xNodeConfiguration);
            services.AddSingleton(xNodeConfiguration);

            services.AddSingleton<StorageService>();
            services.AddSingleton<TenantService>();
            services.AddSingleton<ProductService>();
            services.AddSingleton<ComponentService>();
            services.AddSingleton<TopicService>();
            services.AddSingleton<ProducerService>();
            services.AddSingleton<ConsumerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>

            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
