namespace VocabularyApp.Common.Core.Transactions;

public interface ITransactionExecutor
{
    Task TransactionAsync(Func<Task> func);
}
