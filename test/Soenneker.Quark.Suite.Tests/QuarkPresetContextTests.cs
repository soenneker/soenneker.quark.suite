using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public class QuarkPresetContextTests
{
    [Test]
    public void ComposedPresets_CanOverwriteAndClearValues()
    {
        var context = new QuarkPresetContext();
        var first = new QuarkPresetToken("first", static c => { c.Width = 120; c.Height = 80; });
        var second = new QuarkPresetToken("second", static c => { c.Width = 240; c.Height = null; });
        first.Apply(context);
        second.Apply(context);
        context.Width.Should().Be((CssValue<WidthBuilder>) 240);
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
        var width = 120;
        var token = new QuarkPresetToken("dynamic", c => c.Width = width);
        var first = new QuarkPresetContext();
        token.Apply(first);
        width = 240;
        var second = new QuarkPresetContext();
        token.Apply(second);
        first.Width.Should().Be((CssValue<WidthBuilder>) 120);
        second.Width.Should().Be((CssValue<WidthBuilder>) 240);
    }
}
