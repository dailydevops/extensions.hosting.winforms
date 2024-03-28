namespace NetEvolve.Extensions.Hosting.WinForms;

using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetEvolve.Extensions.Hosting.WinForms.Internals;

#if NET7_0_OR_GREATER
/// <summary>
/// Extension methods for <see cref="IHostBuilder"/> or <see cref="HostApplicationBuilder"/> to configure Windows Forms Lifetime.
/// </summary>
#else
/// <summary>
/// Extension methods for <see cref="IHostBuilder"/> to configure Windows Forms Lifetime.
/// </summary>
#endif
public static class IHostBuilderExtensions
{
    /// <summary>
    /// Enables Windows Forms support, builds and starts the host with the specified <typeparamref name="TStartForm"/>,
    /// then waits for the host to close the <typeparamref name="TStartForm"/> before shutting down.
    /// </summary>
    /// <typeparam name="TStartForm">Form with which the application is to be started.</typeparam>
    /// <param name="builder">The <see cref="IHostBuilder"/> to configure.</param>
    /// <param name="configure">The action to be executed for the configuration of the <see cref="WindowsFormsOptions"/>.</param>
    /// <returns><see cref="IHostBuilder"/> with enabled Windows Forms support.</returns>
    public static IHostBuilder UseWindowsForms<TStartForm>(
        this IHostBuilder builder,
        Action<WindowsFormsOptions>? configure = null
    )
        where TStartForm : Form
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.ConfigureServices(services =>
            services
                // Add the start form
                .AddSingleton<TStartForm>()
                .AddSingleton(sp => new ApplicationContext(sp.GetRequiredService<TStartForm>()))
                // Default WindowsForms services
                .AddWindowsFormsLifetime(configure)
        );
    }

    /// <summary>
    /// Enables Windows Forms support, builds and starts the host with the specified <typeparamref name="TApplicationContext"/>,
    /// then waits for the host to close the <typeparamref name="TApplicationContext"/> before shutting down.
    /// </summary>
    /// <typeparam name="TApplicationContext"></typeparam>
    /// <param name="builder">The <see cref="IHostBuilder"/> to configure.</param>
    /// <param name="contextFactory">The <see cref="ApplicationContext"/> factory.</param>
    /// <param name="configure">The action to be executed for the configuration of the <see cref="WindowsFormsOptions"/>.</param>
    /// <returns><see cref="IHostBuilder"/> with enabled Windows Forms support.</returns>
    public static IHostBuilder UseWindowsForms<TApplicationContext>(
        this IHostBuilder builder,
        Func<IServiceProvider, TApplicationContext>? contextFactory = null,
        Action<WindowsFormsOptions>? configure = null
    )
        where TApplicationContext : ApplicationContext
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.ConfigureServices(services =>
        {
            services = contextFactory is null
                ? services.AddSingleton<ApplicationContext, TApplicationContext>()
                : services.AddSingleton<ApplicationContext, TApplicationContext>(sp =>
                    contextFactory.Invoke(sp)
                );

            _ = services
            // Default WindowsForms services
            .AddWindowsFormsLifetime(configure);
        });
    }

    /// <summary>
    /// Enables Windows Forms support, builds and starts the host with the specified <typeparamref name="TApplicationContext"/>,
    /// which is created by the <paramref name="contextFactory"/> function, then waits for the host to close the <typeparamref name="TApplicationContext"/> before shutting down.
    /// </summary>
    /// <typeparam name="TApplicationContext"></typeparam>
    /// <typeparam name="TStartForm">Form with which the application is to be started.</typeparam>
    /// <param name="builder">The <see cref="IHostBuilder"/> to configure.</param>
    /// <param name="contextFactory">The <see cref="ApplicationContext"/> factory.</param>
    /// <param name="configure">The action to be executed for the configuration of the <see cref="WindowsFormsOptions"/>.</param>
    /// <returns><see cref="IHostBuilder"/> with enabled Windows Forms support.</returns>
    public static IHostBuilder UseWindowsForms<TApplicationContext, TStartForm>(
        this IHostBuilder builder,
        Func<IServiceProvider, TStartForm, TApplicationContext> contextFactory,
        Action<WindowsFormsOptions>? configure = null
    )
        where TApplicationContext : ApplicationContext
        where TStartForm : Form
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(contextFactory);

        return builder.ConfigureServices(services =>
            services
                .AddSingleton<TStartForm>()
                .AddSingleton(sp =>
                {
                    var startForm = sp.GetRequiredService<TStartForm>();
                    return contextFactory(sp, startForm);
                })
                .AddSingleton<ApplicationContext>(sp =>
                    sp.GetRequiredService<TApplicationContext>()
                )
                // Default WindowsForms services
                .AddWindowsFormsLifetime(configure)
        );
    }

