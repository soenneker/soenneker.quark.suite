using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
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
        var inputClasses = cut.Find("[data-slot='command-input']").GetAttribute("class")!;
        var listClasses = cut.Find("[data-slot='command-list']").GetAttribute("class")!;
        var groupClasses = cut.Find("[data-slot='command-group']").GetAttribute("class")!;
        var itemClasses = cut.Find("[data-slot='command-item']").GetAttribute("class")!;
        var shortcutClasses = cut.Find("[data-slot='command-shortcut']").GetAttribute("class")!;
        var separatorClasses = cut.Find("[data-slot='command-separator']").GetAttribute("class")!;

        commandClasses.Should().Contain("flex");
        commandClasses.Should().Contain("h-full");
        commandClasses.Should().Contain("w-full");
        commandClasses.Should().Contain("flex-col");
        commandClasses.Should().Contain("overflow-hidden");
        commandClasses.Should().Contain("rounded-xl!");
        commandClasses.Should().Contain("bg-popover");
        commandClasses.Should().Contain("p-1");
        commandClasses.Should().Contain("text-popover-foreground");
        commandClasses.Should().NotContain("rounded-md");
        commandClasses.Should().NotContain("size-full");
        commandClasses.Should().NotContain("max-w-sm");
        commandClasses.Should().NotContain("border");
        commandClasses.Should().NotContain("q-command");

        wrapperClasses.Should().Contain("p-1");
        wrapperClasses.Should().Contain("pb-0");
        wrapperClasses.Should().NotContain("border-b");
        wrapperClasses.Should().NotContain("px-3");

        var inputGroupClasses = cut.Find("[data-slot='input-group']").GetAttribute("class")!;
        inputGroupClasses.Should().Contain("group/input-group");
        inputGroupClasses.Should().Contain("h-8!");
        inputGroupClasses.Should().Contain("rounded-lg!");
        inputGroupClasses.Should().Contain("border-input/30");
        inputGroupClasses.Should().Contain("bg-input/30");
        inputGroupClasses.Should().Contain("shadow-none!");

        var searchIconClasses = cut.Find("[data-slot='input-group-addon'] [data-slot='icon']").GetAttribute("class")!;
        searchIconClasses.Should().Contain("size-4");
        searchIconClasses.Should().NotContain("opacity-50");

        inputClasses.Should().Contain("w-full");
        inputClasses.Should().Contain("text-sm");
        inputClasses.Should().Contain("outline-hidden");
        inputClasses.Should().Contain("disabled:cursor-not-allowed");
        inputClasses.Should().Contain("disabled:opacity-50");
        inputClasses.Should().NotContain("flex");
        inputClasses.Should().NotContain("bg-transparent");
        inputClasses.Should().NotContain("placeholder:text-muted-foreground");
        inputClasses.Should().NotContain("h-10");
        inputClasses.Should().NotContain("rounded-md");
        inputClasses.Should().NotContain("py-3");
        inputClasses.Should().NotContain("q-command-input");

        listClasses.Should().Contain("max-h-[300px]");
        listClasses.Should().Contain("scroll-py-1");
        listClasses.Should().Contain("overflow-x-hidden");
        listClasses.Should().Contain("overflow-y-auto");
        listClasses.Should().NotContain("no-scrollbar");
        listClasses.Should().NotContain("q-command-list");

        groupClasses.Should().Contain("overflow-hidden");
        groupClasses.Should().Contain("p-1");
        groupClasses.Should().Contain("text-foreground");
        groupClasses.Should().Contain("[&_[cmdk-group-heading]]:px-2");
        groupClasses.Should().Contain("[&_[cmdk-group-heading]]:py-1.5");
        groupClasses.Should().Contain("[&_[cmdk-group-heading]]:text-xs");
        groupClasses.Should().Contain("[&_[cmdk-group-heading]]:font-medium");
        groupClasses.Should().Contain("[&_[cmdk-group-heading]]:text-muted-foreground");
        cut.Find("[cmdk-group-heading]").TextContent.Should().Be("Suggestions");
        groupClasses.Should().NotContain("q-command-group");

        itemClasses.Should().Contain("relative");
        itemClasses.Should().Contain("group/command-item");
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
        itemClasses.Should().Contain("data-[selected=true]:bg-muted");
        itemClasses.Should().Contain("data-[selected=true]:text-foreground");
        itemClasses.Should().NotContain("data-[selected=true]:bg-accent");
        itemClasses.Should().NotContain("data-[selected=true]:text-accent-foreground");
        itemClasses.Should().Contain("[&_svg]:pointer-events-none");
        itemClasses.Should().Contain("data-selected:*:[svg]:text-foreground");
        itemClasses.Should().NotContain("q-command-item");

        shortcutClasses.Should().Contain("ml-auto");
        shortcutClasses.Should().Contain("text-xs");
        shortcutClasses.Should().Contain("tracking-widest");
        shortcutClasses.Should().Contain("text-muted-foreground");
        shortcutClasses.Should().NotContain("group-data-selected/command-item:text-foreground");
        shortcutClasses.Should().NotContain("q-command-shortcut");

        separatorClasses.Should().Contain("-mx-1");
        separatorClasses.Should().Contain("h-px");
        separatorClasses.Should().Contain("bg-border");
        separatorClasses.Should().NotContain("q-command-separator");
    }

    [Test]
    public void Command_source_matches_shadcn_v4_contract()
    {
        var input = ReadCommandSource("CommandInput.razor");
        var item = ReadCommandSource("CommandItem.razor");
        var dialog = ReadCommandSource("CommandDialog.razor");

        input.Should().Contain("attrs[\"data-slot\"] = \"command-input-wrapper\"");
        input.Should().Contain("LucideIcon.Search");
        input.Should().Contain("attrs[\"data-slot\"] = \"input-group\"");

        item.Should().Contain("data-selected");
        item.Should().Contain("data-[selected=true]:bg-muted");
        item.Should().Contain("group/command-item");
        item.Should().Contain("data-[disabled=true]:pointer-events-none");

        dialog.Should().Contain("DialogContent ShowCloseButton=\"@ShowCloseButton\" Class=\"overflow-hidden p-0\"");
        dialog.Should().Contain("[&_[data-slot=command-input-wrapper]]:h-12");
        dialog.Should().Contain("DialogTitle Class=\"sr-only\"");
        dialog.Should().Contain("DialogDescription Class=\"sr-only\"");
        dialog.Should().NotContain("DialogBody Class=\"overflow-hidden p-0\"");
    }

    private static string ReadCommandSource(string fileName)
    {
        return File.ReadAllText(Path.Combine(GetSuiteRootForCommand(), "src", "Soenneker.Quark.Suite", "Components", "Command", fileName));
    }

    private static string GetSuiteRootForCommand()
    {
        var directory = AppContext.BaseDirectory;

        for (var i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }
}
