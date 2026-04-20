using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Command_slots_match_shadcn_base_classes()
    {
        var cut = Render<Command>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<CommandInput>(0);
                builder.CloseComponent();

                builder.OpenComponent<CommandList>(1);
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(listBuilder =>
                {
                    listBuilder.OpenComponent<CommandGroup>(0);
                    listBuilder.AddAttribute(1, "Heading", "Suggestions");
                    listBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(groupBuilder =>
                    {
                        groupBuilder.OpenComponent<CommandItem>(0);
                        groupBuilder.AddAttribute(1, "SearchValue", "Calendar");
                        groupBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(itemBuilder =>
                        {
                            itemBuilder.AddContent(0, "Calendar");
                            itemBuilder.OpenComponent<CommandShortcut>(1);
                            itemBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(shortcutBuilder => shortcutBuilder.AddContent(0, "⌘K")));
                            itemBuilder.CloseComponent();
                        }));
                        groupBuilder.CloseComponent();
                    }));
                    listBuilder.CloseComponent();

                    listBuilder.OpenComponent<CommandSeparator>(3);
                    listBuilder.CloseComponent();
                }));
                builder.CloseComponent();

                builder.OpenComponent<CommandEmpty>(4);
                builder.AddAttribute(5, "ChildContent", (RenderFragment)(emptyBuilder => emptyBuilder.AddContent(0, "No results found.")));
                builder.CloseComponent();
            })));

        var commandClasses = cut.Find("[data-slot='command']").GetAttribute("class")!;
        var wrapperClasses = cut.Find("[data-slot='command-input-wrapper']").GetAttribute("class")!;
        var inputGroupClasses = cut.Find("[data-slot='input-group']").GetAttribute("class")!;
        var inputClasses = cut.Find("[data-slot='command-input']").GetAttribute("class")!;
        var listClasses = cut.Find("[data-slot='command-list']").GetAttribute("class")!;
        var groupClasses = cut.Find("[data-slot='command-group']").GetAttribute("class")!;
        var itemClasses = cut.Find("[data-slot='command-item']").GetAttribute("class")!;
        var shortcutClasses = cut.Find("[data-slot='command-shortcut']").GetAttribute("class")!;
        var separatorClasses = cut.Find("[data-slot='command-separator']").GetAttribute("class")!;

        commandClasses.Should().Contain("flex");
        commandClasses.Should().Contain("size-full");
        commandClasses.Should().Contain("flex-col");
        commandClasses.Should().Contain("overflow-hidden");
        commandClasses.Should().Contain("rounded-xl!");
        commandClasses.Should().Contain("max-w-sm");
        commandClasses.Should().Contain("rounded-lg");
        commandClasses.Should().Contain("border");
        commandClasses.Should().Contain("bg-popover");
        commandClasses.Should().Contain("p-1");
        commandClasses.Should().Contain("text-popover-foreground");
        commandClasses.Should().NotContain("q-command");

        wrapperClasses.Should().Contain("p-1");
        wrapperClasses.Should().Contain("pb-0");

        inputGroupClasses.Should().Contain("group/input-group");
        inputGroupClasses.Should().Contain("relative");
        inputGroupClasses.Should().Contain("flex");
        inputGroupClasses.Should().Contain("h-8");
        inputGroupClasses.Should().Contain("w-full");
        inputGroupClasses.Should().Contain("min-w-0");
        inputGroupClasses.Should().Contain("items-center");
        inputGroupClasses.Should().Contain("rounded-lg");
        inputGroupClasses.Should().Contain("border");
        inputGroupClasses.Should().Contain("border-input/30");
        inputGroupClasses.Should().Contain("bg-input/30");
        inputGroupClasses.Should().Contain("in-data-[slot=combobox-content]:focus-within:border-inherit");
        inputGroupClasses.Should().Contain("has-disabled:bg-input/50");
        inputGroupClasses.Should().Contain("has-[[data-slot=input-group-control]:focus-visible]:ring-3");
        inputGroupClasses.Should().Contain("*:data-[slot=input-group-addon]:pl-2!");

        inputClasses.Should().Contain("w-full");
        inputClasses.Should().Contain("text-sm");
        inputClasses.Should().Contain("outline-hidden");
        inputClasses.Should().Contain("disabled:cursor-not-allowed");
        inputClasses.Should().NotContain("bg-transparent");
        inputClasses.Should().NotContain("placeholder:text-muted-foreground");
        inputClasses.Should().NotContain("q-command-input");

        cut.Find("[data-slot='input-group-addon']").GetAttribute("class")!.Should().Contain("group-data-[disabled=true]/input-group:opacity-50");
        cut.Find("[data-slot='input-group-addon']").GetAttribute("class")!.Should().Contain("has-[>button]:ml-[-0.3rem]");

        listClasses.Should().Contain("no-scrollbar");
        listClasses.Should().Contain("max-h-72");
        listClasses.Should().Contain("scroll-py-1");
        listClasses.Should().Contain("overflow-x-hidden");
        listClasses.Should().Contain("overflow-y-auto");
        listClasses.Should().Contain("outline-none");
        listClasses.Should().NotContain("q-command-list");

        groupClasses.Should().Contain("overflow-hidden");
        groupClasses.Should().Contain("p-1");
        groupClasses.Should().Contain("text-foreground");
        groupClasses.Should().Contain("**:[[cmdk-group-heading]]:px-2");
        groupClasses.Should().Contain("**:[[cmdk-group-heading]]:py-1.5");
        groupClasses.Should().Contain("**:[[cmdk-group-heading]]:text-xs");
        groupClasses.Should().Contain("**:[[cmdk-group-heading]]:font-medium");
        groupClasses.Should().Contain("**:[[cmdk-group-heading]]:text-muted-foreground");
        groupClasses.Should().NotContain("q-command-group");

        itemClasses.Should().Contain("group/command-item");
        itemClasses.Should().Contain("relative");
        itemClasses.Should().Contain("flex");
        itemClasses.Should().Contain("cursor-default");
        itemClasses.Should().Contain("items-center");
        itemClasses.Should().Contain("gap-2");
        itemClasses.Should().Contain("rounded-sm");
        itemClasses.Should().Contain("px-2");
        itemClasses.Should().Contain("py-1.5");
        itemClasses.Should().Contain("text-sm");
        itemClasses.Should().Contain("outline-hidden");
        itemClasses.Should().Contain("select-none");
        itemClasses.Should().Contain("data-selected:bg-muted");
        itemClasses.Should().Contain("data-selected:text-foreground");
        itemClasses.Should().Contain("[&_svg]:pointer-events-none");
        itemClasses.Should().Contain("data-selected:*:[svg]:text-foreground");
        itemClasses.Should().NotContain("q-command-item");

        shortcutClasses.Should().Contain("ml-auto");
        shortcutClasses.Should().Contain("text-xs");
        shortcutClasses.Should().Contain("tracking-widest");
        shortcutClasses.Should().Contain("text-muted-foreground");
        shortcutClasses.Should().Contain("group-data-selected/command-item:text-foreground");
        shortcutClasses.Should().NotContain("q-command-shortcut");

        separatorClasses.Should().Contain("-mx-1");
        separatorClasses.Should().Contain("h-px");
        separatorClasses.Should().Contain("bg-border");
        separatorClasses.Should().NotContain("q-command-separator");
    }
}
