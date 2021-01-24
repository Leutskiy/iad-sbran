using System;
using Microsoft.Extensions.DependencyInjection;
using NSwag.Generation.AspNetCore;

namespace Sbran.WebApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, string title, Action<AspNetCoreOpenApiDocumentGeneratorSettings>? configure = null)
        {
            services.AddOpenApiDocument(openApiSettings =>
            {
                // Устанавливает заголовок для API
                openApiSettings.PostProcess = d => d.Info.Title = title;

                // Включает генерацию схемы со Enum-значениями вместо int
                openApiSettings.GenerateEnumMappingDescription = true;
				
                // Включает построение схемы модели вместе с подклассами
                openApiSettings.AllowReferencesWithProperties = true;

                configure?.Invoke(openApiSettings);
            });

            return services;
        }


    }
}