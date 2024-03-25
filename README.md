# NetEvolve.Extensions.Hosting.WinForms

[![Nuget](https://img.shields.io/nuget/v/NetEvolve.Extensions.Hosting.WinForms?logo=nuget)](https://www.nuget.org/packages/NetEvolve.Extensions.Hosting.WinForms/)
[![Nuget](https://img.shields.io/nuget/dt/NetEvolve.Extensions.Hosting.WinForms?logo=nuget)](https://www.nuget.org/packages/NetEvolve.Extensions.Hosting.WinForms/)

The main purpose of this package is to provide a way to use the `Microsoft.Extensions.Hosting` for WinForms applications, allowing the use of dependency injection, configuration, logging, and other features provided by the `Microsoft.Extensions` libraries.

:bulb: This package is available for .NET 6.0 and later.

## Contribution

If you have any suggestions, bug reports, or any other form of feedback, please feel free to open an issue or a pull request. Any contributions are welcome!

## Why not .NET Standard?
With the .NET Standard Microsoft created a specification for APIs that are intended to be available on all .NET implementations. This was a great idea, but it also has some drawbacks. The main drawback is that the .NET Standard is a specification and not an implementation. This means that the real work is done by .NET implementations, such as .NET 5.0 and later versions. Which is why we decided us against the .NET Standard and for the concrete .NET implementations.

See [The future of .NET Standard](https://devblogs.microsoft.com/dotnet/the-future-of-net-standard/) for more details.

## Installation
To use this package, you need to add the package to your project. You can do this by using the NuGet package manager or by using the dotnet CLI.
```powershell
dotnet add package NetEvolve.Extensions.Hosting.WinForms
```

## Usage
To use the `Microsoft.Extensions.Hosting` in a WinForms application, you just need to create a new `HostBuilder` and configure it as you would do in a console application.

```csharp
namespace WinForms;

using Microsoft.Extensions.Hosting;
using NetEvolve.Extensions.Hosting.WinForms;

internal static class Program
{
    internal static async Task Main() =>
        await CreateHostBuilder().Build().RunAsync().ConfigureAwait(false);

    public static IHostBuilder CreateHostBuilder() =>
        Host.CreateDefaultBuilder().UseWindowsForms<Form1>();
}
```

Therefore, you can use for example the `Microsoft.Extensions.DependencyInjection` to register services and inject them into your forms.

```csharp
namespace WinForms;

using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private readonly ILogger<Form1> _logger;

    public Form1(ILogger<Form1> logger)
    {
        _logger = logger;
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        _logger.LogInformation("Form loaded.");
    }
}
```