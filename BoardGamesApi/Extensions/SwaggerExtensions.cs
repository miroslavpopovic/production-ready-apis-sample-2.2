using Microsoft.AspNetCore.Builder;
using NJsonSchema;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.AspNetCore;
using NSwag.SwaggerGeneration.Processors;
using NSwag.SwaggerGeneration.Processors.Security;

namespace BoardGamesApi.Extensions
{
    public static class SwaggerExtensions
    {
        public static void UseSwagger(this IApplicationBuilder app)
        {
            // TODO: Update after the issues with ApiVersioning have been solved
            app.UseSwaggerWithApiExplorer(settings => { InitializeSwaggerForApiVersion(settings, "1"); });
            app.UseSwaggerWithApiExplorer(settings => { InitializeSwaggerForApiVersion(settings, "2"); });

            app.UseSwaggerUi3(
                settings =>
                {
                    settings.SwaggerRoutes.Add(new SwaggerUi3Route("v1", "/swagger/v1/swagger.json"));
                    settings.SwaggerRoutes.Add(new SwaggerUi3Route("v2", "/swagger/v2/swagger.json"));
                });
        }

        private static void InitializeSwaggerForApiVersion(
            SwaggerSettings<AspNetCoreToSwaggerGeneratorSettings> settings, string apiVersion)
        {
            settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;

            settings.GeneratorSettings.OperationProcessors.Add(new OperationSecurityScopeProcessor("jwt-token"));
            settings.GeneratorSettings.DocumentProcessors.Add(
                new SecurityDefinitionAppender(
                    "jwt-token", new SwaggerSecurityScheme
                    {
                        Type = SwaggerSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description =
                            "Enter \"Bearer jwt-token\" as value. " +
                            "Use https://localhost:44303/get-token to get read-only JWT token. " +
                            "Use https://localhost:44303/get-token?admin=true to get admin (read-write) JWT token.",
                        In = SwaggerSecurityApiKeyLocation.Header
                    }));

            settings.GeneratorSettings.OperationProcessors
                .TryGet<ApiVersionProcessor>().IncludedVersions = new[] {apiVersion};

            settings.SwaggerRoute = $"/swagger/v{apiVersion}/swagger.json";

            settings.PostProcess = document =>
            {
                document.Info.Version = $"v{apiVersion}";
                document.Info.Title = $"Board Games API v{apiVersion}";
                document.Info.Description = "A sample API for presentation purpose";
                document.Info.TermsOfService = "Do whatever you want with it :)";
                document.Info.Contact = new SwaggerContact
                {
                    Name = "Miroslav Popovic",
                    Email = string.Empty,
                    Url = "https://miroslavpopovic.com"
                };
                document.Info.License = new SwaggerLicense
                {
                    Name = "MIT",
                    Url = "https://opensource.org/licenses/MIT"
                };
            };
        }
    }
}
