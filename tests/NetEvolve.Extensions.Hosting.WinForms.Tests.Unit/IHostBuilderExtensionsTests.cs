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

        _ = Assert.Throws<ArgumentNullException>("builder", () => builder.UseWindowsForms<TestFormFine>());
    }

    [Test]
    public void UseWindowsForms_TApplicationContext_IHostBuilderNull_ThrowArgumentNullException()
    {
        IHostBuilder builder = null!;

        _ = Assert.Throws<ArgumentNullException>("builder", () => builder.UseWindowsForms<TestApplicatonContext>());
    }

    [Test]
    public void UseWindowsForms_TApplicationContextTStartForm_IHostBuilderNull_ThrowArgumentNullException()
    {
        IHostBuilder builder = null!;

        _ = Assert.Throws<ArgumentNullException>(
            "builder",
            () => builder.UseWindowsForms<TestApplicatonContext, TestFormFine>(null!)
        );
    }

    [Test]
    public void UseWindowsForms_TApplicationContextTStartForm_IHostBuilderSet_ContextFactoryNull_ThrowArgumentNullException()
    {
        var builder = Substitute.For<IHostBuilder>();

        _ = Assert.Throws<ArgumentNullException>(
            "contextFactory",
            () => builder.UseWindowsForms<TestApplicatonContext, TestFormFine>(null!)
        );
    }

#if NET7_0_OR_GREATER
    [Test]
    public void UseWindowsForms_TStartForm_HostApplicationBuilderNull_ThrowArgumentNullException()
    {
        HostApplicationBuilder builder = null!;

        _ = Assert.Throws<ArgumentNullException>("builder", () => builder!.UseWindowsForms<TestFormFine>());
    }

    [Test]
    public void UseWindowsForms_TApplicationContext_HostApplicationBuilderNull_ThrowArgumentNullException()
    {
        HostApplicationBuilder builder = null!;

        _ = Assert.Throws<ArgumentNullException>("builder", () => builder.UseWindowsForms<TestApplicatonContext>());
    }

    [Test]
    public void UseWindowsForms_TApplicationContextTStartForm_HostApplicationBuilderNull_ThrowArgumentNullException()
    {
        HostApplicationBuilder builder = null!;

        _ = Assert.Throws<ArgumentNullException>(
            "builder",
            () => builder.UseWindowsForms<TestApplicatonContext, TestFormFine>(null!)
        );
    }

    [Test]
    public void UseWindowsForms_TApplicationContextTStartForm_HostApplicationBuilderSet_ContextFactoryNull_ThrowArgumentNullException()
    {
        var builder = new HostApplicationBuilder();

        _ = Assert.Throws<ArgumentNullException>(
            "contextFactory",
            () => builder.UseWindowsForms<TestApplicatonContext, TestFormFine>(null!)
        );
    }
#endif

#pragma warning disable CA1812
    private sealed class TestFormFine : Form { }
#pragma warning restore CA1812
}
