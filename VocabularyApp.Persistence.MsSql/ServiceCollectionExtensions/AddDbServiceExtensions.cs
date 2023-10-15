using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VocabularyApp.Application.Entities;
using VocabularyApp.Application.Services.Words;
using VocabularyApp.Common.Core;
using VocabularyApp.Common.Core.Transactions;
using VocabularyApp.Initializer.DataProviders;
using VocabularyApp.Persistence.MsSql.Initializer;
using VocabularyApp.Persistence.MsSql.Services.Words;
using VocabularyApp.Persistence.MsSql.Transactions;
using Microsoft.Extensions.Configuration;


namespace VocabularyApp.Persistence.MsSql.ServiceCollectionExtensions;

public static class AddDbServiceExtensions
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration, string? environment)
    {
        ArgumentNullException.ThrowIfNull(environment);

        var dbSettings = configuration.GetSection("DbSettings").Get<DbSettings>();
        var connectionString = dbSettings.ConnectionString;

        var builder = new SqlConnectionStringBuilder(connectionString);

        if (environment == HostingEnvironments.Development)
        {
            services.AddDbContext<AppDbContext>(
                options => options
                    .UseSqlServer(builder.ConnectionString)
                .EnableSensitiveDataLogging());

        }
        else
        {
            services.AddDbContext<AppDbContext>(
                    options => options
                        .UseSqlServer(builder.ConnectionString));
        }

        return services;
    }

    public static IServiceCollection AddDbServiceProvider(this IServiceCollection services)
    {
        services.AddScoped<IWordServiceDataProvider, WordServiceDataProvider>();
        services.AddTransient<IDatabaseInitializerDataProvider, DatabaseInitializerDataProvider>();
        services.AddScoped<IDatabaseTestDataSeederDataProvider, DatabaseTestDataSeederDataProvider>();
        services.AddTransient<ITransactionExecutor, DbTransactionExecutor>();
        return services;
    }
}
