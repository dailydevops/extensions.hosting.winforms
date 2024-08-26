namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Architecture.Internals;

using ArchUnitNET.Domain;
using ArchUnitNET.xUnit;
using NetEvolve.Extensions.Hosting.WinForms.Tests.Architecture;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

public class NamespaceTests
{
    private static readonly IObjectProvider<IType> _objects = Types()
        .That()
        .ResideInNamespace("NetEvolve.Extensions.Hosting.WinForms.Internals");

    [Fact]
    public void Classes_should_be_Internal()
    {
        var rule = Classes().That().Are(_objects).Should().BeInternal();

        rule.Check(ProjectArchitecture.Instance);
    }

    [Fact]
    public void Classes_should_be_Sealed_Or_Static()
    {
        var rule = Classes().That().Are(_objects).Should().BeSealed();

        rule.Check(ProjectArchitecture.Instance);
    }

    [Fact]
    public void Interfaces_should_be_internal()
    {
        var rule = Interfaces().That().Are(_objects).Should().BeInternal();

        rule.WithoutRequiringPositiveResults().Check(ProjectArchitecture.Instance);
    }
}
