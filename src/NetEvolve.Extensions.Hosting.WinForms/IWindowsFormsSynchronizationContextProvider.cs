namespace NetEvolve.Extensions.Hosting.WinForms;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Forms;

public interface IWindowsFormsSynchronizationContextProvider
{
    WindowsFormsSynchronizationContext Context { get; }

    void Invoke([NotNull] Action action);

    [return: MaybeNull]
    TResult Invoke<TResult>([NotNull] Func<TResult> action);

    [return: MaybeNull]
    TResult Invoke<TResult, TInput>([NotNull] Func<TInput, TResult> action, TInput input);

    ValueTask InvokeAsync([NotNull] Action action);

    ValueTask<TResult> InvokeAsync<TResult>([NotNull] Func<TResult> action);

    ValueTask<TResult> InvokeAsync<TResult, TInput>(
        [NotNull] Func<TInput, TResult> action,
        TInput input
    );
}
