using VocabularyApp.Common.Core;
using VocabularyApp.Common.Core.Reflection;

namespace VocabularyApp.Api.ServiceConfigurationExtensions;

public static class ScrutorExtensions
{
    public static IServiceCollection ConfigureScrutor(this IServiceCollection services, string assemblyPrefix)
    {
        // more Scrutor examples and scenarios:
        // https://andrewlock.net/using-scrutor-to-automatically-register-your-services-with-the-asp-net-core-di-container/
        // Scrutor Github repo:
        // https://github.com/khellang/Scrutor

        services.Scan(scan => scan
            .FromAssemblies(AssemblyScanner.GetAssemblies(assemblyPrefix))
            .AddClasses(classes => classes.AssignableTo<ITransient>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()
            .AddClasses(classes => classes.AssignableTo<IScoped>())
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo<ISingleton>())
            .AsSelfWithInterfaces()
            .WithSingletonLifetime());

        return services;
    }
}
