namespace NetEvolve.Extensions.Hosting.WinForms.Internals;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

/// <inheritdoc />
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
            return serviceProvider.GetRequiredService<T>();
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
            return await synchronizationContext
                .InvokeAsync(serviceProvider.GetRequiredService<T>)!
                .ConfigureAwait(false);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    /// <inheritdoc />
    public Form GetMainFormular()
    {
        var context = serviceProvider.GetRequiredService<ApplicationContext>();

        if (context.MainForm is null)
        {
            throw new InvalidOperationException("The main form is not set.");
        }

        return context.MainForm;
    }

    /// <inheritdoc />
    public ValueTask<Form> GetMainFormularAsync()
    {
        var context = serviceProvider.GetRequiredService<ApplicationContext>();

        if (context.MainForm is null)
        {
            throw new InvalidOperationException("The main form is not set.");
        }

        return new ValueTask<Form>(context.MainForm);
    }

    /// <inheritdoc />
    public T GetScopedForm<T>()
        where T : Form
    {
        var factory = serviceProvider.GetService<IServiceScopeFactory>();
        var scope = factory!.CreateScope();
        try
        {
            var form = scope.ServiceProvider.GetRequiredService<T>();
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

        var form = scope.ServiceProvider.GetRequiredService<T>();
        return form;
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
        ArgumentNullException.ThrowIfNull(scope);
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
}
