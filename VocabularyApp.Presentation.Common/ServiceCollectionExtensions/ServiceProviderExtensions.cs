using VocabularyApp.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace VocabularyApp.Presentation.Common.ServiceCollectionExtensions;

public static class ServiceProviderExtensions
{
    public static async Task RunStartupTasksAsync(this IServiceProvider services, ILogger logger)
    {
        using var scope = services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        try
        {
            var applicationInitializers = serviceProvider.GetServices<IApplicationInitializer>();

            foreach (var appInitializer in applicationInitializers)
            {
                await appInitializer.StartAsync();
            }
        }
        catch (Exception e)
        {
            logger.ApplicationInitializationFailed(e);
            throw;
        }
    }
}
