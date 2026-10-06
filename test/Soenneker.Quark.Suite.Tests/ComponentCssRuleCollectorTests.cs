using AwesomeAssertions;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark.Suite.Tests;

public class ComponentCssRuleCollectorTests
{
    [Test]
    public void ManyDeclarations_SurviveBufferGrowthAndRepeatedUse()
    {
        System.Span<char> scratch = stackalloc char[256];
        for (var pass = 0; pass < 3; pass++)
        {
            var collector = new ComponentCssRuleCollector();
            var builder = new PooledStringBuilder(scratch);
            try
            {
                for (var i = 0; i < 100; i++)
                {
                    collector.Add("a", $"--item-{i}: {i}");
                    collector.Add("b", $"--other-{i}: {i}");
                }
                collector.AppendTo(ref builder);
                var output = builder.ToString();
                output.Should().StartWith("a {\n  --item-0: 0;");
                output.Should().Contain("--item-99: 99;\n}\nb {\n  --other-0: 0;");
                output.Should().EndWith("--other-99: 99;\n}");
            }
            finally { builder.Dispose(); collector.Dispose(); }
        }
    }

    [Test]
    public void Styles_TrimSegmentsAndKeepGroupingOrder()
    {
        var collector = new ComponentCssRuleCollector();
        collector.AddStyle("a", " ;  width: 10px ;\t; height: 20px;  ");
        collector.Add("b", "display: block");
        collector.AddStyle("a", "color: red;opacity: 1");
        var builder = new PooledStringBuilder(stackalloc char[256]);
        try
        {
            collector.AppendTo(ref builder);
            builder.ToString().Should().Be("a {\n  width: 10px;\n  height: 20px;\n  color: red;\n  opacity: 1;\n}\nb {\n  display: block;\n}");
        }
        finally { builder.Dispose(); collector.Dispose(); }
    }

    [Test]
    public void BlankStyles_DoNotCreateBlocksOrSeparators()
    {
        var collector = new ComponentCssRuleCollector();
        collector.AddStyle("a", " ; ;\t; ");
        var builder = new PooledStringBuilder(stackalloc char[64]);
        try
        {
            builder.Append("existing");
            collector.AppendTo(ref builder);
            builder.ToString().Should().Be("existing");
        }
        finally { builder.Dispose(); collector.Dispose(); }
    }
}
