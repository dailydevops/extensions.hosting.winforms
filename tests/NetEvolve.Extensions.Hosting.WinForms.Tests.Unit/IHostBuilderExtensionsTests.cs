namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Unit;

using System;
using System.Windows.Forms;
using Microsoft.Extensions.Hosting;
using NetEvolve.Extensions.Hosting.WinForms.Tests.Unit.Internals;
using NSubstitute;

public partial class IHostBuilderExtensionsTests
{
    [Test]
    public void UseWindowsForms_TStartForm_IHostBuilderNull_ThrowArgumentNullException()
    {
        IHostBuilder builder = null!;

        var exception = await Assert.Throws<ArgumentNullException>(() => builder.UseWindowsForms<TestFormFine>());
        await Assert.That(exception.ParamName).IsEqualTo("builder");
    }

    [Test]
    public void UseWindowsForms_TApplicationContext_IHostBuilderNull_ThrowArgumentNullException()
    {
        IHostBuilder builder = null!;

        var exception = await Assert.Throws<ArgumentNullException>(() =>
            builder.UseWindowsForms<TestApplicatonContext>()
        );
        await Assert.That(exception.ParamName).IsEqualTo("builder");
    }

    [Test]
    public void UseWindowsForms_TApplicationContextTStartForm_IHostBuilderNull_ThrowArgumentNullException()
    {
        IHostBuilder builder = null!;

        var exception = await Assert.Throws<ArgumentNullException>(() =>
            builder.UseWindowsForms<TestApplicatonContext, TestFormFine>(null!)
        );
        await Assert.That(exception.ParamName).IsEqualTo("builder");
    }

    [Test]
    public void UseWindowsForms_TApplicationContextTStartForm_IHostBuilderSet_ContextFactoryNull_ThrowArgumentNullException()
    {
        var builder = Substitute.For<IHostBuilder>();

        var exception = await Assert.Throws<ArgumentNullException>(() =>
            builder.UseWindowsForms<TestApplicatonContext, TestFormFine>(null!)
        );
        await Assert.That(exception.ParamName).IsEqualTo("contextFactory");
    }

#if NET7_0_OR_GREATER
    [Test]
    public void UseWindowsForms_TStartForm_HostApplicationBuilderNull_ThrowArgumentNullException()
    {
        HostApplicationBuilder builder = null!;

        var exception = await Assert.Throws<ArgumentNullException>(() => builder!.UseWindowsForms<TestFormFine>());
        await Assert.That(exception.ParamName).IsEqualTo("builder");
    }

    [Test]
    public void UseWindowsForms_TApplicationContext_HostApplicationBuilderNull_ThrowArgumentNullException()
    {
        HostApplicationBuilder builder = null!;

        var exception = await Assert.Throws<ArgumentNullException>(() =>
            builder.UseWindowsForms<TestApplicatonContext>()
        );
        await Assert.That(exception.ParamName).IsEqualTo("builder");
    }

    [Test]
    public void UseWindowsForms_TApplicationContextTStartForm_HostApplicationBuilderNull_ThrowArgumentNullException()
    {
        HostApplicationBuilder builder = null!;

        var exception = await Assert.Throws<ArgumentNullException>(() =>
            builder.UseWindowsForms<TestApplicatonContext, TestFormFine>(null!)
        );
        await Assert.That(exception.ParamName).IsEqualTo("builder");
    }

    [Test]
    public void UseWindowsForms_TApplicationContextTStartForm_HostApplicationBuilderSet_ContextFactoryNull_ThrowArgumentNullException()
    {
        var builder = new HostApplicationBuilder();

        var exception = await Assert.Throws<ArgumentNullException>(() =>
            builder.UseWindowsForms<TestApplicatonContext, TestFormFine>(null!)
        );
        await Assert.That(exception.ParamName).IsEqualTo("contextFactory");
    }
#endif

#pragma warning disable CA1812
    private sealed class TestFormFine : Form { }
#pragma warning restore CA1812
}
