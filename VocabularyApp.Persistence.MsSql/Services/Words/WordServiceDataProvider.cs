using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VocabularyApp.Application.Entities;
using VocabularyApp.Application.ErrorHandling;
using VocabularyApp.Application.Services.Words;

namespace VocabularyApp.Persistence.MsSql.Services.Words;

public class WordServiceDataProvider : IWordServiceDataProvider
{
    private readonly AppDbContext context;
    private readonly IMapper mapper;

    public WordServiceDataProvider(AppDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<int> DeleteThingAsync(int id)
    {
        /*
            For the sake of performance, ExecuteSqlRawAsync is used here instead of querying and deleting the rows via linq.

            ExecuteSqlRawAsync method executes the given SQL against the database and returns the number of rows affected. If the record specified by the id parameter does not exists, it simply returns 0 for the affected rows.

            Note that this method does not start a transaction. To use this method with a transaction, first call Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions.BeginTransaction(Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade,System.Data.IsolationLevel) or UseTransaction. Note that the current Microsoft.EntityFrameworkCore.Storage.ExecutionStrategy is not used by this method since the SQL may not be idempotent and does not run in a transaction. An Microsoft.EntityFrameworkCore.Storage.ExecutionStrategy can be used explicitly, making sure to also use a transaction if the SQL is not idempotent.

            As with any API that accepts SQL it is important to parameterize any user input to protect against a SQL injection attack. You can include parameter place holders in the SQL query string and then supply parameter values as additional arguments.
        */

        return await context.Database.ExecuteSqlRawAsync($"DELETE FROM [dbo].[{nameof(context.Words)}] WHERE [{nameof(Entities.Word.WordTitle)}] = @p0", id);
    }

    public async Task<bool> DoesWordExistAsync(string wordTitle)
    {
        if (string.IsNullOrEmpty(wordTitle)) return false;

        return await context.Words.AnyAsync(x => x.WordTitle == wordTitle);
    }

    public async Task<IEnumerable<Word>> GetAllWordsAsync()
    {
        try
        {
            var wordEntities = await context.Words.ToListAsync();

            if (wordEntities.Count == 0)
            {
                throw new VocabularyAppException(ErrorCode.NoWordsInDatabase);
            }

            return mapper.Map<IEnumerable<Word>>(wordEntities);
        }
        catch (VocabularyAppException ex)
        {
            // Log the exception or perform any necessary actions
            Console.WriteLine("Exception occurred: " + ex.Message);
            // You can choose to continue processing or return a default response
            return Enumerable.Empty<Word>(); // Return an empty collection or handle it as needed
        }
    }
}

