using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UEditorNetCore;
using wtujvk.LearningMeCSharp.CoreMvc.Codes;

namespace wtujvk.LearningMeCSharp.CoreMvc
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
            // var dir= AppContext.BaseDirectory;
            AppDataInit.WebRoot= Directory.GetCurrentDirectory();
            AppDataInit.Init();
            //F:\study\github\LearningMeCSharp\LearningMeCSharp\wtujvk.LearningMeCSharp.CoreMvc\wwwroot\ueditor\config2.json
           var configjsonpath= System.IO.Path.Combine(AppDataInit.WebRoot, "wwwroot//lib//ueditor//config2.json");
            bool exsit = System.IO.File.Exists(configjsonpath);
            services.AddUEditorService(configjsonpath);
            services.AddSession();
            services.AddMvc();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseSession();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
