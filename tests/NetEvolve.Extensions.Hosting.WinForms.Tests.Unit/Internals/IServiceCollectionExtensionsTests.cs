namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Unit.Internals;

using System;
using Microsoft.Extensions.DependencyInjection;
using NetEvolve.Extensions.Hosting.WinForms.Internals;
using Xunit;

public class IServiceCollectionExtensionsTests
{
    [Fact]
    public void AddWindowsForms_ServicesNull_ThrowArgumentNullException() =>
        _ = Assert.Throws<ArgumentNullException>(
            "services",
            () => IServiceCollectionExtensions.AddWindowsForms(null!, null)
        );

    [Theory]
    [MemberData(nameof(AddWindowsFormsData))]
    public void AddWindowsForms_ConfigurationNull_Expected(
        int expectedServices,
        Action<WindowsFormsOptions>? configure
    )
    {
        var serviceCollection = new ServiceCollection().AddWindowsForms(configure);

        var services = serviceCollection.BuildServiceProvider();

        Assert.NotNull(services);
        Assert.Equal(expectedServices, serviceCollection.Count);
    }

    public static TheoryData<int, Action<WindowsFormsOptions>?> AddWindowsFormsData =>
        new TheoryData<int, Action<WindowsFormsOptions>?>
        {
            { 4, null },
            {
                10,
                options =>
                {
                    options.EnableConsoleShutdown = true;
                }
            }
        };
}
