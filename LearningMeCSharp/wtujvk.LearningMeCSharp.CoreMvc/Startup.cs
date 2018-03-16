using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using UEditor.Core;
using wtujvk.LearningMeCSharp.CoreMvc.Codes;

namespace wtujvk.LearningMeCSharp.CoreMvc
{
    public class Startup
    {
        string UeResources = String.Empty;
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            AppDataInit.WebRoot = environment.WebRootPath;
            AppDataInit.ContentRootPath = environment.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        private IServiceCollection _services;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // var dir= AppContext.BaseDirectory;
            AppDataInit.Init();
            UeResources = Configuration.GetSection("UeResources").Value;
            services.AddUEditorService(basePath:AppDataInit.WebRoot);
            services.AddSession();
            services.AddMvc();

            _services = services;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.Map("/allservices", builder => builder.Run(async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync($"<h1>所有服务{_services.Count}个</h1><table><thead><tr><th>类型</th><th>生命周期</th><th>Instance</th></tr></thead><tbody>");
                    foreach (var svc in _services)
                    {
                        await context.Response.WriteAsync("<tr>");
                        await context.Response.WriteAsync($"<td>{svc.ServiceType.FullName}</td>");
                        await context.Response.WriteAsync($"<td>{svc.Lifetime}</td>");
                        await context.Response.WriteAsync($"<td>{svc.ImplementationType?.FullName}</td>");
                        await context.Response.WriteAsync("</tr>");
                    }
                    await context.Response.WriteAsync("</tbody></table>");
                }));
                app.UseDeveloperExceptionPage();
            
           }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), UeResources)),
            //    RequestPath = "/"+ UeResources,
            //    OnPrepareResponse = ctx =>
            //    {
            //        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=36000");
            //    }
            //});
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
