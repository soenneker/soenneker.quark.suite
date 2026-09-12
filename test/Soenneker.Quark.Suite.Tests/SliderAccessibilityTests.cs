using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Web;
using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Slider_readonly_preserves_value_for_keyboard_and_pointer_input()
    {
        var changes = 0;
        var cut = Render<Slider>(p => p.Add(c => c.ReadOnly, true)
            .Add(c => c.SliderValue, 30)
            .Add(c => c.SliderValueChanged, _ => changes++)
            .Add(c => c.OnChange, _ => changes++));

        var thumb = cut.Find("[role='slider']");
        thumb.GetAttribute("aria-readonly").Should().Be("true");
        thumb.GetAttribute("tabindex").Should().Be("0");
        await thumb.KeyDownAsync(new KeyboardEventArgs { Key = "ArrowRight" });
        var primitive = cut.FindComponent<BradixSlider>();
        await cut.InvokeAsync(() => primitive.Instance.HandlePointerStart(0.8, 0.5, -1));
        await cut.InvokeAsync(() => primitive.Instance.HandlePointerEnd());

        cut.Find("[role='slider']").GetAttribute("aria-valuenow").Should().Be("30");
        changes.Should().Be(0);
    }

    [Test]
    public void Slider_explicit_label_and_description_reach_focusable_thumb()
    {
        var cut = Render<Slider>(p => p.Add(c => c.AriaLabel, "Volume")
            .Add(c => c.AriaDescribedBy, "volume-help"));

        var thumb = cut.Find("[role='slider']");
        thumb.GetAttribute("aria-label").Should().Be("Volume");
        thumb.GetAttribute("aria-describedby").Should().Be("volume-help");
    }

    [Test]
    public void Slider_range_thumbs_keep_context_in_their_names()
    {
        var cut = Render<Slider>(p => p.Add(c => c.Values, new double[] { 20, 80 })
            .Add(c => c.AriaLabel, "Price"));

        var thumbs = cut.FindAll("[role='slider']");
        thumbs[0].GetAttribute("aria-label").Should().Be("Price Minimum");
        thumbs[1].GetAttribute("aria-label").Should().Be("Price Maximum");
    }

    [Test]
    public void Slider_range_forwards_label_references_and_descriptions_to_every_thumb()
    {
        var cut = Render<Slider>(p => p.Add(c => c.Values, new double[] { 20, 80 })
            .Add(c => c.AriaLabelledBy, "price-label")
            .Add(c => c.Attributes, new Dictionary<string, object> { ["aria-describedby"] = "price-help" }));

        foreach (var thumb in cut.FindAll("[role='slider']"))
        {
            thumb.GetAttribute("aria-labelledby").Should().Be("price-label");
            thumb.GetAttribute("aria-describedby").Should().Be("price-help");
        }
    }
}
