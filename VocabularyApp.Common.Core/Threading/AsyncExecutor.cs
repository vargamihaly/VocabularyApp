using Microsoft.Extensions.DependencyInjection;

namespace VocabularyApp.Common.Core.Threading;

public interface IAsyncExecutor
{
    Task Run<TIn>(Func<TIn, Task> func) where TIn : notnull;

    Task<TOut> Run<TIn, TOut>(Func<TIn, Task<TOut>> func) where TIn : notnull;

    Task RunOptional<TIn>(Func<TIn?, Task> func);

    Task<TOut> RunOptional<TIn, TOut>(Func<TIn?, Task<TOut>> func);
}

public class AsyncExecutor : IAsyncExecutor, IScoped
{
    private readonly IServiceProvider serviceProvider;

    public AsyncExecutor(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task Run<TIn>(Func<TIn, Task> func) where TIn : notnull
    {
        await Task.Run(async () =>
        {
            using var scope = serviceProvider.CreateScope();
            TIn service = scope.ServiceProvider.GetRequiredService<TIn>();
            await func(service);
        });
    }

    public async Task<TOut> Run<TIn, TOut>(Func<TIn, Task<TOut>> func) where TIn : notnull
    {
        var task = Task.Run(async () =>
        {
            using var scope = serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TIn>();
            var res = await func(service);
            return res;
        });
        return await task;
    }

    public async Task RunOptional<TIn>(Func<TIn?, Task> func)
    {
        await Task.Run(async () =>
        {
            using var scope = serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetService<TIn>();
            await func(service);
        });
    }

    public async Task<TOut> RunOptional<TIn, TOut>(Func<TIn?, Task<TOut>> func)
    {
        var task = Task.Run(async () =>
        {
            using var scope = serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetService<TIn>();
            var res = await func(service);
            return res;
        });
        return await task;
    }
}
