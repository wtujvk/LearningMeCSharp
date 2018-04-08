using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Demo.LindAgile.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using wtujvk.LearningMeCSharp.WebApiDemo.Codes;
using wtujvk.LearningMeCSharp.WebApiDemo.Codes.Filters;
using wtujvk.LearningMeCSharp.WebApiDemo.Codes.Ioc;
using wtujvk.LearningMeCSharp.WebApiDemo.Codes.Repositorys;

namespace wtujvk.LearningMeCSharp.WebApiDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<MysqlDbContext>(opts => opts.UseMySQL(Configuration.GetConnectionString("MySqlConnection")));
            services.AddTransient(typeof(IExtensionRepository<>), typeof(MyEFREpository<>));
            services.AddTransient<IExtensionRepository<LoginInfo>, MyEFREpository<LoginInfo>>();
            services.AddTransient<DbContext, MysqlDbContext>();
            //IocHelper.Register<IExtensionRepository<LoginInfo>, EFRepository<LoginInfo>>();
           // IocHelper.Register(typeof(IExtensionRepository<LoginInfo>), typeof(EFRepository<LoginInfo>));
            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }); ;
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = "https://twitter.com/spboyer"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
                c.OperationFilter<SwaggerFileUploadFilter>();//文件上传过滤器
                c.OperationFilter<HttpHeaderFilter>(); //全局过滤器，权限过滤
            });

            //跨域支持
            services.AddCors(opt => opt.AddPolicy("WebAPIPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseMvcWithDefaultRoute();
            app.UseMvc();
            app.UseStaticFiles();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
