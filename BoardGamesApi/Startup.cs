using BoardGamesApi.Data;
using BoardGamesApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;

namespace BoardGamesApi
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
            services.AddSingleton<IGamesRepository, GamesRepository>();

            services.AddJwtBearerAuthentication(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Inject our custom error handling middleware into ASP.NET Core pipeline
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseSwaggerUi3WithApiExplorer(
                settings =>
                {
                    settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;
                    settings.PostProcess = document =>
                    {
                        document.Info.Version = "v1";
                        document.Info.Title = "Board Games API v1";
                        document.Info.Description = "A sample API for presentation purpose";
                        document.Info.TermsOfService = "Do whatever you want with it :)";
                        document.Info.Contact = new NSwag.SwaggerContact
                        {
                            Name = "Miroslav Popovic",
                            Email = string.Empty,
                            Url = "https://miroslavpopovic.com"
                        };
                        document.Info.License = new NSwag.SwaggerLicense
                        {
                            Name = "MIT",
                            Url = "https://opensource.org/licenses/MIT"
                        };
                    };
                });

            app.UseMvc();
        }
    }
}
