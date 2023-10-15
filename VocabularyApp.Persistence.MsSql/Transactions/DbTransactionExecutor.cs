using VocabularyApp.Common.Core.Transactions;

namespace VocabularyApp.Persistence.MsSql.Transactions;

public class DbTransactionExecutor : ITransactionExecutor
{
    private readonly AppDbContext context;

    public DbTransactionExecutor(AppDbContext context)
    {
        this.context = context;
    }

    public async Task TransactionAsync(Func<Task> func)
    {
        ArgumentNullException.ThrowIfNull(func);

        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            await func.Invoke();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
