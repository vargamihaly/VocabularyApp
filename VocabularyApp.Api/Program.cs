using VocabularyApp.Common.Core;
using VocabularyApp.Initializer;
using VocabularyApp.Api.Middlewares;
using VocabularyApp.Persistence.MsSql.ServiceCollectionExtensions;
using VocabularyApp.Api.ServiceConfigurationExtensions;

#pragma warning disable CA1852
// disabling CA1852 here is a workaround for a known-issue: https://github.com/dotnet/roslyn-analyzers/issues/6141

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: false)
    .AddEnvironmentVariables()
    .Build();

var builder = WebApplication.CreateBuilder(args);

using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
    .SetMinimumLevel(LogLevel.Trace)
    .AddConsole());
ILogger<Program> logger = loggerFactory.CreateLogger<Program>();


builder.Services
    .ConfigureDbAppSettings(configuration)
    .AddDbServiceProvider()
    .AddAppDbContext(configuration, environment);

builder.Services
    .AddLogging()
    .AddInitializerService()
    .ConfigureScrutor("VocabularyApp")
    .ConfigureSwagger(logger)
    .ConfigureAutomapper()
    .AddControllers();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins("*")  
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
    
});

var app = builder.Build();

/************************* ************************* *************************/


if (app.Environment.EnvironmentName == HostingEnvironments.Development)
{
    app.UseDeveloperExceptionPage();
    app.UseOpenApi();
    app.UseSwaggerUi3();
}
else
{
    app.UseHsts();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

await app.Services.RunStartupTasksAsync(app.Logger);

// var appVersionProvider = app.Services.GetRequiredService<IApplicationVersionProvider>();
// app.Logger.LogApplicationStarted(appVersionProvider.ApplicationVersion);

await app.RunAsync();