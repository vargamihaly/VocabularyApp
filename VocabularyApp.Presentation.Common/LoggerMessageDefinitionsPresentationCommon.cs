using Microsoft.Extensions.Logging;

namespace VocabularyApp.Presentation.Common;

public static class WebEventIds
{
    public const int ApplicationInitializationFailed = 1;
}

/// <summary>
/// More about this the new .NET 6 high performance logging: https://docs.microsoft.com/en-us/dotnet/core/extensions/high-performance-logging
/// </summary>
public static partial class LoggerMessageDefinitionsPresentationCommon
{
    [LoggerMessage(EventId = WebEventIds.ApplicationInitializationFailed, Level = LogLevel.Critical, Message = "An error occurred while running application initializers!")]
    public static partial void ApplicationInitializationFailed(this ILogger logger, Exception ex);
}
