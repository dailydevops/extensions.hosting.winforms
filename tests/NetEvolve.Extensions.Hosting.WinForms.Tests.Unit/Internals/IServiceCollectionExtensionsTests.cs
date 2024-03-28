namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Unit.Internals;

using System;
using Microsoft.Extensions.DependencyInjection;
using NetEvolve.Extensions.Hosting.WinForms.Internals;
using Xunit;

public class IServiceCollectionExtensionsTests
{
    [Fact]
    public void AddWindowsFormsLifetime_ServicesNull_ThrowArgumentNullException() =>
        _ = Assert.Throws<ArgumentNullException>(
            "services",
            () => IServiceCollectionExtensions.AddWindowsFormsLifetime(null!, null)
        );

    [Theory]
    [MemberData(nameof(AddWindowsFormsData))]
    public void AddWindowsFormsLifetime_ConfigurationNull_Expected(
        int expectedServices,
        Action<WindowsFormsOptions>? configure
    )
    {
        var serviceCollection = new ServiceCollection().AddWindowsFormsLifetime(configure);

        var services = serviceCollection.BuildServiceProvider();

        Assert.NotNull(services);
        Assert.Equal(expectedServices, serviceCollection.Count);
    }

    public static TheoryData<int, Action<WindowsFormsOptions>?> AddWindowsFormsData =>
        new TheoryData<int, Action<WindowsFormsOptions>?>
        {
            { 5, null },
            {
                11,
                options =>
                {
                    options.EnableConsoleShutdown = true;
                }
            }
        };
}
