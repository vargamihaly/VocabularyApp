using VocabularyApp.Application.Entities;

namespace VocabularyApp.Initializer.DataProviders;

public interface IDatabaseTestDataSeederDataProvider
{
    Task PersistAsync(IEnumerable<Word> words);
}
