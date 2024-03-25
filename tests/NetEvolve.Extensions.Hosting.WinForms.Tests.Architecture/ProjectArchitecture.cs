namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Architecture;

using System;
using System.Threading;
using ArchUnitNET.Domain;
using ArchUnitNET.Loader;

internal static class ProjectArchitecture
{
    // TIP: load your architecture once at the start to maximize performance of your tests
    private static readonly Lazy<Architecture> _instance = new Lazy<Architecture>(
        () => LoadArchitecture(),
        LazyThreadSafetyMode.PublicationOnly
    );

    public static Architecture Instance => _instance.Value;

    private static Architecture LoadArchitecture()
    {
        var architecture = new ArchLoader()
            .LoadAssembly(typeof(IFormularProvider).Assembly)
            .Build();
        return architecture;
    }
}
