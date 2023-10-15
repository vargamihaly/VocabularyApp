namespace VocabularyApp.Initializer.DataProviders;

public interface IDatabaseInitializerDataProvider
{
    Task<bool> DoesDatabaseExistAsync();
    Task<bool> IsEmptyDatabaseAsync();
    Task MigrateAsync();
}
