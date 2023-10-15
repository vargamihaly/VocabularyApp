using Newtonsoft.Json;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace VocabularyApp.Application.ErrorHandling;

[Serializable]
public class VocabularyAppException : Exception
{
    protected VocabularyAppException()
    {
    }

    protected VocabularyAppException(string message)
        : base(message)
    {
    }

    protected VocabularyAppException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected VocabularyAppException(string? debugInfo, params object?[] debugObjects)
    : base(string.Empty, null)
    {
        DebugInfo = debugInfo;
        DebugObjectsString = StringifyDebugObjects(debugObjects);
    }

    protected VocabularyAppException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
    
    public VocabularyAppException(ErrorCode code, string? debugInfo = null, params object?[] debugObjects)
        : this(debugInfo, debugObjects)
    {
        Code = code;
    }

    public string? DebugInfo { get; }
    public string? DebugObjectsString { get; }
    public ErrorCode Code { get; }

    private string? StringifyDebugObjects(object?[]? debugObjects)
    {
        string? result = null;

        if (debugObjects != null && debugObjects.Any())
        {
            var stringBuilder = new StringBuilder();

            foreach (var debugObject in debugObjects.Where(o => o != null))
            {
                stringBuilder.AppendLine(CultureInfo.InvariantCulture, $"Object type: {debugObject!.GetType()}\nContents: {JsonConvert.SerializeObject(debugObject)}");
            }

            result = stringBuilder.ToString();
        }

        return result;
    }
}
