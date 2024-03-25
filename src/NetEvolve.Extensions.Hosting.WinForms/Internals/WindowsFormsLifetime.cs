namespace NetEvolve.Extensions.Hosting.WinForms.Internals;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

internal sealed partial class WindowsFormsLifetime(
    IOptions<WindowsFormsOptions> optionsGetter,
    IHostEnvironment environment,
    IHostApplicationLifetime applicationLifetime,
    ILoggerFactory loggerFactory
) : IHostLifetime, IDisposable
{
    private CancellationTokenRegistration _applicationStarted;
    private CancellationTokenRegistration _applicationStopping;
    private bool _disposedValue;

    private readonly WindowsFormsOptions _options = optionsGetter.Value;
    private readonly ILogger _logger = loggerFactory.CreateLogger("Microsoft.Hosting.Lifetime");

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task WaitForStartAsync(CancellationToken cancellationToken)
    {
        if (!_options.SuppressStatusMessages)
        {
            _applicationStarted = applicationLifetime.ApplicationStarted.Register(
                state =>
                {
                    if (state is WindowsFormsLifetime lifetime)
                    {
                        lifetime.OnApplicationStarted();
                    }
                },
                this
            );
            _applicationStopping = applicationLifetime.ApplicationStopping.Register(
                state =>
                {
                    if (state is WindowsFormsLifetime lifetime)
                    {
                        lifetime.OnApplicationStopping();
                    }
                },
                this
            );
        }

        if (_options.EnableConsoleShutdown)
        {
            Console.CancelKeyPress += OnCancelKeyPress;
        }

        return Task.CompletedTask;
    }

    private void OnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        e.Cancel = true;
        applicationLifetime.StopApplication();
    }

    private void OnApplicationStopping() => LogShuttingDown(_logger);

    private void OnApplicationStarted()
    {
        Action<ILogger> logAction = _options.EnableConsoleShutdown
            ? LogStartedWithConsoleShutdown
            : LogStarted;
        logAction(_logger);

        LogStartedDetails(_logger, environment.EnvironmentName, environment.ContentRootPath);
    }

    [LoggerMessage(1, LogLevel.Information, "Application is shutting down...")]
    private static partial void LogShuttingDown(ILogger logger);

    [LoggerMessage(
        2,
        LogLevel.Information,
        "Application started. Close the startup Form to shut down."
    )]
    private static partial void LogStarted(ILogger logger);

    [LoggerMessage(
        3,
        LogLevel.Information,
        "Application started. Close the startup Form or press CTRL+C to shut down."
    )]
    private static partial void LogStartedWithConsoleShutdown(ILogger logger);

    [LoggerMessage(
        4,
        LogLevel.Debug,
        """
            Hosting environment: {EnvironmentName}
            Content root path: {ContentRootPath}
            """
    )]
    private static partial void LogStartedDetails(
        ILogger logger,
        string environmentName,
        string contentRootPath
    );

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _applicationStarted.Dispose();
                _applicationStopping.Dispose();

                if (_options.EnableConsoleShutdown)
                {
                    Console.CancelKeyPress -= OnCancelKeyPress;
                }
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
