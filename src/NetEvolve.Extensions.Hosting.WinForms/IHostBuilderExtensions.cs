namespace NetEvolve.Extensions.Hosting.WinForms;

using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetEvolve.Extensions.Hosting.WinForms.Internals;

public static class IHostBuilderExtensions
{
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
                .AddWindowsForms(configure)
        );
    }

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
                ? services.AddSingleton<TApplicationContext>()
                : services.AddSingleton(sp => contextFactory.Invoke(sp));

            _ = services
                .AddSingleton<ApplicationContext, TApplicationContext>()
                // Default WindowsForms services
                .AddWindowsForms(configure);
        });
    }

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
                .AddWindowsForms(configure)
        );
    }

#if NET7_0
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
            .AddWindowsForms(configure);

        return builder;
    }

    public static HostApplicationBuilder UseWindowsForms<TApplicationContext>(
        this HostApplicationBuilder builder,
        Func<IServiceProvider, TApplicationContext>? contextFactory = null,
        Action<WindowsFormsOptions>? configure = null
    )
        where TApplicationContext : ApplicationContext
    {
        ArgumentNullException.ThrowIfNull(builder);

        var services = contextFactory is null
            ? builder.Services.AddSingleton<TApplicationContext>()
            : builder.Services.AddSingleton(sp => contextFactory.Invoke(sp));

        _ = services
            .AddSingleton<ApplicationContext>(sp => sp.GetRequiredService<TApplicationContext>())
            // Default WindowsForms services
            .AddWindowsForms(configure);

        return builder;
    }

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
            .AddWindowsForms(configure);

        return builder;
    }
#endif

#if NET8_0_OR_GREATER
    public static IHostApplicationBuilder UseWindowsForms<TStartForm>(
        this IHostApplicationBuilder builder,
        Action<WindowsFormsOptions>? configure = null
    )
        where TStartForm : Form
    {
        ArgumentNullException.ThrowIfNull(builder);

        _ = builder
            .Services.AddSingleton<TStartForm>()
            .AddSingleton(sp => new ApplicationContext(sp.GetRequiredService<TStartForm>()))
            // Default WindowsForms services
            .AddWindowsForms(configure);

        return builder;
    }

    public static IHostApplicationBuilder UseWindowsForms<TApplicationContext>(
        this IHostApplicationBuilder builder,
        Func<IServiceProvider, TApplicationContext>? contextFactoy = null,
        Action<WindowsFormsOptions>? configure = null
    )
        where TApplicationContext : ApplicationContext
    {
        ArgumentNullException.ThrowIfNull(builder);

        var services = contextFactoy is null
            ? builder.Services.AddSingleton<TApplicationContext>()
            : builder.Services.AddSingleton(sp => contextFactoy(sp));

        _ = services
            .AddSingleton<ApplicationContext>(sp => sp.GetRequiredService<TApplicationContext>())
            // Default WindowsForms services
            .AddWindowsForms(configure);

        return builder;
    }

    public static IHostApplicationBuilder UseWindowsForms<TApplicationContext, TStartForm>(
        this IHostApplicationBuilder builder,
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
            .AddWindowsForms(configure);

        return builder;
    }
#endif
}
