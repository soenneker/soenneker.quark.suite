using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Tree_removing_the_current_item_makes_the_remaining_item_tabbable()
    {
        var showFirst = true;
        RenderFragment items = builder =>
        {
            if (showFirst)
            {
                builder.OpenComponent<TreeItem>(0);
                builder.SetKey("first");
                builder.AddAttribute(1, "Id", "first");
                builder.CloseComponent();
            }
            builder.OpenComponent<TreeItem>(2);
            builder.SetKey("second");
            builder.AddAttribute(3, "Id", "second");
            builder.CloseComponent();
        };
        var cut = Render<Tree>(p => p.Add(c => c.ChildContent, items));
        cut.Find("#first").GetAttribute("tabindex").Should().Be("0");
        showFirst = false;
        await cut.InvokeAsync(cut.Instance.Refresh);
        cut.FindAll("#first").Should().BeEmpty();
        cut.WaitForAssertion(() => cut.Find("#second").GetAttribute("tabindex").Should().Be("0"));
    }

    [Test]
    public void AutoSaveStatus_animates_only_while_saving()
    {
        var cut = Render<AutoSaveStatus>();
        cut.FindAll(".animate-spin").Should().BeEmpty();
        cut.Render(p => p.Add(c => c.State, AutoSaveState.Saving));
        cut.FindAll(".animate-spin").Should().ContainSingle();
        cut.Render(p => p.Add(c => c.State, AutoSaveState.Saved).Add(c => c.HasSaved, true));
        cut.FindAll(".animate-spin").Should().BeEmpty();
    }
}
