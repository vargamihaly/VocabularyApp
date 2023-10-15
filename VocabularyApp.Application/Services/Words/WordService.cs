using VocabularyApp.Application.Entities;
using VocabularyApp.Common.Core;

namespace VocabularyApp.Application.Services.Words;

public interface IWordService : IScoped
{
    Task<IEnumerable<Word>> GetAllWordsAsync();
    
    Task<bool> DoesWordExistAsync(string wordTitle);
    
}

public class WordService : IWordService
{
    private readonly IWordServiceDataProvider dataProvider;

    public WordService(IWordServiceDataProvider dataProvider)
    {
        this.dataProvider = dataProvider;
    }

    public Task<IEnumerable<Word>> GetAllWordsAsync()
    {
        return dataProvider.GetAllWordsAsync();
    }

    public Task<bool> DoesWordExistAsync(string wordTitle)
    {
        return dataProvider.DoesWordExistAsync(wordTitle);
    }
}
