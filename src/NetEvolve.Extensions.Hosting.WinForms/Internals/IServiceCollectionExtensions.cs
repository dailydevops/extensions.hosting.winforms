namespace NetEvolve.Extensions.Hosting.WinForms.Internals;

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

internal static class IServiceCollectionExtensions
{
    public static IServiceCollection AddWindowsForms(
        this IServiceCollection services,
        Action<WindowsFormsOptions>? configure,
        Action<IServiceProvider>? additionalServices
    )
    {
        if (configure is not null)
        {
            _ = services.Configure(configure);
        }

        return services
            // Add the WindowsFormsLifetime
            .AddSingleton<IHostLifetime, WindowsFormsLifetime>()
            // Add the SyncronizationContext provider for WindowsForms
            .AddSingleton<WindowsFormsSynchronizationContextProvider>()
            .AddSingleton<IWindowsFormsSynchronizationContextProvider>(sp =>
                sp.GetRequiredService<WindowsFormsSynchronizationContextProvider>()
            )
            .AddHostedService(sp =>
            {
                var options = sp.GetRequiredService<IOptions<WindowsFormsOptions>>();
                var lifetime = sp.GetRequiredService<IHostApplicationLifetime>();
                var synchronizationContext =
                    sp.GetRequiredService<WindowsFormsSynchronizationContextProvider>();

                return new WindowsFormsHostedService(
                    options,
                    lifetime,
                    sp,
                    synchronizationContext,
                    additionalServices
                );
            });
    }
}
