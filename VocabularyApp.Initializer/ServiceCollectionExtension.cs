using VocabularyApp.Application;
using Microsoft.Extensions.DependencyInjection;

namespace VocabularyApp.Initializer;
public static class DbInitializerServiceCollectionExtension
{
    public static IServiceCollection AddInitializerService(this IServiceCollection services)
    {
        services.AddTransient<IApplicationInitializer, DatabaseInitializer>();
        services.AddScoped<IDatabaseTestDataSeeder, DatabaseTestDataSeeder>();

        return services;
    }
}
