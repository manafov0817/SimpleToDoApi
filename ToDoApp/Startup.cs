using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using ToDoApp.Data.Data;
using ToDoApp.WebApi.Mapper;
using ToDoApp.WebApi.Repository.Abstract;
using ToDoApp.WebApi.Repository.Concrete;

namespace ToDoApp
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

            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IToDoListRepository, ToDoListRepository>();

            services.AddAutoMapper(typeof(ToDoMapping));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ToDoApi",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "To Do Api",
                        Version = "1"
                    });

                string[] name = Assembly.GetExecutingAssembly().GetName().Name.Split(".");

                var xmlCommentFile = $"{name[0]}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);

                options.IncludeXmlComments(cmlCommentsFullPath);
            });

            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = false;
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
         
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed(new ToDoAppDbContext());
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/ToDoApi/swagger.json", "To Do Api");
                options.RoutePrefix = "";
             });

            app.UseRouting();

            app.UseAuthorization();

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
