namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Architecture;

using ArchUnitNET.Domain;
using ArchUnitNET.TUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

public partial class NamespaceTests
{
    private static readonly IObjectProvider<IType> _objects = Types()
        .That()
        .ResideInNamespace("NetEvolve.Extensions.Hosting.WinForms");

    [Test]
    public void Classes_should_be_public()
    {
        var rule = Classes().That().Are(_objects).Should().BePublic();

        rule.Check(ProjectArchitecture.Instance);
    }

    [Test]
    public void Classes_should_be_Sealed_Or_Static()
    {
        var rule = Classes().That().Are(_objects).Should().BeSealed();

        rule.Check(ProjectArchitecture.Instance);
    }

    [Test]
    public void Interfaces_should_be_public()
    {
        var rule = Interfaces().That().Are(_objects).Should().BePublic();

        rule.Check(ProjectArchitecture.Instance);
    }
}
