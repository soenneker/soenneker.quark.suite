using System;
using System.Reflection;
using System.Linq;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class AotSafetyContractTests
{
    [Test]
    [Arguments(typeof(RenderComponent), "AddCss")]
    [Arguments(typeof(RenderComponent), "AddIf")]
    [Arguments(typeof(ComponentOptions), "AddRules")]
    [Arguments(typeof(Component), "BuildTypographyClassAndStyle")]
    [Arguments(typeof(Component), "BuildLayoutClassAndStyle")]
    [Arguments(typeof(Component), "BuildInteractionClassAndStyle")]
    [Arguments(typeof(Component), "BuildVisualClassAndStyle")]
    [Arguments(typeof(RenderComponent), "BuildClassAndStyleAttributes")]
    [Arguments(typeof(ComponentCssGenerator), "Generate")]
    [Arguments(typeof(ComponentsCssGenerator), "Generate")]
    public void Css_processing_boundaries_must_not_be_inlined(Type declaringType, string methodName)
    {
        var methods = declaringType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(method => method.Name == methodName).ToArray();
        methods.Should().NotBeEmpty($"{declaringType.FullName}.{methodName} must exist");
        foreach (var method in methods)
            (method.MethodImplementationFlags & MethodImplAttributes.NoInlining).Should().Be(MethodImplAttributes.NoInlining);
    }
}
