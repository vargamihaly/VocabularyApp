using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.Globalization;

namespace VocabularyApp.Presentation.Common.ServiceCollectionExtensions;

public static class LoggerExtensions
{
    public static IServiceCollection ConfigureLoggers(this IServiceCollection services, IConfiguration configuration)
    {
        var currentDirectory = Environment.CurrentDirectory;
        Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        var consoleLogger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration, "SerilogConsole")
            .Enrich.FromLogContext()
            .CreateLogger();

        var ownLogger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration, "SerilogOwn")
            .Enrich.FromLogContext()
            .CreateLogger();

        var allLogger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration, "SerilogAll")
            .Enrich.FromLogContext()
            .WriteTo.File(
                Path.Combine(AppContext.BaseDirectory),
                LevelAlias.Minimum,
                "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                CultureInfo.InvariantCulture)
            .CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder
                .ClearProviders()
                .AddSerilog(consoleLogger)
                .AddSerilog(ownLogger)
                .AddSerilog(allLogger);
        });

        Environment.CurrentDirectory = currentDirectory;

        return services;
    }
}
