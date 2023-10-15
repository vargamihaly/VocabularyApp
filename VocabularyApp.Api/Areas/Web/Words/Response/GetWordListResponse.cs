namespace VocabularyApp.Api.Areas.Web.Words.Response;

public class GetWordListResponse
{
    public int Count => WordList.Count;

    public List<GetWordListItemResponse> WordList { get; set; } = new();
}

public class GetWordListItemResponse
{
    public string WordTitle { get; set; }
    public string Description { get; set; }
}
