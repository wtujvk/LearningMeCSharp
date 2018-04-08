using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Demo.LindAgile.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using wtujvk.LearningMeCSharp.WebApiDemo.Codes;
using wtujvk.LearningMeCSharp.WebApiDemo.Codes.Repositorys;

namespace wtujvk.LearningMeCSharp.WebApiDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        string Appname= $"{typeof(Startup).Namespace}";
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MysqlDbContext>(opt => opt.UseMySQL(Configuration.GetConnectionString("MySqlConnection")));
            services.AddMvc()
        .AddJsonOptions(options =>
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
        });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = Appname
                });

                //Determine base path for the application.  
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //Set the comments path for the swagger json and ui.  
                var xmlname = $"{Appname}.xml";
                var xmlPath = Path.Combine(basePath, xmlname);
                options.IncludeXmlComments(xmlPath);
            });
            services.AddTransient(typeof(IExtensionRepository<>), typeof(EFRepository<>));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //使用自定义异常处理
            app.UseErrorHandling(new ExceptionHandlerOptions()
            {
                //注意此处的路径不能使用~符号
                ExceptionHandlingPath = "/Home/Error"
            });
           // app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "webapi",
                   template: "api/{controller}/{action}/{id?}",
                   defaults: new { controller = "Home", action = "Index" });
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Appname} API V1");
            });
        }
    }
}
