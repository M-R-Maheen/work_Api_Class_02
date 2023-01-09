using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_Api_Class_02.Models;

namespace work_Api_Class_02
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
            services.AddControllers();
            services.AddDbContext<VehicleDbContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("AppCon")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VehicleDbContext db)
        {
            if (!db.VehicleTypes.Any())
            {
                VehicleType t = new VehicleType
                {
                    TypeName = "Car",
                    SuitableFor = "Personal Use"
                };
                t.Vehicles.Add(new Vehicle
                {
                    Model = "Porshe 9G",
                    Capacity = 5,
                    MainFeature = "Volor Options, Comfortable for any surface"
                });
                db.VehicleTypes.Add(t);
                db.SaveChanges();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
