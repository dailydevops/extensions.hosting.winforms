namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Architecture.Internals;

using ArchUnitNET.Domain;
using ArchUnitNET.TUnit;
using NetEvolve.Extensions.Hosting.WinForms.Tests.Architecture;
using TUnit.Core;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

public partial class NamespaceTests
{
    private static readonly IObjectProvider<IType> _objects = Types()
        .That()
        .ResideInNamespace("NetEvolve.Extensions.Hosting.WinForms.Internals");

    [Test]
    public void Classes_should_be_Internal()
    {
        var rule = Classes().That().Are(_objects).Should().BeInternal();

        rule.Check(ProjectArchitecture.Instance);
    }

    [Test]
    public void Classes_should_be_Sealed_Or_Static()
    {
        var rule = Classes().That().Are(_objects).Should().BeSealed();

        rule.Check(ProjectArchitecture.Instance);
    }

    [Test]
    public void Interfaces_should_be_internal()
    {
        var rule = Interfaces().That().Are(_objects).Should().BeInternal();

        rule.WithoutRequiringPositiveResults().Check(ProjectArchitecture.Instance);
    }
}
