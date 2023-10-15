using System.Reflection;

namespace VocabularyApp.Common.Core;

public static class TypeExtensions
{
    public static Dictionary<string, string?> GetFieldValues(this Type type)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));

        return type
                  .GetFields(BindingFlags.Public | BindingFlags.Static)
                  .Where(f => f.FieldType == typeof(string))
                  .ToDictionary(f => f.Name, f => f.GetValue(null)?.ToString());
    }
}
