namespace Xample.Simple;

using Microsoft.Extensions.Hosting;
using NetEvolve.Extensions.Hosting.WinForms;

internal static class Program
{
    internal static async Task Main() => await CreateHostBuilder().Build().RunAsync().ConfigureAwait(false);

    public static IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder().UseWindowsForms<Form1>();
}
