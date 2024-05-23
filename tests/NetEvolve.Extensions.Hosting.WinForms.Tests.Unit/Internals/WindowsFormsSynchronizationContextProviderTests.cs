namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Unit.Internals;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetEvolve.Extensions.Hosting.WinForms.Internals;
using Xunit;

public class WindowsFormsSynchronizationContextProviderTests
{
    [Fact]
    public void Invoke_ActionNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        _ = Assert.Throws<ArgumentNullException>("action", () => provider.Invoke(null!));
    }

    [Fact]
    public void Invoke_Action_ContextNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        _ = Assert.Throws<ArgumentNullException>("Context", () => provider.Invoke(() => { }));
    }

    [Fact]
    public void Invoke_Action_Expected()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!
        };

        // Act / Assert
        provider.Invoke(() => { });

        Assert.True(true, "No exception was thrown.");
    }

    [Fact]
    public void Invoke_FuncNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        _ = Assert.Throws<ArgumentNullException>("action", () => provider.Invoke((Func<int>)null!));
    }

    [Fact]
    public void Invoke_Func_ContextNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        _ = Assert.Throws<ArgumentNullException>("Context", () => provider.Invoke(() => 42));
    }

    [Fact]
    public void Invoke_Func_Expected()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!
        };

        // Act
        var result = provider.Invoke(() => 42);

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public void Invoke_FuncWithInputNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        _ = Assert.Throws<ArgumentNullException>(
            "action",
            () => provider.Invoke((Func<int, int>)null!, 42)
        );
    }

    [Fact]
    public void Invoke_FuncWithInput_ContextNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        _ = Assert.Throws<ArgumentNullException>(
            "Context",
            () => provider.Invoke((int input) => input * 2, 21)
        );
    }

    [Fact]
    public void Invoke_FuncWithInput_Expected()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!
        };

        // Act
        var result = provider.Invoke((int input) => input * 2, 21);

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public async Task InvokeAsync_ActionNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        _ = await Assert.ThrowsAsync<ArgumentNullException>(
            "action",
            async () => await provider.InvokeAsync(null!)
        );
    }

    [Fact]
    public async Task InvokeAsync_Action_ContextNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        _ = await Assert.ThrowsAsync<ArgumentNullException>(
            "Context",
            async () => await provider.InvokeAsync(() => { })
        );
    }

    [Fact]
    public async Task InvokeAsync_ActionThrows_ExpectedException()
    { // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!
        };

        // Act
        _ = await Assert.ThrowsAsync<NotImplementedException>(
            async () =>
                await provider.InvokeAsync(() =>
                {
                    throw new NotImplementedException();
                })
        );
    }

    [Fact]
    public async Task InvokeAsync_Action_CancellationTokenCanceled_ThrowsTaskCanceledException()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = new WindowsFormsSynchronizationContext()
        };

        // Act
        _ = await Assert.ThrowsAsync<TaskCanceledException>(
            async () => await provider.InvokeAsync(() => { }, new CancellationToken(true))
        );
    }

    [Fact]
    public async Task InvokeAsync_Action_Expected()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!
        };

        // Act
        await provider.InvokeAsync(() => { });

        // Assert
        Assert.True(true, "No exception was thrown.");
    }

    [Fact]
    public async Task InvokeAsync_FuncNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        _ = await Assert.ThrowsAsync<ArgumentNullException>(
            "action",
            async () => await provider.InvokeAsync((Func<int>)null!)
        );
    }

    [Fact]
    public async Task InvokeAsync_Func_ContextNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        _ = await Assert.ThrowsAsync<ArgumentNullException>(
            "Context",
            async () => await provider.InvokeAsync(() => 42)
        );
    }

    [Fact]
    public async Task InvokeAsync_FuncThrows_ExpectedException()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!
        };

        // Act
        _ = await Assert.ThrowsAsync<NotImplementedException>(
            async () =>
                await provider.InvokeAsync<int>(() =>
                {
                    throw new NotImplementedException();
                })
        );
    }

    [Fact]
    public async Task InvokeAsync_Func_CancellationTokenCanceled_ThrowsTaskCanceledException()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = new WindowsFormsSynchronizationContext()
        };

        // Act
        _ = await Assert.ThrowsAsync<TaskCanceledException>(
            async () => await provider.InvokeAsync(() => 42, new CancellationToken(true))
        );
    }

    [Fact]
    public async Task InvokeAsync_Func_Expected()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!
        };

        // Act
        var result = await provider.InvokeAsync(() => 42);

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public async Task InvokeAsync_FuncWithInputNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        _ = await Assert.ThrowsAsync<ArgumentNullException>(
            "action",
            async () => await provider.InvokeAsync((Func<int, int>)null!, 42)
        );
    }

    [Fact]
    public async Task InvokeAsync_FuncWithInput_ContextNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        _ = await Assert.ThrowsAsync<ArgumentNullException>(
            "Context",
            async () => await provider.InvokeAsync((int input) => input * 2, 21)
        );
    }

    [Fact]
    public async Task InvokeAsync_FuncWithInputThrows_ExpectedException()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!
        };

        // Act
        _ = await Assert.ThrowsAsync<NotImplementedException>(
            async () =>
                await provider.InvokeAsync<int, int>(
                    (int input) =>
                    {
                        throw new NotImplementedException();
                    },
                    42
                )
        );
    }

    [Fact]
    public async Task InvokeAsync_FuncWithInput_CancellationTokenCanceled_ThrowsTaskCanceledException()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = new WindowsFormsSynchronizationContext()
        };

        // Act
        _ = await Assert.ThrowsAsync<TaskCanceledException>(
            async () =>
                await provider.InvokeAsync(
                    (int input) => input * 2,
                    21,
                    new CancellationToken(true)
                )
        );
    }

    [Fact]
    public async Task InvokeAsync_FuncWithInput_Expected()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = SynchronizationContext.Current!
        };

        // Act
        var result = await provider.InvokeAsync((int input) => input * 2, 21);

        // Assert
        Assert.Equal(42, result);
    }
}
