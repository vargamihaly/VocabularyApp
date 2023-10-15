using VocabularyApp.Application;
using VocabularyApp.Initializer.DataProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace VocabularyApp.Initializer;

public class DatabaseInitializer : IApplicationInitializer
{
    private readonly IDatabaseInitializerDataProvider dataProvider;
    private readonly ILogger<DatabaseInitializer> logger;
    private readonly IConfiguration configuration;
    private readonly IDatabaseTestDataSeeder databaseTestDataSeeder;
    private readonly string configSectionSeedTestData = "Developer:SeedTestData";

    public DatabaseInitializer(
        ILogger<DatabaseInitializer> logger,
        IDatabaseInitializerDataProvider dataProvider,
        IConfiguration configuration,
        IDatabaseTestDataSeeder databaseTestDataSeeder)
    {
        this.logger = logger;
        this.dataProvider = dataProvider;
        this.configuration = configuration;
        this.databaseTestDataSeeder = databaseTestDataSeeder;
    }

    public async Task StartAsync()
    {
        logger.LogInformation($"Database seeder started...");

        var dbExists = await dataProvider.DoesDatabaseExistAsync();

        if (!dbExists)
        {
            logger.LogDebug("Database does not exist! Creating it...");
        }

        var dbEmpty = !dbExists || await dataProvider.IsEmptyDatabaseAsync();
        logger.LogDebug($"Database is empty: {dbEmpty}");

        await dataProvider.MigrateAsync();

        logger.LogInformation("Database migrations have run successfully!");

        if (dbEmpty)
        {
            logger.LogInformation("Seeding data using initializer...");
            
            if (UseTestData())
            {
                logger.LogInformation("Seeding TEST data...");
                await databaseTestDataSeeder.SeedTestDataAsync().ConfigureAwait(false);
            }
        }
        else
        {
            logger.LogInformation("Database already exists, no further action taken.");
        }
    }

    private bool UseTestData()
    {
        if (!configuration.GetSection($"{configSectionSeedTestData}").Exists()) return false;
        if (!bool.TryParse(configuration.GetSection($"{configSectionSeedTestData}").Value, out var useTestData)) return false;

        return useTestData;
    }
}
