using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTicket.Data.Contexts;
using TrainTicket.Data.Repositories.Abstract;
using TrainTicket.Data.UnitOfWork.Abstract;
using TrainTicket.Data.UnitOfWork.Concrete;
using TrainTicket.Entity.Entities;
using TrainTicket.MVC.AutoMapper.Profiles;
using TrainTicket.Service.Abstract;
using TrainTicket.Service.Concrete;

namespace TrainTicket.MVC
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
            services.AddAutoMapper(typeof(TicketProfile) , typeof(TrainRouteProfile) , typeof(CityProfile));
            services.AddDbContext<TrainTicketContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<ITicketService,TicketManager>();
            services.AddScoped<ICityService,CityManager>();
            services.AddScoped<ITrainRouteService,TrainRouteManager>();

            services.AddMvc();


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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
