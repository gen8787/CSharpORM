using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MapProject
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
        //~~ A D D   L I N E S  B E L O W ~~ //
            services.AddDbContext<MapProjectContext>(options => options.UseMySql (Configuration["DBInfo:ConnectionString"]));
            services.AddHttpContextAccessor(); //@using Microsoft.AspNetCore.Http in _ViewImports.cshtml
            services.AddSession();
            services.AddMvc(options => options.EnableEndpointRouting = false);
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
            }
        //~~ A D D   L I N E S   B E L O W ~~ //
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(); // make sure UseMvc comes last
        }
    }
}
