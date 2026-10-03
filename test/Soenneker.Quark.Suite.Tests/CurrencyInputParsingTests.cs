using System;
using System.Globalization;
using System.Reflection;
using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Currency_input_observes_distinct_readonly_cultures_with_the_same_name()
    {
        CultureInfo previous = CultureInfo.CurrentCulture;
        var first = (CultureInfo)CultureInfo.InvariantCulture.Clone();
        var second = (CultureInfo)CultureInfo.InvariantCulture.Clone();
        second.NumberFormat.NumberDecimalSeparator = ":";
        try
        {
            // Exercise both caches in this execution context; the renderer captures its culture.
            var input = new CurrencyInput();
            foreach (string name in new[] { "FormatDisplayValue", "FormatEditingValue" })
            {
                var format = typeof(CurrencyInput).GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic)!;
                CultureInfo.CurrentCulture = CultureInfo.ReadOnly(first);
                format.Invoke(input, [12.34m]).Should().Be("12.34");
                CultureInfo.CurrentCulture = CultureInfo.ReadOnly(second);
                format.Invoke(input, [12.34m]).Should().Be("12:34");
            }
        }
        finally { CultureInfo.CurrentCulture = previous; }
    }

    [Test]
    public void Currency_input_parses_symbols_grouping_parentheses_and_long_inputs()
    {
        (string Culture, string Text, decimal Expected)[] cases =
        [
            ("en-US", " $1,234.56 ", 1234.56m),
            ("de-DE", "1.234,56 €", 1234.56m),
            ("fr-FR", "1\u202f234,56 €", 1234.56m),
            ("en-US", "($1,234.56)", -1234.56m),
            ("en-US", "1" + new string(' ', 300) + "234.567", 1234.57m)
        ];
        foreach (var (culture, text, expected) in cases)
        {
            var cut = Render<CurrencyInput>(p => p.Add(c => c.CultureName, culture));
            cut.Find("input").Input(text);
            cut.Instance.Value.Should().Be(expected);
        }
    }

    [Test]
    public void Currency_input_preserves_invalid_values_and_clamps_valid_values()
    {
        var cut = Render<CurrencyInput>(p => p.Add(c => c.CultureName, "en-US")
            .Add(c => c.Value, 10m).Add(c => c.AllowNegative, false).Add(c => c.Max, 20m));
        cut.Find("input").Input("($12)");
        cut.Instance.Value.Should().Be(10m);
        cut.Find("input").Input("invalid");
        cut.Instance.Value.Should().Be(10m);
        cut.Find("input").Input("$100");
        cut.Instance.Value.Should().Be(20m);
        cut.Find("input").Input("  ");
        cut.Instance.Value.Should().BeNull();
    }

    [Test]
    public void Currency_input_supports_empty_group_separators_and_ordered_multi_character_tokens()
    {
        CultureInfo previous = CultureInfo.CurrentCulture;
        var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
        culture.NumberFormat.CurrencyGroupSeparator = "";
        culture.NumberFormat.NumberGroupSeparator = "";
        culture.NumberFormat.CurrencyDecimalSeparator = "::";
        culture.NumberFormat.NumberDecimalSeparator = ":";
        culture.NumberFormat.CurrencySymbol = "USD";
        try
        {
            CultureInfo.CurrentCulture = culture;
            var cut = Render<CurrencyInput>(p => p.Add(c => c.Symbol, "US"));
            cut.Find("input").Input("US12::34");
            cut.Instance.Value.Should().Be(12.34m);
            // Removing the explicit symbol first leaves D, so this remains invalid.
            cut.Find("input").Input("USD56::78");
            cut.Instance.Value.Should().Be(12.34m);
        }
        finally { CultureInfo.CurrentCulture = previous; }
    }
}
