namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Integration;

using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using global::TUnit.Core.Executors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

[TestExecutor<STAThreadExecutor>]
public partial class IHostBuilderExtensionsTests
{
    [Test]
    public async Task UseWindowsForms_IHostBuilder_StartForm_ConfigureNull_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var host = Host.CreateDefaultBuilder().UseWindowsForms<TestForm>().Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken).ConfigureAwait(false);

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15, cancellationToken: cancellationToken).ConfigureAwait(false);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    [Test]
    public async Task UseWindowsForms_IHostBuilder_StartForm_ConfigureFine_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var host = Host.CreateDefaultBuilder()
            .UseWindowsForms<TestForm>(options =>
            {
                options.EnableConsoleShutdown = true;
                options.EnableVisualStyles = false;
            })
            .Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken).ConfigureAwait(false);

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15, cancellationToken: cancellationToken).ConfigureAwait(false);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    [Test]
    public async Task UseWindowsForms_IHostBuilder_ApplicationContext_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var host = Host.CreateDefaultBuilder().UseWindowsForms<TestApplicationContext>().Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15, cancellationToken: cancellationToken).ConfigureAwait(false);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    [Test]
    public async Task UseWindowsForms_IHostBuilder_ApplicationContextFactory_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var host = Host.CreateDefaultBuilder()
            .UseWindowsForms(sp => new TestApplicationContext(new TestForm()))
            .Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15, cancellationToken: cancellationToken).ConfigureAwait(false);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    [Test]
    public async Task UseWindowsForms_IHostBuilder_AdvancedFactory_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var host = Host.CreateDefaultBuilder()
            .UseWindowsForms<TestApplicationContext, TestForm>((sp, form) => new TestApplicationContext(form))
            .Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15, cancellationToken: cancellationToken).ConfigureAwait(false);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

#if NET7_0_OR_GREATER
    [Test]
    public async Task UseWindowsForms_HostApplicationBuilder_StartForm_ConfigureNull_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        var builder = Host.CreateApplicationBuilder();
        _ = builder.UseWindowsForms<TestForm>();
        using var host = builder.Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15, cancellationToken: cancellationToken).ConfigureAwait(false);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    [Test]
    public async Task UseWindowsForms_HostApplicationBuilder_StartForm_ConfigureFine_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        var builder = Host.CreateApplicationBuilder();
        _ = builder.UseWindowsForms<TestForm>(options =>
        {
            options.EnableConsoleShutdown = true;
            options.EnableVisualStyles = false;
        });
        using var host = builder.Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15, cancellationToken: cancellationToken).ConfigureAwait(false);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    [Test]
    public async Task UseWindowsForms_HostApplicationBuilder_ApplicationContext_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        var builder = Host.CreateApplicationBuilder();
        _ = builder.UseWindowsForms<TestApplicationContext>();
        using var host = builder.Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15, cancellationToken: cancellationToken).ConfigureAwait(false);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    [Test]
    public async Task UseWindowsForms_HostApplicationBuilder_ApplicationContextFactory_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        var builder = Host.CreateApplicationBuilder();
        _ = builder.UseWindowsForms(sp => new TestApplicationContext(new TestForm()));
        using var host = builder.Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15, cancellationToken: cancellationToken).ConfigureAwait(false);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    [Test]
    public async Task UseWindowsForms_HostApplicationBuilder_AdvancedFactory_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        var builder = Host.CreateApplicationBuilder();
        _ = builder.UseWindowsForms<TestApplicationContext, TestForm>((sp, form) => new TestApplicationContext(form));
        using var host = builder.Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15, cancellationToken: cancellationToken).ConfigureAwait(false);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }
