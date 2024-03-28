namespace NetEvolve.Extensions.Hosting.WinForms.Internals;

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal static class IServiceCollectionExtensions
{
    public static IServiceCollection AddWindowsFormsLifetime(
        this IServiceCollection services,
        Action<WindowsFormsOptions>? configure
    )
    {
        ArgumentNullException.ThrowIfNull(services);

        if (configure is not null)
        {
            _ = services.Configure(configure);
        }

        return services
            // Add the WindowsFormsLifetime
            .AddSingleton<IHostLifetime, WindowsFormsLifetime>()
            // Add the FormularProvider
            .AddSingleton<IFormularProvider, FormularProvider>()
            // Add the SyncronizationContext provider for WindowsForms
            .AddSingleton<WindowsFormsSynchronizationContextProvider>()
            .AddSingleton<IWindowsFormsSynchronizationContextProvider>(sp =>
                sp.GetRequiredService<WindowsFormsSynchronizationContextProvider>()
            )
            .AddHostedService<WindowsFormsHostedService>();
    }
}
