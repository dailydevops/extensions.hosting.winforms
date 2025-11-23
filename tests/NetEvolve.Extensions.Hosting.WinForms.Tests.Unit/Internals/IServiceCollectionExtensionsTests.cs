namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Unit.Internals;

using System;
using Microsoft.Extensions.DependencyInjection;
using NetEvolve.Extensions.Hosting.WinForms.Internals;

public partial class IServiceCollectionExtensionsTests
{
    [Test]
    public void AddWindowsFormsLifetime_ServicesNull_ThrowArgumentNullException() =>
        _ = Assert.Throws<ArgumentNullException>(
            "services",
            () => IServiceCollectionExtensions.AddWindowsFormsLifetime(null!, null)
        );

    [Test]
    [Arguments(5, null)]
    [Arguments(11, "EnableConsoleShutdown")]
    public async Task AddWindowsFormsLifetime_ConfigurationNull_Expected(
        int expectedServices,
        string? configurationType
    )
    {
        Action<WindowsFormsOptions>? configure = configurationType switch
        {
            "EnableConsoleShutdown" => options => options.EnableConsoleShutdown = true,
            _ => null,
        };

        var serviceCollection = new ServiceCollection().AddWindowsFormsLifetime(configure);

        var services = serviceCollection.BuildServiceProvider();

        using (Assert.Multiple())
        {
            _ = await Assert.That(services).IsNotNull();
            _ = await Assert.That(serviceCollection.Count).IsEqualTo(expectedServices);
        }
    }
}
