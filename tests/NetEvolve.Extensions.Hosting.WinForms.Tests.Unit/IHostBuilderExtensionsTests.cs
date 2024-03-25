namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Unit;

using System;
using Microsoft.Extensions.Hosting;
using NetEvolve.Extensions.Hosting.WinForms.Tests.Unit.Internals;
using NSubstitute;
using Xunit;

public class IHostBuilderExtensionsTests
{
    [Fact]
    public void UseWindowsForms_TStartForm_IHostBuilderNull_ThrowArgumentNullException()
    {
        IHostBuilder builder = null!;

        _ = Assert.Throws<ArgumentNullException>(
            "builder",
            () => builder.UseWindowsForms<TestForm>()
        );
    }

    [Fact]
    public void UseWindowsForms_TApplicationContext_IHostBuilderNull_ThrowArgumentNullException()
    {
        IHostBuilder builder = null!;

        _ = Assert.Throws<ArgumentNullException>(
            "builder",
            () => builder.UseWindowsForms<TestApplicatonContext>()
        );
    }

    [Fact]
    public void UseWindowsForms_TApplicationContextTStartForm_IHostBuilderNull_ThrowArgumentNullException()
    {
        IHostBuilder builder = null!;

        _ = Assert.Throws<ArgumentNullException>(
            "builder",
            () => builder.UseWindowsForms<TestApplicatonContext, TestForm>(null!)
        );
    }

    [Fact]
    public void UseWindowsForms_TApplicationContextTStartForm_IHostBuilderSet_ContextFactoryNull_ThrowArgumentNullException()
    {
        var builder = Substitute.For<IHostBuilder>();

        _ = Assert.Throws<ArgumentNullException>(
            "contextFactory",
            () => builder.UseWindowsForms<TestApplicatonContext, TestForm>(null!)
        );
    }

#if NET7_0_OR_GREATER
    [Fact]
    public void UseWindowsForms_TStartForm_HostApplicationBuilderNull_ThrowArgumentNullException()
    {
        HostApplicationBuilder builder = null!;

        _ = Assert.Throws<ArgumentNullException>(
            "builder",
            () => builder!.UseWindowsForms<TestForm>()
        );
    }

    [Fact]
    public void UseWindowsForms_TApplicationContext_HostApplicationBuilderNull_ThrowArgumentNullException()
    {
        HostApplicationBuilder builder = null!;

        _ = Assert.Throws<ArgumentNullException>(
            "builder",
            () => builder.UseWindowsForms<TestApplicatonContext>()
        );
    }

    [Fact]
    public void UseWindowsForms_TApplicationContextTStartForm_HostApplicationBuilderNull_ThrowArgumentNullException()
    {
        HostApplicationBuilder builder = null!;

        _ = Assert.Throws<ArgumentNullException>(
            "builder",
            () => builder.UseWindowsForms<TestApplicatonContext, TestForm>(null!)
        );
    }

    [Fact]
    public void UseWindowsForms_TApplicationContextTStartForm_HostApplicationBuilderSet_ContextFactoryNull_ThrowArgumentNullException()
    {
        var builder = new HostApplicationBuilder();

        _ = Assert.Throws<ArgumentNullException>(
            "contextFactory",
            () => builder.UseWindowsForms<TestApplicatonContext, TestForm>(null!)
        );
    }
#endif
}
