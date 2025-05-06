namespace NetEvolve.Extensions.Hosting.WinForms.Internals;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

internal sealed class WindowsFormsHostedService(
    IOptions<WindowsFormsOptions> options,
    IHostApplicationLifetime applicationLifetime,
    IServiceProvider serviceProvider,
    WindowsFormsSynchronizationContextProvider synchronizationContextProvider
) : IHostedService, IDisposable
{
    private CancellationTokenRegistration _cancellationTokenRegistration;
    private readonly WindowsFormsOptions _options = options.Value;
    private bool _disposedValue;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenRegistration = applicationLifetime.ApplicationStopping.Register(
            state =>
            {
                if (state is WindowsFormsHostedService hostedService)
                {
                    hostedService.OnApplicationStopping();
                }
            },
            this
        );

        var uiThread = new Thread(StartUIThread);
        uiThread.SetApartmentState(ApartmentState.STA);
        uiThread.Name = "Hosting UI Thread";
        uiThread.Start();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                Application.ApplicationExit -= OnApplicationExit;
                _cancellationTokenRegistration.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void OnApplicationExit(object? sender, EventArgs e) => applicationLifetime.StopApplication();

    private void OnApplicationStopping()
    {
        var applicationContext = serviceProvider.GetService<ApplicationContext>();
        var mainForm = applicationContext?.MainForm;

        // If the main form is not null and the handle is created, close and dispose it.
        if (mainForm is not null && mainForm.IsHandleCreated)
        {
            mainForm.Invoke(() =>
            {
                mainForm.Close();
                mainForm.Dispose();
            });
        }
    }

    private void StartUIThread()
    {
        _ = Application.SetHighDpiMode(_options.HighDpiMode);
        Application.SetCompatibleTextRenderingDefault(_options.CompatibleTextRenderingDefault);
        if (_options.EnableVisualStyles)
        {
            Application.EnableVisualStyles();
        }

        if (_options.DefaultFont is not null)
        {
            Application.SetDefaultFont(_options.DefaultFont);
        }

        Application.ApplicationExit += OnApplicationExit;

        // Disable the auto install of the WindowsFormsSynchronizationContext.
        WindowsFormsSynchronizationContext.AutoInstall = false;

        // Create the WindowsFormsSynchronizationContext and set it as the current synchronization context.
        synchronizationContextProvider.Context = new WindowsFormsSynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(synchronizationContextProvider.Context);

        if (_options.PreloadAction is not null)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var formsProvider = scope.ServiceProvider.GetRequiredService<IFormularProvider>();
                _options.PreloadAction.Invoke(scope.ServiceProvider, formsProvider);
            }
        }

        var applicationContext = serviceProvider.GetRequiredService<ApplicationContext>();

        Application.Run(applicationContext);
    }
}
