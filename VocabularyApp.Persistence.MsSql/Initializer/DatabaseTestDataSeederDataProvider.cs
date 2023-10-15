using VocabularyApp.Application.Entities;
using VocabularyApp.Initializer.DataProviders;

namespace VocabularyApp.Persistence.MsSql.Initializer;

public class DatabaseTestDataSeederDataProvider : IDatabaseTestDataSeederDataProvider
{
    private readonly AppDbContext context;

    public DatabaseTestDataSeederDataProvider(AppDbContext context)
    {
        this.context = context;
    }

    public async Task PersistAsync(IEnumerable<Word> words)
    {
        ArgumentNullException.ThrowIfNull(words);

        foreach (var word in words)
        {
            var wordEntity = new Persistence.MsSql.Entities.Word()
            {
                WordTitle = word.WordTitle,
                Description = word.Description,
            };

            context.Words.Add(wordEntity);
        }

        await context.SaveChangesAsync();
    }
}
