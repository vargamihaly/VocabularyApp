using Microsoft.Data.SqlClient;
using VocabularyApp.Application;

namespace VocabularyApp.Api.ServiceConfigurationExtensions;

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
            logger.LogCritical(e, "An error occurred while running application initializers!");
            throw;
        }
    }
}
