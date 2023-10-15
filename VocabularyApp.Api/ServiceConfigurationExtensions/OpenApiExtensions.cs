using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace VocabularyApp.Api.ServiceConfigurationExtensions;

/// <summary>
/// https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag
/// </summary>
public static class OpenApiExtensions
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services, ILogger logger)
    {
        services.AddSwaggerDocument(settings =>
        {
            settings.PostProcess = document =>
            {
                document.Info.Title = "Application Template API";
                document.Info.TermsOfService = "All rights reserved.";
                document.Info.Contact = new NSwag.OpenApiContact
                {
                    Name = "Misi",
                    Email = "vmisi20[at]gmail[d0t]com"
                };
            };
            settings.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };
            settings.SerializerSettings.Converters.Add(new StringEnumConverter());
        });

        return services;
    }
}
