using System;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    [Arguments(typeof(Switch), true)]
    [Arguments(typeof(Switch), false)]
    [Arguments(typeof(Check), true)]
    [Arguments(typeof(Check), false)]
    [Arguments(typeof(Radio), true)]
    [Arguments(typeof(Radio), false)]
    public void Field_labels_target_toggle_controls_with_explicit_or_generated_ids(Type controlType, bool explicitId)
    {
        var cut = Render<Field>(p => p.AddChildContent(builder =>
        {
            builder.OpenComponent<FieldLabel>(0);
            if (explicitId)
                builder.AddAttribute(1, nameof(FieldLabel.For), "notifications");
            builder.AddAttribute(2, nameof(FieldLabel.ChildContent), (RenderFragment)(child => child.AddContent(0, "Notifications")));
            builder.CloseComponent();
            builder.OpenComponent(3, controlType);
            if (explicitId)
                builder.AddAttribute(4, "Id", "notifications");
            builder.CloseComponent();
        }));

        var targetId = cut.Find("label").GetAttribute("for");
        targetId.Should().NotBeNullOrWhiteSpace();
        cut.Find($"#{targetId}").Matches("input[type='radio'], button[role='switch'], button[role='checkbox']").Should().BeTrue();
    }
}
