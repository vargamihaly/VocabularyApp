using Microsoft.Extensions.Configuration;

namespace VocabularyApp.Presentation.Common;

public static class ConfigurationExtensions
{
    public static string GetDatabaseTechnology(this IConfigurationRoot configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var configValue = configuration.GetSection("Database").Value?.Trim();
        var isConfigSet = !string.IsNullOrWhiteSpace(configValue);
        var result = isConfigSet ? configValue : SupportedDatabases.MsSql;

        if (isConfigSet)
        {
            Console.WriteLine($"Chosen database technology: {result}");
        }
        else
        {
            Console.WriteLine($"Using default database technology: {SupportedDatabases.MsSql}");
        }

        return result!;
    }
}
