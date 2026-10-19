using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class ComponentConstructionCatalogTests
{
    [Test]
    public void Every_concrete_component_has_and_meets_a_construction_budget()
    {
        var assembly = typeof(ComponentConstructionCatalogTests).Assembly;
        using var stream = assembly.GetManifestResourceStream(assembly.GetManifestResourceNames().Single(n => n.EndsWith("ComponentConstructionBudgets.json")))!;
        var budgets = JsonSerializer.Deserialize<Dictionary<string, long>>(stream)!;
        var types = ComponentConstructionCatalog.Types.ToArray();
        types.Select(t => t.FullName!).Should().BeEquivalentTo(budgets.Keys,
            "new components must be measured and receive an explicit byte budget");
        foreach (var type in types)
            ComponentConstructionCatalog.Measure(ComponentConstructionCatalog.Close(type))
                .Should().BeLessThanOrEqualTo(budgets[type.FullName!], type.FullName);
    }
}
