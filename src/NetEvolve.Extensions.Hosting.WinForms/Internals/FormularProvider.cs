namespace NetEvolve.Extensions.Hosting.WinForms.Internals;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

internal sealed class FormularProvider(
    IServiceProvider serviceProvider,
    IWindowsFormsSynchronizationContextProvider synchronizationContext
) : IFormularProvider, IDisposable
{
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    private bool _disposedValue;

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _semaphore.Dispose();
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

    /// <inheritdoc />
    public T GetFormular<T>()
        where T : Form
    {
        _semaphore.Wait();
        try
        {
            var form = synchronizationContext.Invoke(() => serviceProvider.GetService<T>());
            ArgumentNullException.ThrowIfNull(form);
            return form;
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    /// <inheritdoc />
    public async ValueTask<T> GetFormularAsync<T>()
        where T : Form
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);
        try
        {
            var form = await synchronizationContext
                .InvokeAsync(() => serviceProvider.GetService<T>())
                .ConfigureAwait(false);
            ArgumentNullException.ThrowIfNull(form);
            return form;
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    /// <inheritdoc />
    public Form GetMainFormular()
    {
        var context = serviceProvider.GetService<ApplicationContext>();

        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(context.MainForm);

        return context.MainForm;
    }

    /// <inheritdoc />
    public ValueTask<Form> GetMainFormularAsync()
    {
        var context = serviceProvider.GetService<ApplicationContext>();

        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(context.MainForm);

        return new ValueTask<Form>(context.MainForm);
    }

    /// <inheritdoc />
    public async ValueTask<T> GetScopedFormAsync<T>()
        where T : Form
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);
        try
        {
            var form = await synchronizationContext
                .InvokeAsync(GetScopedForm<T>)
                .ConfigureAwait(false);
            return form;
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    /// <inheritdoc />
    public async ValueTask<T> GetScopedFormAsync<T>(IServiceScope scope)
        where T : Form
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);
        try
        {
            var form = await synchronizationContext
                .InvokeAsync(GetScopedForm<T>, scope)
                .ConfigureAwait(false);
            return form;
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    /// <inheritdoc />
    public T GetScopedForm<T>()
        where T : Form
    {
        var factory = serviceProvider.GetService<IServiceScopeFactory>();
        var scope = factory!.CreateScope();
        try
        {
            var form = scope.ServiceProvider.GetService<T>();

            ArgumentNullException.ThrowIfNull(form);

            form.Disposed += (_, _) => scope.Dispose();
            return form;
        }
        catch
        {
            scope.Dispose();
            throw;
        }
    }

    /// <inheritdoc />
    public T GetScopedForm<T>([NotNull] IServiceScope scope)
        where T : Form
    {
        ArgumentNullException.ThrowIfNull(scope);

        var form = scope.ServiceProvider.GetService<T>();

        ArgumentNullException.ThrowIfNull(form);

        return form;
    }
}
