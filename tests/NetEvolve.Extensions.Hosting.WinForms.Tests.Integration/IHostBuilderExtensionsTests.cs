namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Integration;

using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using global::TUnit.Core.Executors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

[TestExecutor<STAThreadExecutor>]
public partial class IHostBuilderExtensionsTests
{
    [Test]
    public async Task UseWindowsForms_IHostBuilder_StartForm_ConfigureNull_Expected()
    {
        using var host = Host.CreateDefaultBuilder().UseWindowsForms<TestForm>().Build();

        await host.StartAsync();

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync();

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync();
    }

    [Test]
    public async Task UseWindowsForms_IHostBuilder_StartForm_ConfigureFine_Expected()
    {
        using var host = Host.CreateDefaultBuilder()
            .UseWindowsForms<TestForm>(options =>
            {
                options.EnableConsoleShutdown = true;
                options.EnableVisualStyles = false;
            })
            .Build();

        await host.StartAsync();

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync();

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync();
    }

    [Test]
    public async Task UseWindowsForms_IHostBuilder_ApplicationContext_Expected()
    {
        using var host = Host.CreateDefaultBuilder().UseWindowsForms<TestApplicationContext>().Build();

        await host.StartAsync();

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync();

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync();
    }

    [Test]
    public async Task UseWindowsForms_IHostBuilder_ApplicationContextFactory_Expected()
    {
        using var host = Host.CreateDefaultBuilder()
            .UseWindowsForms(sp => new TestApplicationContext(new TestForm()))
            .Build();

        await host.StartAsync();

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync();

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync();
    }

    [Test]
    public async Task UseWindowsForms_IHostBuilder_AdvancedFactory_Expected()
    {
        using var host = Host.CreateDefaultBuilder()
            .UseWindowsForms<TestApplicationContext, TestForm>((sp, form) => new TestApplicationContext(form))
            .Build();

        await host.StartAsync();

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync();

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync();
    }

#if NET7_0_OR_GREATER
    [Test]
    public async Task UseWindowsForms_HostApplicationBuilder_StartForm_ConfigureNull_Expected()
    {
        var builder = Host.CreateApplicationBuilder();
        _ = builder.UseWindowsForms<TestForm>();
        using var host = builder.Build();

        await host.StartAsync();

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync();

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync();
    }

    [Test]
    public async Task UseWindowsForms_HostApplicationBuilder_StartForm_ConfigureFine_Expected()
    {
        var builder = Host.CreateApplicationBuilder();
        _ = builder.UseWindowsForms<TestForm>(options =>
        {
            options.EnableConsoleShutdown = true;
            options.EnableVisualStyles = false;
        });
        using var host = builder.Build();

        await host.StartAsync();

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync();

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync();
    }

    [Test]
    public async Task UseWindowsForms_HostApplicationBuilder_ApplicationContext_Expected()
    {
        var builder = Host.CreateApplicationBuilder();
        _ = builder.UseWindowsForms<TestApplicationContext>();
        using var host = builder.Build();

        await host.StartAsync();

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync();

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync();
    }

    [Test]
    public async Task UseWindowsForms_HostApplicationBuilder_ApplicationContextFactory_Expected()
    {
        var builder = Host.CreateApplicationBuilder();
        _ = builder.UseWindowsForms(sp => new TestApplicationContext(new TestForm()));
        using var host = builder.Build();

        await host.StartAsync();

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync();

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync();
    }

    [Test]
    public async Task UseWindowsForms_HostApplicationBuilder_AdvancedFactory_Expected()
    {
        var builder = Host.CreateApplicationBuilder();
        _ = builder.UseWindowsForms<TestApplicationContext, TestForm>((sp, form) => new TestApplicationContext(form));
        using var host = builder.Build();

        await host.StartAsync();

        var provider = host.Services.GetService<IFormularProvider>()!;
        var mainForm = await provider.GetMainFormularAsync();

        do
        {
            // This test runs too fast for the handle to be created.
            // Therefore, we have to slow down a little.
            await Task.Delay(15);
        } while (!mainForm.IsHandleCreated);

        _ = await Assert.That(mainForm).IsNotNull();
        _ = await Assert.That(mainForm).IsTypeOf<TestForm>();

        await host.StopAsync();
    }
#endif

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
