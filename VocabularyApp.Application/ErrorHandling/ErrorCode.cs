namespace VocabularyApp.Application.ErrorHandling;

public enum ErrorCode
{
    [ErrorType(ErrorType.ForbiddenAction)]
    Forbidden,

    [ErrorType(ErrorType.ApplicationError)]
    UnexpectedError,

    [ErrorType(ErrorType.ApplicationError)]
    UnknownError,

    [ErrorType(ErrorType.ApplicationError)]
    ConfigurationError,

    [ErrorType(ErrorType.InvalidAction)]
    WordDoesntExist,
    
    [ErrorType(ErrorType.ApplicationError)]
    NoWordsInDatabase,
}
