using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Tests;

public sealed class CommandOrderTests : BunitContext
{
    public CommandOrderTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddBradixSuiteAsScoped();
        Services.AddDefaultQuarkOptionsAsScoped();
        ComponentFactories.AddStub<Icon>();
    }

    [Test]
    public async Task Search_order_is_stable_and_invalidates_for_mutable_keywords_and_groups()
    {
        var command = Render<Command>();
        string[] keywords = ["find"];
        var first = Render<CommandItem>(p => p.Add(x => x.SearchValue, "z").Add(x => x.Keywords, keywords));
        var second = Render<CommandItem>(p => p.Add(x => x.SearchValue, "a find"));
        var duplicate = Render<CommandItem>(p => p.Add(x => x.SearchValue, "a find"));
        var group = Render<CommandGroup>(p => p.Add(x => x.Heading, "one"));
        var sameGroup = Render<CommandGroup>(p => p.Add(x => x.Heading, "one"));
        var owner = command.Instance;
        await command.InvokeAsync(() =>
        {
            owner.RegisterItem(first.Instance, group.Instance);
            owner.RegisterItem(second.Instance, sameGroup.Instance);
            owner.RegisterItem(duplicate.Instance, sameGroup.Instance);
            return owner.SetSearch("find");
        });

        owner.GetItemOrder(second.Instance).Should().Be(0);
        owner.GetItemOrder(duplicate.Instance).Should().Be(1);
        owner.GetItemOrder(first.Instance).Should().Be(2);

        keywords[0] = "absent";
        first.Render(p => p.Add(x => x.Keywords, keywords));
        owner.UpdateItemGroup(first.Instance, group.Instance);
        owner.GetItemOrder(first.Instance).Should().BeNull();
        owner.GetItemOrder(duplicate.Instance).Should().Be(1);

        owner.UpdateItemGroup(second.Instance, null);
        owner.GetItemOrder(second.Instance).Should().Be(0);
        owner.GetItemOrder(duplicate.Instance).Should().Be(0);

        await command.InvokeAsync(() => owner.SetSearch(null));
        owner.GetItemOrder(duplicate.Instance).Should().BeNull();
    }
}
