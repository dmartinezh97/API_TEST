using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VUE_CAR1678.Datos;
using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using VUE_CAR1678.Web.Helpers;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using VUE_CAR1678.Web.Middlewares;
using Newtonsoft.Json.Serialization;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace VUE_CAR1678.Web
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
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Conexion")));

            //Cors
            //services.ConfigureCors();
            services.AddCors(options => {
                //options.AddPolicy("Todos", builder => builder.WithOrigins("*").WithHeaders("*").WithMethods("*"));
                options.AddPolicy("Todos", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.ConfigureAuthentication(Configuration);

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //Dejar JSON por defecto
            services.AddMvc()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            AddSwagger(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Control de excepciones.
            app.ConfigureExceptionHandler(env);
            // app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API v1");
            });

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();                       

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Función de configuración extensión Swagger.       
        /// </summary>
        /// <param name="services"></param>
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                // Info de cabecera
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Doc {groupName}",
                    Version = groupName,
                    Description = "web services API (.Net Core)",
                    Contact = new OpenApiContact
                    {
                        Name = "at infoserveis",
                        Email = "info@atinfoserveis.com",
                        Url = new Uri("https://www.atinfoserveis.com/web/"),
                    }
                });

                // Poder añadir token
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Token usar Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                // Incluyendo los comentarios xml de los endpoint
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);               
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
