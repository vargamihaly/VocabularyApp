using VocabularyApp.Application.Entities;

namespace VocabularyApp.Application.Services.Words;

public interface IWordServiceDataProvider
{
    
    public Task<bool> DoesWordExistAsync(string id);
    public Task<IEnumerable<Word>> GetAllWordsAsync();
}
