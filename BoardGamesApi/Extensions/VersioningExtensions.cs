using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGamesApi.Extensions
{
    public static class VersioningExtensions
    {
        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                });
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstitutionFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                    options.ApiVersionParameterSource = new UrlSegmentApiVersionReader();
                });
        }
    }
}