#endif

    [Test]
    public async Task UseWindowsForms_IHostBuilder_StartForm_WithDefaultFont_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var font = new Font("Arial", 10f);
        using var host = Host.CreateDefaultBuilder()
            .UseWindowsForms<TestForm>(options =>
            {
                options.DefaultFont = font;
            })
            .Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15, cancellationToken: cancellationToken).ConfigureAwait(false);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    [Test]
    public async Task UseWindowsForms_IHostBuilder_StartForm_WithPreloadAction_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        var preloadInvoked = false;

        using var host = Host.CreateDefaultBuilder()
            .UseWindowsForms<TestForm>(options =>
            {
                options.PreloadAction = (_, _) => preloadInvoked = true;
            })
            .Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15, cancellationToken: cancellationToken).ConfigureAwait(false);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();
        _ = await Assert.That(preloadInvoked).IsTrue();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

#if NET7_0_OR_GREATER
    [Test]
    public async Task UseWindowsForms_HostApplicationBuilder_StartForm_WithDefaultFont_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var font = new Font("Arial", 10f);
        var builder = Host.CreateApplicationBuilder();
        _ = builder.UseWindowsForms<TestForm>(options =>
        {
            options.DefaultFont = font;
        });
        using var host = builder.Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        await WaitForHandleCreatedAsync(mainForm, cancellationToken).ConfigureAwait(false);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    [Test]
    public async Task UseWindowsForms_HostApplicationBuilder_StartForm_WithPreloadAction_Expected(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        var preloadInvoked = false;

        var builder = Host.CreateApplicationBuilder();
        _ = builder.UseWindowsForms<TestForm>(options =>
        {
            options.PreloadAction = (_, _) => preloadInvoked = true;
        });
        using var host = builder.Build();

        await host.StartAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        await WaitForHandleCreatedAsync(mainForm, cancellationToken).ConfigureAwait(false);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();
        _ = await Assert.That(preloadInvoked).IsTrue();

        await host.StopAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }
#endif

    /// <summary>
    /// Waits until <paramref name="form"/>'s window handle has been created by the WinForms UI thread.
    /// Polling <see cref="Control.IsHandleCreated"/> directly from another thread is racy: the property
    /// can observe the handle as created while <c>Control.CreateHandle()</c> is still on the stack (its
    /// internal "creating handle" guard is only cleared once <c>CreateHandle()</c> returns), so code that
    /// reacts immediately - e.g. disposing the host - can hit
    /// "InvalidOperationException: Value Dispose() cannot be called while doing CreateHandle()", as seen
    /// on this repository's Windows CI runner. Waiting on <see cref="Control.HandleCreated"/> with a
    /// continuation that always resumes asynchronously (a thread-pool hop) lets that call stack unwind
    /// before the awaiting test continues, and a bounded timeout turns a stalled UI thread into a clear,
    /// fast failure instead of the test runner's default 5-minute timeout.
    /// </summary>
    private static async Task WaitForHandleCreatedAsync(
        Form form,
        CancellationToken cancellationToken,
        int timeoutSeconds = 30
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (form.IsHandleCreated)
        {
            return;
        }

        var handleCreated = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        void OnHandleCreated(object? sender, EventArgs e) => handleCreated.TrySetResult(true);

        form.HandleCreated += OnHandleCreated;
        try
        {
            // Re-check after subscribing, in case the handle was created between the first check and now.
            if (form.IsHandleCreated)
            {
                return;
            }

            using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);
            using var registration = linkedCts.Token.Register(() => handleCreated.TrySetCanceled(linkedCts.Token));

            try
            {
                _ = await handleCreated.Task.ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested)
            {
                throw new TimeoutException(
                    $"The handle for form '{form.GetType().Name}' was not created within {timeoutSeconds} seconds."
                );
            }
        }
        finally
        {
            form.HandleCreated -= OnHandleCreated;
        }
    }

#pragma warning disable CA1812
    private sealed class TestApplicationContext : ApplicationContext
    {
        public TestApplicationContext()
#pragma warning disable CA2000 // Dispose objects before losing scope
            : this(new TestForm()) { }
#pragma warning restore CA2000 // Dispose objects before losing scope

        public TestApplicationContext(Form form)
            : base(form) { }
    }

    private sealed class TestForm : Form
    {
        public TestForm() => Load += (_, _) => Visible = false;
    }
#pragma warning restore CA1812
}
