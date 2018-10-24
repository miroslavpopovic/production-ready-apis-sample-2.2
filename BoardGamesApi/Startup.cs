using BoardGamesApi.Data;
using BoardGamesApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddMvc()
                // HACK: Had to fall back to 2.1 compatibility to support API Versioning
                // https://github.com/Microsoft/aspnet-api-versioning/issues/363
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddVersioning();

            services.AddSwagger();

            services.AddHealthChecks();
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

            app.UseMiddleware<LimitingMiddleware>();

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseHealthChecks("/live");

            app.UseMvc();
        }
    }
}
