using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Reflection;
using TimeTrackingApi._1.Data.Configuration;
using TimeTrackingApi._1.Data.Interface;
using TimeTrackingApi._2.Repositoty;
using TimeTrackingApi._2.Repositoty.IRepository;

namespace TimeTrackingApi
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

            //dependency injection
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskSpentTimeRepository, TaskSpentTimeRepository>();

            // MongoDB
            services.Configure<TimeTrackingStoreDatabaseSettings>( Configuration.GetSection(nameof(TimeTrackingStoreDatabaseSettings)));            
            services.AddSingleton<ITimeTrackingStoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<TimeTrackingStoreDatabaseSettings>>().Value);          
            services.AddSingleton<ISettingsService, SettingServices>();


            //Swagger Config 
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ApiUsers", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "API TimeTracker",
                    Version = "1",
                    Description = "Backend TimeTracker",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "ema18gm@gamil.com",
                        Name = "Emmanuel Garcia",
                        Url = new Uri("Https://www.linkedin.com/in/emmanuelgarcia")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }
                });

                options.SwaggerDoc("ApiProjects", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "API TimeTracker",
                    Version = "1",
                    Description = "Backend TimeTracker",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "ema18gm@gamil.com",
                        Name = "Emmanuel Garcia",
                        Url = new Uri("Https://www.linkedin.com/in/emmanuelgarcia")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }
                });

                options.SwaggerDoc("ApiTaks", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "API TimeTracker",
                    Version = "1",
                    Description = "Backend TimeTracker",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "ema18gm@gamil.com",
                        Name = "Emmanuel Garcia",
                        Url = new Uri("Https://www.linkedin.com/in/emmanuelgarcia")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var commentsApiPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                options.IncludeXmlComments(commentsApiPath);

            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //swagger documentation
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/ApiUsers/swagger.json", "Users Controller");
                options.SwaggerEndpoint("/swagger/ApiProjects/swagger.json", "Projects Controller");
                options.SwaggerEndpoint("/swagger/ApiTaks/swagger.json", "Tasks Controller");

                options.RoutePrefix = "swagger";
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
