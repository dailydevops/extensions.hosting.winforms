namespace NetEvolve.Extensions.Hosting.WinForms;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

/// <summary>
/// Allows to invoke actions on the Windows Forms synchronization context.
/// </summary>
public interface IWindowsFormsSynchronizationContextProvider
{
    /// <summary>
    /// Invokes the specified action on the Windows Forms synchronization context.
    /// </summary>
    /// <param name="action">The action to be performed.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is <see langword="null"/>.</exception>
    void Invoke([NotNull] Action action);

    /// <summary>
    /// Invokes the specified action on the Windows Forms synchronization context.
    /// </summary>
    /// <typeparam name="TResult">The expected return type.</typeparam>
    /// <param name="action">The action to be performed.</param>
    /// <returns>The result of the specified action.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is <see langword="null"/>.</exception>
    [return: MaybeNull]
    TResult Invoke<TResult>([NotNull] Func<TResult> action);

    /// <summary>
    /// Invokes the specified action on the Windows Forms synchronization context.
    /// </summary>
    /// <typeparam name="TResult">The expected return type.</typeparam>
    /// <typeparam name="TInput">The input type to be passed, which is to be processed.</typeparam>
    /// <param name="action">The action to be performed.</param>
    /// <param name="input">The specified input.</param>
    /// <returns>The result of the specified action.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is <see langword="null"/>.</exception>
    [return: MaybeNull]
    TResult Invoke<TResult, TInput>([NotNull] Func<TInput, TResult> action, TInput input);

    /// <summary>
    /// Invokes the specified asynchronous action on the Windows Forms synchronization context.
    /// </summary>
    /// <param name="action">The action to be performed.</param>
    /// <returns>The result of the specified action.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is <see langword="null"/>.</exception>
    ValueTask InvokeAsync([NotNull] Action action);

    /// <summary>
    /// Invokes the specified asynchronous action on the Windows Forms synchronization context.
    /// </summary>
    /// <typeparam name="TResult">The expected return type.</typeparam>
    /// <param name="action">The action to be performed.</param>
    /// <returns>The result of the specified action.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is <see langword="null"/>.</exception>
    ValueTask<TResult> InvokeAsync<TResult>([NotNull] Func<TResult> action);

    /// <summary>
    /// Invokes the specified asynchronous action on the Windows Forms synchronization context.
    /// </summary>
    /// <typeparam name="TResult">The expected return type.</typeparam>
    /// <typeparam name="TInput">The input type to be passed, which is to be processed.</typeparam>
    /// <param name="action">The action to be performed.</param>
    /// <param name="input">The specified input.</param>
    /// <returns>The result of the specified action.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is <see langword="null"/>.</exception>
    ValueTask<TResult> InvokeAsync<TResult, TInput>(
        [NotNull] Func<TInput, TResult> action,
        TInput input
    );
}
