namespace VocabularyApp.Common.Core.Extensions;

public static class CollectionExtensions
{
    public static IEnumerable<T> AsEnumerable<T>(this T sourceObject) => sourceObject.AsList();

    public static List<T> AsList<T>(this T sourceObject) => new() { sourceObject };
}
