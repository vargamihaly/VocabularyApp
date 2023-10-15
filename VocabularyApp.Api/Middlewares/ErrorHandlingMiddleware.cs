using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using VocabularyApp.Application.ErrorHandling;

namespace VocabularyApp.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ErrorHandlingMiddleware> logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        this.next = next;
        logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
    }

    private enum ProtectedWordType
    {
        NotProtected,
        Removable,
        ToShorten,
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        await LogHttpRequestAsync(context.Request, logger);

        if (exception is VocabularyAppException vocabularyAppException)
        {
            logger.LogWarning(exception, $"VocabularyAppException occurred with code: {vocabularyAppException.Code}");

            await WriteResponseAsync(context, HttpStatusCode.PreconditionFailed, $"VocabularyAppException occurred with code: {vocabularyAppException.Code}");
        }
        else
        {
            logger.LogError(exception, "Unexpected exception occurred!");

            await WriteResponseAsync(context, HttpStatusCode.InternalServerError, new ErrorDto() { Error = exception.Message });
        }
    }
    
    private static async Task LogHttpRequestAsync(HttpRequest httpRequest, ILogger logger)
    {
        try
        {
            httpRequest.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(httpRequest.Body);
            var body = await reader.ReadToEndAsync();
            var requestObj = new
            {
                httpRequest.Method,
                httpRequest.Path,
                httpRequest.QueryString,
                Body = JsonConvert.DeserializeObject(body),
                Headers = JsonConvert.SerializeObject(httpRequest.Headers),
            };
            var requestJson = JsonConvert.SerializeObject(requestObj, Formatting.Indented);

            var jObject = JObject.Parse(requestJson);
            RemoveProtectedContent(jObject, ProtectedWordType.NotProtected);

            logger.LogError($"-- FAULTY REQUEST: {jObject}");
        }
        catch (Exception e)
        {
            logger.LogCritical(e, $"FAILED TO LOG FAULTY HTTP REQUEST!");
        }
    }

    private static void RemoveProtectedContent(JToken jToken, ProtectedWordType wordType)
    {
        var wordsRemove = new string[] { "password" };
        var wordsToShorten = new string[] { "base64" };

        if (wordType == ProtectedWordType.Removable && !jToken.HasValues && jToken.Parent is JProperty pprop) pprop.Value = "(removed from log)";
        if (wordType == ProtectedWordType.ToShorten && !jToken.HasValues && jToken.Parent is JProperty sprop) sprop.Value = $"{sprop.Value.ToString()[..150]}...";

        foreach (var x in jToken)
        {
            if (x is not JProperty jProperty) continue;

            RemoveProtectedContent(jProperty.Value, GetProtectedWordType(jProperty, wordsRemove, wordsToShorten));
        }
    }

    private static ProtectedWordType GetProtectedWordType(
        JProperty jProperty,
        IEnumerable<string> wordsToRemove,
        IEnumerable<string> wordsToShorten)
    {
        var containsProtected = wordsToRemove.Any(s => jProperty.Name.Contains(s, StringComparison.OrdinalIgnoreCase));
        var containsToBeShortened = wordsToShorten.Any(s => jProperty.Name.Contains(s, StringComparison.OrdinalIgnoreCase));

        if (containsProtected) return ProtectedWordType.Removable;
        if (containsToBeShortened) return ProtectedWordType.ToShorten;

        return ProtectedWordType.NotProtected;
    }

    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "HttpContext exists by the will of the God of the Mighty Framework")]
    public async Task Invoke(HttpContext context)
    {
        try
        {
            // Ensures if the request's body can be read multiple times. Normally buffers request bodies in memory; writes requests larger than 30K bytes to disk. Check performance effects!
            context.Request.EnableBuffering();

            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task WriteResponseAsync(HttpContext context, HttpStatusCode httpStatusCode, object error)
    {
        var serializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), };
        serializerSettings.Converters.Add(new StringEnumConverter());

        var result = JsonConvert.SerializeObject(error, serializerSettings);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)httpStatusCode;

        return context.Response.WriteAsync(result);
    }

   
}