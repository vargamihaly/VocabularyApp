namespace VocabularyApp.Api.Extensions;

public static class ContextExtensions
{
    public static string GetRequestString(this HttpContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        context.Request.Headers.TryGetValue("User-Agent", out var userAgent);
        context.Request.Headers.TryGetValue("X-App-Version", out var xAppVersion);

        var user = GetUser(context);

        var method = context.Request.Method;
        var protocol = context.Request.IsHttps ? "https" : "http";
        var host = context.Request.Host.HasValue ? context.Request.Host.Value : "(no host data)";
        var path = context.Request.Path.HasValue ? context.Request.Path.Value : "";
        var queryString = context.Request.QueryString;

        var requestString = $"{userAgent}: [{method}][{user}][{xAppVersion}] {protocol}://{host}{path}{queryString}";

        return requestString;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "This is just a placeholder code, solely for educational purposes")]
    private static string GetUser(HttpContext context)
    {
        // if there is a user, then get user's details here
        return "";
    }
}
