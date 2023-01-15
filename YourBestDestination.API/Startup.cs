using YourBestDestination.BLL.Services;
using YourBestDestination.DAL.Repositories;
using YourBestDestination.DAL;
using YourBestDestination.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using YourBestDestination.Infrastructure.Extensions;

namespace YourBestDestination.API
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
            services.AddControllers();

            services.AddDbContext<ApplicationContext>(options =>
                options.UseInMemoryDatabase(databaseName: "YourBestDestinationDB"));

            #region Repositories
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IHotelRepository, HotelRepository>();
            #endregion

            #region Services
            services.AddTransient<IHotelService, HotelService>();
            services.AddTransient<ILocationService, LocationService>();
            #endregion

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionHandler();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
