using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace VUE_CAR1678.Web.Helpers
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Controlamos si la aplicación se encuentra en producción o no.
            // Para ejecutar la aplicación en producción o en desarrollo.
            // 1 - Botón derecho sobre la API.
            // 2 - Pestaña depurar y Variables de entorno.
            // 3 - Cambiar Valor por:
            //                      - Development
            //                      - Production
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsProduction() || env.IsStaging())
            {
                app.UseExceptionHandler(
                    option => {
                        option.Run(
                            async context =>
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                var ex = context.Features.Get<IExceptionHandlerFeature>();
                                if (ex != null)
                                {
                                    await context.Response.WriteAsync(ex.Error.Message);
                                }
                            }
                          );
                    }
                    );

                //app.UseExceptionHandler(appBuilder =>
                //{
                //    appBuilder.Run(async c =>
                //    {
                //        c.Response.StatusCode = 500;
                //        await c.Response.WriteAsync("Something went horribl wrong, try again later...");
                //    });
                //});

            }
        }

    }
}
