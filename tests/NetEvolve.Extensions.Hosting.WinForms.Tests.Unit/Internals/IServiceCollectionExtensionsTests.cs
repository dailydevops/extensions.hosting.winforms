namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Unit.Internals;

using System;
using Microsoft.Extensions.DependencyInjection;
using NetEvolve.Extensions.Hosting.WinForms.Internals;
using TUnit.Assertions.Extensions;
using TUnit.Core;

public partial class IServiceCollectionExtensionsTests
{
    [Test]
    public void AddWindowsFormsLifetime_ServicesNull_ThrowArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(() =>
            IServiceCollectionExtensions.AddWindowsFormsLifetime(null!, null)
        );
        Assert.That(exception.ParamName).IsEqualTo("services");
    }

    [Test]
    [Arguments(5, null)]
    [Arguments(11, "EnableConsoleShutdown")]
    public void AddWindowsFormsLifetime_ConfigurationNull_Expected(int expectedServices, string? configurationType)
    {
        Action<WindowsFormsOptions>? configure = configurationType switch
        {
            "EnableConsoleShutdown" => options => options.EnableConsoleShutdown = true,
            _ => null,
        };

        var serviceCollection = new ServiceCollection().AddWindowsFormsLifetime(configure);

        var services = serviceCollection.BuildServiceProvider();

        Assert.That(services).IsNotNull();
        Assert.That(serviceCollection.Count).IsEqualTo(expectedServices);
    }
}
