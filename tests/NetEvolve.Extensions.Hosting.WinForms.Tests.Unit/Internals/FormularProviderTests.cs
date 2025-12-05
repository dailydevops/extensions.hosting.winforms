namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Unit.Internals;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using NetEvolve.Extensions.Hosting.WinForms.Internals;
using static System.Formats.Asn1.AsnWriter;

public partial class FormularProviderTests
{
    [Test]
    public async Task GetFormular_EverythingFine_Expected()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>();
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);

        // Act
#pragma warning disable CA1849, S6966, VSTHRD103 // Call async methods when in an async method
        using var resultForm = formularProvider.GetFormular<TestFormFine>();
#pragma warning restore CA1849, S6966, VSTHRD103 // Call async methods when in an async method

        // Assert
        _ = await Assert.That(resultForm).IsNotNull();
    }

    [Test]
    public void GetFormular_InvalidForm_ThrowsInvalidOperationException()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>();
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);

        // Act / Assert
        _ = Assert.Throws<InvalidOperationException>(() => formularProvider.GetFormular<TestFormNotRegistered>());
    }

    [Test]
    public async Task GetFormularAsync_EverythingFine_Expected()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>();
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);

        // Act
        using var resultForm = await formularProvider.GetFormularAsync<TestFormFine>();

        // Assert
        _ = await Assert.That(resultForm).IsNotNull();
    }

    [Test]
    public async Task GetFormularAsync_InvalidForm_ThrowsInvalidOperationException()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>();
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);

        // Act / Assert
        _ = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await formularProvider.GetFormularAsync<TestFormNotRegistered>()
        );
    }

    [Test]
    public async Task GetScopedForm_EverythingFine_Expected()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>();
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);

        // Act
#pragma warning disable CA1849, S6966, VSTHRD103 // Call async methods when in an async method
        using var resultForm = formularProvider.GetScopedForm<TestFormFine>();
#pragma warning restore CA1849, S6966, VSTHRD103 // Call async methods when in an async method

        // Assert
        _ = await Assert.That(resultForm).IsNotNull();
    }

    [Test]
    public void GetScopedForm_InvalidForm_ThrowsInvalidOperationException()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>();
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);

        // Act / Assert
        _ = Assert.Throws<InvalidOperationException>(() => formularProvider.GetScopedForm<TestFormNotRegistered>());
    }

    [Test]
    public async Task GetScopedFormAsync_EverythingFine_Expected()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>();
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);

        // Act
        using var resultForm = await formularProvider.GetScopedFormAsync<TestFormFine>();

        // Assert
        _ = await Assert.That(resultForm).IsNotNull();
    }

    [Test]
    public async Task GetScopedFormAsync_InvalidForm_ThrowsInvalidOperationException()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>();
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);

        // Act / Assert
        _ = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await formularProvider.GetScopedFormAsync<TestFormNotRegistered>()
        );
    }

    [Test]
    public async Task GetScopedForm_WithScope_EverythingFine_Expected()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>();
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);
        using var scope = serviceProvider.CreateScope();

        // Act
#pragma warning disable CA1849, S6966, VSTHRD103 // Call async methods when in an async method
        using var resultForm = formularProvider.GetScopedForm<TestFormFine>(scope);
#pragma warning restore CA1849, S6966, VSTHRD103 // Call async methods when in an async method

        // Assert
        _ = await Assert.That(resultForm).IsNotNull();
    }

    [Test]
    public void GetScopedForm_WithScope_InvalidForm_ThrowsInvalidOperationException()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>();
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);
        using var scope = serviceProvider.CreateScope();

        // Act / Assert
        _ = Assert.Throws<InvalidOperationException>(() =>
            formularProvider.GetScopedForm<TestFormNotRegistered>(scope)
        );
    }

    [Test]
    public async Task GetScopedFormAsync_WithScope_EverythingFine_Expected()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>();
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);
        using var scope = serviceProvider.CreateScope();

        // Act
        using var resultForm = await formularProvider.GetScopedFormAsync<TestFormFine>(scope);

        // Assert
        _ = await Assert.That(resultForm).IsNotNull();
    }

    [Test]
    public async Task GetScopedFormAsync_WithScope_InvalidForm_ThrowsInvalidOperationException()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>();
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);
        using var scope = serviceProvider.CreateScope();

        // Act / Assert
        _ = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await formularProvider.GetScopedFormAsync<TestFormNotRegistered>(scope)
        );
    }

    [Test]
    public async Task GetMainFormular_EverythingFine_Expected()
    {
        // Arrange
        var services = new ServiceCollection()
            .AddScoped<TestFormFine>()
            .AddSingleton(sp => new ApplicationContext(sp.GetRequiredService<TestFormFine>()));
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);

        // Act
#pragma warning disable CA1849, S6966, VSTHRD103 // Call async methods when in an async method
        using var resultForm = formularProvider.GetMainFormular();
#pragma warning restore CA1849, S6966, VSTHRD103 // Call async methods when in an async method

        // Assert
        _ = await Assert.That(resultForm).IsNotNull();
    }

    [Test]
    public void GetMainFormular_InvalidForm_ThrowsInvalidOperationException()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>().AddSingleton(sp => new ApplicationContext());
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);

        // Act / Assert
        _ = Assert.Throws<InvalidOperationException>(() => formularProvider.GetMainFormular());
    }

    [Test]
    public async Task GetMainFormularAsync_EverythingFine_Expected()
    {
        // Arrange
        var services = new ServiceCollection()
            .AddScoped<TestFormFine>()
            .AddSingleton(sp => new ApplicationContext(sp.GetRequiredService<TestFormFine>()));
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);

        // Act
        using var resultForm = await formularProvider.GetMainFormularAsync();

        // Assert
        _ = await Assert.That(resultForm).IsNotNull();
    }

    [Test]
    public async Task GetMainFormularAsync_InvalidForm_ThrowsInvalidOperationException()
    {
        // Arrange
        var services = new ServiceCollection().AddScoped<TestFormFine>().AddSingleton(sp => new ApplicationContext());
        var serviceProvider = services.BuildServiceProvider();
        var synchronizationContext = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!,
        };
        using var formularProvider = new FormularProvider(serviceProvider, synchronizationContext);

        // Act / Assert
        _ = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await formularProvider.GetMainFormularAsync()
        );
    }

#pragma warning disable CA1812
#pragma warning disable S2094 // Classes should not be empty
    private sealed class TestFormFine : Form { }

    private sealed class TestFormNotRegistered : Form { }
#pragma warning restore S2094 // Classes should not be empty
#pragma warning restore CA1812
}
