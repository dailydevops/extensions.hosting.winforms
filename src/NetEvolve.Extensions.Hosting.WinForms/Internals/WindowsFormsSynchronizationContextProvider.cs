namespace NetEvolve.Extensions.Hosting.WinForms.Internals;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Forms;

internal sealed class WindowsFormsSynchronizationContextProvider
    : IWindowsFormsSynchronizationContextProvider,
        IDisposable
{
    private bool _disposedValue;

    internal WindowsFormsSynchronizationContext Context { get; set; } = default!;

    /// <inheritdoc/>
    public void Invoke([NotNull] Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        Context.Send(
            delegate
            {
                action();
            },
            null
        );
    }

    /// <inheritdoc/>
    [return: MaybeNull]
    public TResult Invoke<TResult>([NotNull] Func<TResult> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        TResult result = default!;
        Context.Send(
            delegate
            {
                result = action();
            },
            null
        );
        return result;
    }

    /// <inheritdoc/>
    [return: MaybeNull]
    public TResult Invoke<TResult, TInput>([NotNull] Func<TInput, TResult> action, TInput input)
    {
        ArgumentNullException.ThrowIfNull(action);

        TResult result = default!;
        Context.Send(
            delegate
            {
                result = action(input);
            },
            null
        );
        return result;
    }

    /// <inheritdoc/>
    public async ValueTask InvokeAsync([NotNull] Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        var tcs = new TaskCompletionSource();
        Context.Post(
            delegate
            {
                try
                {
                    tcs.SetResult();
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            },
            tcs
        );

        await tcs.Task.ConfigureAwait(true);
    }

    /// <inheritdoc/>
    public async ValueTask<TResult> InvokeAsync<TResult>([NotNull] Func<TResult> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        var tcs = new TaskCompletionSource<TResult>();
        Context.Post(
            delegate
            {
                try
                {
                    var result = action();
                    tcs.SetResult(result);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            },
            tcs
        );
        return await tcs.Task.ConfigureAwait(true);
    }

    /// <inheritdoc/>
    public async ValueTask<TResult> InvokeAsync<TResult, TInput>(
        [NotNull] Func<TInput, TResult> action,
        TInput input
    )
    {
        ArgumentNullException.ThrowIfNull(action);

        var tcs = new TaskCompletionSource<TResult>();
        Context.Post(
            delegate
            {
                try
                {
                    var result = action(input);
                    tcs.SetResult(result);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            },
            tcs
        );
        return await tcs.Task.ConfigureAwait(true);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                Context.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
