using Microsoft.AspNetCore.Builder;
using NJsonSchema;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;

namespace BoardGamesApi.Extensions
{
    public static class SwaggerExtensions
    {
        public static void UseSwagger(this IApplicationBuilder app)
        {
            app.UseSwaggerUi3WithApiExplorer(
                settings =>
                {
                    settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;

                    settings.GeneratorSettings.OperationProcessors.Add(
                        new OperationSecurityScopeProcessor("jwt-token"));
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
        }
    }
}
