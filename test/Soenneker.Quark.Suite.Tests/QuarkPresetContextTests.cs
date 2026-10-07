using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public class QuarkPresetContextTests
{
    [Test]
    public void Frozen_preset_runs_once_preserves_nulls_and_cannot_be_mutated_through_the_callback()
    {
        int calls = 0;
        QuarkPresetContext? retained = null;
        var frozen = QuarkPresetToken.Freeze("frozen", c =>
        {
            calls++;
            retained = c;
            c.Width = Width.IsFull;
            c.Height = null;
            c.Padding = default(CssValue<PaddingBuilder>);
        });
        retained!.Width = Width.Is0;
        var context = new QuarkPresetContext { Height = Height.IsFull, Margin = Margin.Is2 };
        frozen.Apply(context);
        frozen.Apply(context);
        calls.Should().Be(1);
        context.Width.Should().Be((CssValue<WidthBuilder>)Width.IsFull);
        context.Height.Should().BeNull();
        context.Padding.HasValue.Should().BeTrue();
        context.Padding.GetValueOrDefault().IsEmpty.Should().BeTrue();
        context.Margin.Should().Be((CssValue<MarginBuilder>)Margin.Is2);
    }

    [Test]
    public void ComposedPresets_CanOverwriteAndClearValues()
    {
        var context = new QuarkPresetContext();
        var first = new QuarkPresetToken("first", static c => { c.Width = Width.Token("[120px]"); c.Height = Height.Token("[80px]"); });
        var second = new QuarkPresetToken("second", static c => { c.Width = Width.Token("[240px]"); c.Height = null; });
        first.Apply(context);
        second.Apply(context);
        context.Width.Should().Be((CssValue<WidthBuilder>) Width.Token("[240px]"));
        context.Height.Should().BeNull();
        context.Padding.Should().BeNull();
    }

    [Test]
    public void EmptyValue_RemainsDistinctFromUnsetValue()
    {
        var context = new QuarkPresetContext { Width = default(CssValue<WidthBuilder>) };
        context.Width.HasValue.Should().BeTrue();
        context.Width.Value.IsEmpty.Should().BeTrue();
        context.Width = null;
        context.Width.Should().BeNull();
    }

    [Test]
    public void ApplyingCallbackAgain_ObservesChangedCapturedState()
    {
        CssValue<WidthBuilder> width = Width.Token("[120px]");
        var token = new QuarkPresetToken("dynamic", c => c.Width = width);
        var first = new QuarkPresetContext();
        token.Apply(first);
        width = Width.Token("[240px]");
        var second = new QuarkPresetContext();
        token.Apply(second);
        first.Width.Should().Be((CssValue<WidthBuilder>) Width.Token("[120px]"));
        second.Width.Should().Be((CssValue<WidthBuilder>) Width.Token("[240px]"));
    }
}