#if NET7_0_OR_GREATER
    /// <summary>
    /// Enables Windows Forms support, builds and starts the host with the specified <typeparamref name="TStartForm"/>,
    /// then waits for the host to close the <typeparamref name="TStartForm"/> before shutting down.
    /// </summary>
    /// <typeparam name="TStartForm">Form with which the application is to be started.</typeparam>
    /// <param name="builder">The <see cref="HostApplicationBuilder"/> to configure.</param>
    /// <param name="configure">The action to be executed for the configuration of the <see cref="WindowsFormsOptions"/>.</param>
    /// <returns><see cref="HostApplicationBuilder"/> with enabled Windows Forms support.</returns>
    public static HostApplicationBuilder UseWindowsForms<TStartForm>(
        this HostApplicationBuilder builder,
        Action<WindowsFormsOptions>? configure = null
    )
        where TStartForm : Form
    {
        ArgumentNullException.ThrowIfNull(builder);

        _ = builder
            .Services.AddSingleton<TStartForm>()
            .AddSingleton(sp => new ApplicationContext(sp.GetRequiredService<TStartForm>()))
            // Default WindowsForms services
            .AddWindowsFormsLifetime(configure);

        return builder;
    }

    /// <summary>
    /// Enables Windows Forms support, builds and starts the host with the specified <typeparamref name="TApplicationContext"/>,
    /// then waits for the host to close the <typeparamref name="TApplicationContext"/> before shutting down.
    /// </summary>
    /// <typeparam name="TApplicationContext"></typeparam>
    /// <param name="builder">The <see cref="HostApplicationBuilder"/> to configure.</param>
    /// <param name="contextFactory">The <see cref="ApplicationContext"/> factory.</param>
    /// <param name="configure">The action to be executed for the configuration of the <see cref="WindowsFormsOptions"/>.</param>
    /// <returns><see cref="HostApplicationBuilder"/> with enabled Windows Forms support.</returns>
    public static HostApplicationBuilder UseWindowsForms<TApplicationContext>(
        this HostApplicationBuilder builder,
        Func<IServiceProvider, TApplicationContext>? contextFactory = null,
        Action<WindowsFormsOptions>? configure = null
    )
        where TApplicationContext : ApplicationContext
    {
        ArgumentNullException.ThrowIfNull(builder);

        var services = contextFactory is null
            ? builder.Services.AddSingleton<ApplicationContext, TApplicationContext>()
            : builder.Services.AddSingleton<ApplicationContext, TApplicationContext>(sp =>
                contextFactory.Invoke(sp)
            );

        _ = services
        // Default WindowsForms services
        .AddWindowsFormsLifetime(configure);

        return builder;
    }

    /// <summary>
    /// Enables Windows Forms support, builds and starts the host with the specified <typeparamref name="TApplicationContext"/>,
    /// which is created by the <paramref name="contextFactory"/> function, then waits for the host to close the <typeparamref name="TApplicationContext"/> before shutting down.
    /// </summary>
    /// <typeparam name="TApplicationContext"></typeparam>
    /// <typeparam name="TStartForm">Form with which the application is to be started.</typeparam>
    /// <param name="builder">The <see cref="HostApplicationBuilder"/> to configure.</param>
    /// <param name="contextFactory">The <see cref="ApplicationContext"/> factory.</param>
    /// <param name="configure">The action to be executed for the configuration of the <see cref="WindowsFormsOptions"/>.</param>
    /// <returns><see cref="HostApplicationBuilder"/> with enabled Windows Forms support.</returns>
    public static HostApplicationBuilder UseWindowsForms<TApplicationContext, TStartForm>(
        this HostApplicationBuilder builder,
        Func<IServiceProvider, TStartForm, TApplicationContext> contextFactory,
        Action<WindowsFormsOptions>? configure = null
    )
        where TApplicationContext : ApplicationContext
        where TStartForm : Form
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(contextFactory);

        _ = builder
            .Services.AddSingleton<TStartForm>()
            .AddSingleton(sp =>
            {
                var startForm = sp.GetRequiredService<TStartForm>();
                return contextFactory(sp, startForm);
            })
            .AddSingleton<ApplicationContext>(sp => sp.GetRequiredService<TApplicationContext>())
            // Default WindowsForms services
            .AddWindowsFormsLifetime(configure);

        return builder;
    }
#endif
}
