namespace NetEvolve.Extensions.Hosting.WinForms.Internals;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <inheritdoc />
internal sealed class WindowsFormsSynchronizationContextProvider
    : IWindowsFormsSynchronizationContextProvider
{
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
    public async ValueTask InvokeAsync(
        [NotNull] Action action,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(action);

        var tcs = new TaskCompletionSource();
        Context.Post(
            delegate
            {
                try
                {
                    action();
                    tcs.SetResult();
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            },
            tcs
        );

        await tcs.Task.WaitAsync(cancellationToken).ConfigureAwait(true);
    }

    /// <inheritdoc/>
    public async ValueTask<TResult> InvokeAsync<TResult>(
        [NotNull] Func<TResult> action,
        CancellationToken cancellationToken = default
    )
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
        return await tcs.Task.WaitAsync(cancellationToken).ConfigureAwait(true);
    }

    /// <inheritdoc/>
    public async ValueTask<TResult> InvokeAsync<TResult, TInput>(
        [NotNull] Func<TInput, TResult> action,
        TInput input,
        CancellationToken cancellationToken = default
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
        return await tcs.Task.WaitAsync(cancellationToken).ConfigureAwait(true);
    }
}
