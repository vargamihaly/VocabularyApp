using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using VocabularyApp.Initializer.DataProviders;

namespace VocabularyApp.Persistence.MsSql.Initializer;

public class DatabaseInitializerDataProvider : IDatabaseInitializerDataProvider
{
    private readonly AppDbContext context;
    public DatabaseInitializerDataProvider(
        AppDbContext context)
    {
        this.context = context;
    }

    public async Task<bool> DoesDatabaseExistAsync()
    {
        return await ((RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>()).ExistsAsync().ConfigureAwait(false);
    }

    public async Task MigrateAsync()
    {
        await context.Database.MigrateAsync().ConfigureAwait(false);
    }

    public async Task<bool> IsEmptyDatabaseAsync()
    {
        var conn = context.Database.GetDbConnection();
        if (conn.State.Equals(ConnectionState.Closed)) await conn.OpenAsync();
        using var command = conn.CreateCommand();
        command.CommandText = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME <> N'__EFMigrationsHistory' and TABLE_SCHEMA <> 'sys'";
        var scalarResult = await command.ExecuteScalarAsync();
        return scalarResult == null;
    }
}
