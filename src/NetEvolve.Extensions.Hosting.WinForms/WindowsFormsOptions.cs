namespace NetEvolve.Extensions.Hosting.WinForms;

using System;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// Options for the Windows Forms host.
/// </summary>
public sealed class WindowsFormsOptions
{
    /// <summary>
    /// Indicates the <see cref="HighDpiMode"/>.
    /// The default is <see cref="HighDpiMode.SystemAware"/>.
    /// </summary>
    public HighDpiMode HighDpiMode { get; set; } = HighDpiMode.SystemAware;

    /// <summary>
    /// Indicates if visual styles are enabled.
    /// The default is <see langword="true"/>.
    /// </summary>
    public bool EnableVisualStyles { get; set; } = true;

    /// <summary>
    /// Indicates if compatible text rendering is enabled.
    /// The default is <see langword="false"/>.
    /// </summary>
    public bool CompatibleTextRenderingDefault { get; set; }

    /// <summary>
    /// Indicates if host lifetime status messages should be supressed such as on startup.
    /// The default is <see langword="false"/>.
    /// </summary>
    public bool SuppressStatusMessages { get; set; }

    /// <summary>
    /// Enables listening for Ctrl+C to additionally initiate shutdown.
    /// The default is <see langword="false"/>.
    /// </summary>
    public bool EnableConsoleShutdown { get; set; }

    /// <summary>
    /// The default font to use inside the WinForms host. If <see langword="null"/> the system default font is used.
    /// </summary>
    public Font? DefaultFont { get; set; }

    /// <summary>
    /// Optional action to run before the main form is created. Something like a splash screen, login form, etc.
    /// </summary>
    public Action<IServiceProvider, IFormularProvider>? PreloadAction { get; set; }
}
