using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECarShop.BL.Interfaces;
using ECarShop.BL.Services;
using ECarShop.DL.Interfaces;
using ECarShop.DL.Repositories;
using Microsoft.OpenApi.Models;
using Serilog;
using FluentValidation.AspNetCore;

namespace ECarShop
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
           

            services.AddSingleton(Log.Logger);
            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<ICarRepository, CarInMemoryRepository>();
            services.AddSingleton<IClientRepository, ClientInMemoryRepository>();
            services.AddSingleton<IDealerRepository, DealerInMemoryRepository>();
            

            services.AddSingleton<ICarService, CarService>();
            services.AddSingleton<IClientService, ClientService>();
            services.AddSingleton<IDealerService, DealerService>();



            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECarShop", Version = "v1" });
            });

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECarShop v1"));
            }



           
            app.UseHttpsRedirection();
            

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
