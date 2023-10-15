using VocabularyApp.Application.Entities;

namespace VocabularyApp.Api.ServiceConfigurationExtensions;

public static class DbConfigurationExtensions
{
    public static IServiceCollection ConfigureDbAppSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbSettings>(options => configuration.GetSection("DbSettings").Bind(options));

        return services;
    }
}
