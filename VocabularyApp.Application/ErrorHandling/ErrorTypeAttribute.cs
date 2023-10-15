namespace VocabularyApp.Application.ErrorHandling;

public enum ErrorType
{
    ForbiddenAction,
    InvalidAction,
    ApplicationError,
}

[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public sealed class ErrorTypeAttribute : Attribute
{
    public ErrorTypeAttribute(ErrorType errorType)
    {
        ErrorType = errorType;
    }

    public ErrorType ErrorType { get; }
}
