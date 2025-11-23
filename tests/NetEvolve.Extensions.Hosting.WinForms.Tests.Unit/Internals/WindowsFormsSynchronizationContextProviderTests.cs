namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Unit.Internals;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetEvolve.Extensions.Hosting.WinForms.Internals;
using TUnit.Assertions.Extensions;
using TUnit.Core;

public partial class WindowsFormsSynchronizationContextProviderTests
{
    [Test]
    public void Invoke_ActionNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        var exception = Assert.Throws<ArgumentNullException>(() => provider.Invoke(null!));
        Assert.That(exception.ParamName).IsEqualTo("action");
    }

    [Test]
    public void Invoke_Action_ContextNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        var exception = Assert.Throws<ArgumentNullException>(() => provider.Invoke(() => { }));
        Assert.That(exception.ParamName).IsEqualTo("Context");
    }

    [Test]
    public void Invoke_Action_Expected()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider { Context = SynchronizationContext.Current! };

        // Act / Assert
        provider.Invoke(() => { });

        // No exception was thrown.
    }

    [Test]
    public void Invoke_FuncNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        var exception = Assert.Throws<ArgumentNullException>(() => provider.Invoke((Func<int>)null!));
        Assert.That(exception.ParamName).IsEqualTo("action");
    }

    [Test]
    public void Invoke_Func_ContextNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        var exception = Assert.Throws<ArgumentNullException>(() => provider.Invoke(() => 42));
        Assert.That(exception.ParamName).IsEqualTo("Context");
    }

    [Test]
    public void Invoke_Func_Expected()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider { Context = SynchronizationContext.Current! };

        // Act
        var result = provider.Invoke(() => 42);

        // Assert
        Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public void Invoke_FuncWithInputNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        var exception = Assert.Throws<ArgumentNullException>(() => provider.Invoke((Func<int, int>)null!, 42));
        Assert.That(exception.ParamName).IsEqualTo("action");
    }

    [Test]
    public void Invoke_FuncWithInput_ContextNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        var exception = Assert.Throws<ArgumentNullException>(() => provider.Invoke((int input) => input * 2, 21));
        Assert.That(exception.ParamName).IsEqualTo("Context");
    }

    [Test]
    public void Invoke_FuncWithInput_Expected()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider { Context = SynchronizationContext.Current! };

        // Act
        var result = provider.Invoke((int input) => input * 2, 21);

        // Assert
        Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task InvokeAsync_ActionNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await provider.InvokeAsync(null!));
        Assert.That(exception.ParamName).IsEqualTo("action");
    }

    [Test]
    public async Task InvokeAsync_Action_ContextNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await provider.InvokeAsync(() => { })
        );
        Assert.That(exception.ParamName).IsEqualTo("Context");
    }

    [Test]
    public async Task InvokeAsync_ActionThrows_ExpectedException()
    { // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider { Context = SynchronizationContext.Current! };

        // Act
        await Assert.ThrowsAsync<NotImplementedException>(async () =>
            await provider.InvokeAsync(() =>
            {
                throw new NotImplementedException();
            })
        );
    }

    [Test]
    public async Task InvokeAsync_Action_CancellationTokenCanceled_ThrowsTaskCanceledException()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = new WindowsFormsSynchronizationContext(),
        };

        // Act
        await Assert.ThrowsAsync<TaskCanceledException>(async () =>
            await provider.InvokeAsync(() => { }, new CancellationToken(true))
        );
    }

    [Test]
    public async Task InvokeAsync_Action_Expected()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider { Context = SynchronizationContext.Current! };

        // Act
        await provider.InvokeAsync(() => { });

        // Assert
        // No exception was thrown.
    }

    [Test]
    public async Task InvokeAsync_FuncNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await provider.InvokeAsync((Func<int>)null!)
        );
        Assert.That(exception.ParamName).IsEqualTo("action"");
    }

    [Test]
    public async Task InvokeAsync_Func_ContextNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await provider.InvokeAsync(() => 42)
        );
        Assert.That(exception.ParamName).IsEqualTo("Context"");
    }

    [Test]
    public async Task InvokeAsync_FuncThrows_ExpectedException()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider { Context = SynchronizationContext.Current! };

        // Act
        await Assert.ThrowsAsync<NotImplementedException>(async () =>
            await provider.InvokeAsync<int>(() =>
            {
                throw new NotImplementedException();
            })
        );
    }

    [Test]
    public async Task InvokeAsync_Func_CancellationTokenCanceled_ThrowsTaskCanceledException()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = new WindowsFormsSynchronizationContext(),
        };

        // Act
        await Assert.ThrowsAsync<TaskCanceledException>(async () =>
            await provider.InvokeAsync(() => 42, new CancellationToken(true))
        );
    }

    [Test]
    public async Task InvokeAsync_Func_Expected()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider { Context = SynchronizationContext.Current! };

        // Act
        var result = await provider.InvokeAsync(() => 42);

        // Assert
        Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task InvokeAsync_FuncWithInputNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await provider.InvokeAsync((Func<int, int>)null!, 42)
        );
        Assert.That(exception.ParamName).IsEqualTo("action"");
    }

    [Test]
    public async Task InvokeAsync_FuncWithInput_ContextNull_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new WindowsFormsSynchronizationContextProvider();

        // Act / Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await provider.InvokeAsync((int input) => input * 2, 21)
        );
        Assert.That(exception.ParamName).IsEqualTo("Context"");
    }

    [Test]
    public async Task InvokeAsync_FuncWithInputThrows_ExpectedException()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider { Context = SynchronizationContext.Current! };

        // Act
        await Assert.ThrowsAsync<NotImplementedException>(async () =>
            await provider.InvokeAsync<int, int>(
                (int input) =>
                {
                    throw new NotImplementedException();
                },
                42
            )
        );
    }

    [Test]
    public async Task InvokeAsync_FuncWithInput_CancellationTokenCanceled_ThrowsTaskCanceledException()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider
        {
            Context = new WindowsFormsSynchronizationContext(),
        };

        // Act
        await Assert.ThrowsAsync<TaskCanceledException>(async () =>
            await provider.InvokeAsync((int input) => input * 2, 21, new CancellationToken(true))
        );
    }

    [Test]
    public async Task InvokeAsync_FuncWithInput_Expected()
    {
        // Arrange
        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        var provider = new WindowsFormsSynchronizationContextProvider { Context = SynchronizationContext.Current! };

        // Act
        var result = await provider.InvokeAsync((int input) => input * 2, 21);

        // Assert
        Assert.That(result).IsEqualTo(42);
    }
}
