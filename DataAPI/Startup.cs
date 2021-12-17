using DataAPI.Logging;
using DataAPI.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DataAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logging/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            
            services.AddSingleton(x => Log.Logger);
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<DealsRepository>();
            services.AddScoped<PlacesRepository>();
            services.AddScoped<ClientUsersRepository>();
            services.AddScoped<PlaceUsersRepository>();
            services.AddScoped<RatingsRepository>();
            services.AddScoped<FoodPlaceTypesRepository>();
            services.AddScoped<OrdersRepository>();
            //services.AddDbContext<AppDbContext>(o => o.UseSqlite("Data source=database.db"));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<AnalyticsMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}