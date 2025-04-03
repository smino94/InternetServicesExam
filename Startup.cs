using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MovieRentalApp.Data;
using MovieRentalApp.Data.Interfaces;
using MovieRentalApp.Data.Repositories;
using MovieRentalApp.Service.Interfaces;
using MovieRentalApp.Service.Mapping;
using MovieRentalApp.Service.Services;

namespace MovieRentalApp.WebApi
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
            // Use In-Memory Database instead of SQL Server
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("MovieRentalAppDb"));

            // Repositories
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            // Services
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IMovieService, MovieService>();

            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddControllers();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie Rental API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Rental API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Seed database with initial data
            SeedDatabase(dbContext);
        }

        private void SeedDatabase(ApplicationDbContext dbContext)
        {
            // Ensure database is created
            dbContext.Database.EnsureCreated();
        }
    }
}