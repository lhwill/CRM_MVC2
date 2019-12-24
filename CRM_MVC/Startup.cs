using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_MVC.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static CRM_MVC.Common.CustomExceptionMiddleWareOption;

namespace CRM_MVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;//关闭GDPR规范    
                                                              //  options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //保存到内存
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //开发环境
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //非开发环境
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            //用户session
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=EmployeeInfo}/{action=Login}/{id?}");
            });

            app.UseStaticFiles();
            app.UseCustomException(new CustomExceptionMiddleWareOption(
                    handleType: CustomExceptionHandleType.PageHandle,  //跳转网页处理方式
                    jsonHandleUrlKeys: new PathString[] { "/api" },//Json处理方式的Url关键字
                    errorHandingPath: "/home/error"));//错误跳转页面
         

        }
       

    }
    public static class CustomExceptionMiddleWareExtensions
    {

        public static IApplicationBuilder UseCustomException(this IApplicationBuilder app, CustomExceptionMiddleWareOption option)
        {
            return app.UseMiddleware<CustomExceptionMiddleWare>(option);
        }
    }
}
