namespace NetEvolve.Extensions.Hosting.WinForms.Tests.Architecture;

using ArchUnitNET.Domain;
using ArchUnitNET.xUnit;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

public class NamespaceTests
{
    private static readonly IObjectProvider<IType> _objects = Types()
        .That()
        .ResideInNamespace("NetEvolve.Extensions.Hosting.WinForms");

    [Fact]
    public void Classes_should_be_public()
    {
        var rule = Classes().That().Are(_objects).Should().BePublic();

        rule.Check(ProjectArchitecture.Instance);
    }

    [Fact]
    public void Classes_should_be_Sealed_Or_Static()
    {
        var rule = Classes().That().Are(_objects).Should().BeSealed();

        rule.Check(ProjectArchitecture.Instance);
    }

    [Fact]
    public void Interfaces_should_be_public()
    {
        var rule = Interfaces().That().Are(_objects).Should().BePublic();

        rule.Check(ProjectArchitecture.Instance);
    }
}
