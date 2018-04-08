using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace wtujvk.LearningMeCSharp.RedisSession
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
            services.AddMvc();
            //添加session
            services.AddSession(opts => {
                int timespan = Convert.ToInt32(Configuration["Redis:IdleTimeout"]);//设置Session闲置超时时间(有效时间周期)超时时间 例如 30
                opts.IdleTimeout = TimeSpan.FromMinutes(timespan); 
                opts.Cookie.HttpOnly = true;
            });
            var redisconfig = Configuration["Redis:Conn"];//redis连接字符串,例如：127.0.0.1
            //使用redis存储
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = redisconfig;
                option.InstanceName = Configuration["Redis:InstanceName"]; //任意名称 例如：MyRedis_
            });
            //使用分布式
            services.AddDataProtection()
                .SetApplicationName(Configuration["Redis:Session_application_name"]) //任意名称 例如：MyRedis_Distributed
                .PersistKeysToRedis(ConnectionMultiplexer.Connect(redisconfig), "DataProtection-Keys");
            var SqlServerCon = Configuration.GetConnectionString("SqlServer");
            ////使用SqlSever
            //services.AddDistributedSqlServerCache((opts) => {
            //    opts.ConnectionString = SqlServerCon;
            //    opts.SchemaName = "dbo";
            //    opts.TableName = "DistCache";
            //});

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
            app.UseSession();//使用session
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
