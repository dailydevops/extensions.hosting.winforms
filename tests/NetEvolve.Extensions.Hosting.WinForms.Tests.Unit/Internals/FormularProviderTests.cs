namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Unit.Internals;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using NetEvolve.Extensions.Hosting.WinForms.Internals;
using Xunit;

public class FormularProviderTests
{
    [Fact]
    public void GetFormular_EverythingFine_Expected()
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
        using var resultForm = formularProvider.GetFormular<TestFormFine>();

        // Assert
        Assert.NotNull(resultForm);
    }

    [Fact]
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
        _ = Assert.Throws<InvalidOperationException>(formularProvider.GetFormular<TestFormNotRegistered>);
    }

    [Fact]
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
        Assert.NotNull(resultForm);
    }

    [Fact]
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

    [Fact]
    public void GetScopedForm_EverythingFine_Expected()
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
        using var resultForm = formularProvider.GetScopedForm<TestFormFine>();

        // Assert
        Assert.NotNull(resultForm);
    }

    [Fact]
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
        _ = Assert.Throws<InvalidOperationException>(formularProvider.GetScopedForm<TestFormNotRegistered>);
    }

    [Fact]
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
        Assert.NotNull(resultForm);
    }

    [Fact]
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

    [Fact]
    public void GetScopedForm_WithScope_EverythingFine_Expected()
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
        using var resultForm = formularProvider.GetScopedForm<TestFormFine>(scope);

        // Assert
        Assert.NotNull(resultForm);
    }

    [Fact]
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

    [Fact]
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
        Assert.NotNull(resultForm);
    }

    [Fact]
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

    [Fact]
    public void GetMainFormular_EverythingFine_Expected()
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
        using var resultForm = formularProvider.GetMainFormular();

        // Assert
        Assert.NotNull(resultForm);
    }

    [Fact]
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
        _ = Assert.Throws<InvalidOperationException>(formularProvider.GetMainFormular);
    }

    [Fact]
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
        Assert.NotNull(resultForm);
    }

    [Fact]
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
