using System.Collections.Generic;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public class ClassListTests
{
    [Test]
    public void Append_PreservesExistingClassOrderAndSkipsBlankContributors()
    {
        var attributes = new Dictionary<string, object> { ["class"] = "consumer" };
        ClassListProbe.Append(attributes, ["flex", null, "  ", "items-center"]);
        attributes["class"].Should().Be("consumer flex items-center");
    }

    [Test]
    public void Append_WithNoContributors_ReusesExistingString()
    {
        var existing = new string('x', 10);
        var attributes = new Dictionary<string, object> { ["class"] = existing };
        ClassListProbe.Append(attributes, [null, " "]);
        attributes["class"].Should().BeSameAs(existing);
    }

    [Test]
    public void Append_NullArrayDoesNotAddAnEmptyAttribute()
    {
        var attributes = new Dictionary<string, object>();
        ClassListProbe.AppendArray(attributes, null!);
        attributes.Should().BeEmpty();
    }
}
