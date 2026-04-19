using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Blazor.MockJsRuntime.Registrars;
using Soenneker.Bradix;
using Soenneker.Quark.Gen.Lucide.Abstractions;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed class RenderedShadcnParityTests : BunitContext
{
    public RenderedShadcnParityTests()
    {
        var module = JSInterop.SetupModule("./_content/Soenneker.Bradix.Suite/js/bradix.js");
        module.SetupVoid("mountPortal", _ => true).SetVoidResult();
        module.SetupVoid("unmountPortal", _ => true).SetVoidResult();
        module.SetupVoid("registerAvatarImageLoadingStatus", _ => true).SetVoidResult();
        module.SetupVoid("unregisterAvatarImageLoadingStatus", _ => true).SetVoidResult();
        module.SetupVoid("registerDelegatedInteraction", _ => true).SetVoidResult();
        module.SetupVoid("unregisterDelegatedInteraction", _ => true).SetVoidResult();
        module.SetupVoid("registerLabelTextSelectionGuard", _ => true).SetVoidResult();
        module.SetupVoid("unregisterLabelTextSelectionGuard", _ => true).SetVoidResult();
        module.SetupVoid("registerPresence", _ => true).SetVoidResult();
        module.SetupVoid("unregisterPresence", _ => true).SetVoidResult();
        module.SetupVoid("registerRovingFocusNavigationKeys", _ => true).SetVoidResult();
        module.SetupVoid("unregisterRovingFocusNavigationKeys", _ => true).SetVoidResult();
        module.SetupVoid("observeCollapsibleContent", _ => true).SetVoidResult();
        module.SetupVoid("unobserveCollapsibleContent", _ => true).SetVoidResult();
        module.SetupVoid("registerDismissableLayerBranch", _ => true).SetVoidResult();
        module.SetupVoid("unregisterDismissableLayerBranch", _ => true).SetVoidResult();
        module.SetupVoid("registerSelectBubbleInput", _ => true).SetVoidResult();
        module.SetupVoid("unregisterSelectBubbleInput", _ => true).SetVoidResult();
        module.SetupVoid("syncSelectBubbleInputValue", _ => true).SetVoidResult();
        module.SetupVoid("registerMenubarDocumentDismiss", _ => true).SetVoidResult();
        module.SetupVoid("unregisterMenubarDocumentDismiss", _ => true).SetVoidResult();
        module.SetupVoid("registerNavigationMenuTriggerInteraction", _ => true).SetVoidResult();
        module.SetupVoid("unregisterNavigationMenuTriggerInteraction", _ => true).SetVoidResult();
        module.SetupVoid("registerSliderPointerBridge", _ => true).SetVoidResult();
        module.SetupVoid("unregisterSliderPointerBridge", _ => true).SetVoidResult();
        module.SetupVoid("syncInputValue", _ => true).SetVoidResult();
        module.SetupVoid("registerAssociatedFormReset", _ => true).SetVoidResult();
        module.SetupVoid("unregisterAssociatedFormReset", _ => true).SetVoidResult();
        module.SetupVoid("registerOneTimePasswordInput", _ => true).SetVoidResult();
        module.SetupVoid("unregisterOneTimePasswordInput", _ => true).SetVoidResult();
        module.Setup<bool>("isDirectionRtl", _ => true).SetResult(false);

        JSInterop.SetupVoid("registerDismissableLayerBranch", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterDismissableLayerBranch", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerSelectBubbleInput", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterSelectBubbleInput", _ => true).SetVoidResult();
        JSInterop.SetupVoid("syncSelectBubbleInputValue", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerMenubarDocumentDismiss", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterMenubarDocumentDismiss", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerNavigationMenuTriggerInteraction", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterNavigationMenuTriggerInteraction", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerSliderPointerBridge", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterSliderPointerBridge", _ => true).SetVoidResult();
        JSInterop.SetupVoid("syncInputValue", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerAssociatedFormReset", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterAssociatedFormReset", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerOneTimePasswordInput", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterOneTimePasswordInput", _ => true).SetVoidResult();
        JSInterop.Setup<bool>("isDirectionRtl", _ => true).SetResult(false);

        Services.AddMockJsRuntimeAsScoped();
        Services.AddBradixSuiteAsScoped();
        Services.AddDefaultQuarkOptionsAsScoped();
        Services.AddScoped<ILucideIconSvgProvider, FakeLucideIconSvgProvider>();
        Services.AddScoped<ICollapseCoordinator, CollapseCoordinator>();
    }

    private sealed class FakeLucideIconSvgProvider : ILucideIconSvgProvider
    {
        public string? GetSvg(string iconName)
        {
            return "<svg viewBox=\"0 0 24 24\" aria-hidden=\"true\"></svg>";
        }
    }

    [Fact]
    public void FieldLabel_matches_shadcn_base_classes()
    {
        var cut = Render<FieldLabel>(parameters => parameters
            .Add(p => p.For, "email")
            .Add(p => p.ChildContent, "Email"));

        string classes = cut.Find("[data-slot='field-label']").GetAttribute("class")!;

        classes.Should().Contain("items-center");
        classes.Should().Contain("group/field-label");
        classes.Should().Contain("peer/field-label");
        classes.Should().Contain("has-data-checked:border-primary/30");
        classes.Should().Contain("has-[>[data-slot=field]]:rounded-lg");
        classes.Should().Contain("*:data-[slot=field]:p-2.5");
        classes.Should().NotContain("has-data-[state=checked]");
        classes.Should().NotContain("rounded-md");
        classes.Should().NotContain("p-4");
    }

    [Fact]
    public void AlertTitle_matches_shadcn_base_classes()
    {
        var cut = Render<AlertTitle>(parameters => parameters
            .Add(p => p.ChildContent, "Alert"));

        string classes = cut.Find("[data-slot='alert-title']").GetAttribute("class")!;

        classes.Should().Contain("font-medium");
        classes.Should().Contain("group-has-[>svg]/alert:col-start-2");
        classes.Should().Contain("[&_a]:underline");
        classes.Should().Contain("[&_a]:underline-offset-3");
        classes.Should().NotContain("q-alert-title");
    }

    [Fact]
    public void AlertDescription_matches_shadcn_base_classes()
    {
        var cut = Render<AlertDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Body"));

        string classes = cut.Find("[data-slot='alert-description']").GetAttribute("class")!;

        classes.Should().Contain("text-sm");
        classes.Should().Contain("text-balance");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().Contain("md:text-pretty");
        classes.Should().Contain("[&_a]:underline-offset-3");
        classes.Should().NotContain("q-alert-description");
        classes.Should().NotContain("col-start-2");
    }

    [Fact]
    public void Card_slots_match_shadcn_base_classes()
    {
        var header = Render<CardHeader>(parameters => parameters
            .Add(p => p.ChildContent, "Header"));
        var title = Render<CardTitle>(parameters => parameters
            .Add(p => p.ChildContent, "Title"));
        var description = Render<CardDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Description"));

        string headerClasses = header.Find("[data-slot='card-header']").GetAttribute("class")!;
        string titleClasses = title.Find("[data-slot='card-title']").GetAttribute("class")!;
        string descriptionClasses = description.Find("[data-slot='card-description']").GetAttribute("class")!;

        headerClasses.Should().Contain("group/card-header");
        headerClasses.Should().Contain("@container/card-header");
        headerClasses.Should().Contain("gap-1");
        headerClasses.Should().Contain("px-4");
        headerClasses.Should().Contain("rounded-t-xl");
        headerClasses.Should().Contain("has-data-[slot=card-description]:grid-rows-[auto_auto]");
        headerClasses.Should().NotContain("q-card-header");

        titleClasses.Should().Contain("cn-font-heading");
        titleClasses.Should().Contain("text-base");
        titleClasses.Should().Contain("leading-snug");
        titleClasses.Should().Contain("font-medium");
        titleClasses.Should().Contain("group-data-[size=sm]/card:text-sm");
        titleClasses.Should().NotContain("q-card-title");

        descriptionClasses.Should().Contain("text-sm");
        descriptionClasses.Should().Contain("text-muted-foreground");
        descriptionClasses.Should().NotContain("q-card-description");
    }

    [Fact]
    public void Empty_slots_match_shadcn_base_classes()
    {
        var header = Render<EmptyHeader>(parameters => parameters
            .Add(p => p.ChildContent, "Header"));
        var title = Render<EmptyTitle>(parameters => parameters
            .Add(p => p.ChildContent, "Title"));
        var description = Render<EmptyDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Description"));

        string headerClasses = header.Find("[data-slot='empty-header']").GetAttribute("class")!;
        string titleClasses = title.Find("[data-slot='empty-title']").GetAttribute("class")!;
        string descriptionClasses = description.Find("[data-slot='empty-description']").GetAttribute("class")!;

        headerClasses.Should().Contain("flex");
        headerClasses.Should().Contain("max-w-sm");
        headerClasses.Should().Contain("flex-col");
        headerClasses.Should().Contain("items-center");
        headerClasses.Should().Contain("gap-2");
        headerClasses.Should().NotContain("q-empty-header");

        titleClasses.Should().Contain("cn-font-heading");
        titleClasses.Should().Contain("text-sm");
        titleClasses.Should().Contain("font-medium");
        titleClasses.Should().Contain("tracking-tight");

        descriptionClasses.Should().Contain("text-sm");
        descriptionClasses.Should().Contain("leading-relaxed");
        descriptionClasses.Should().Contain("text-muted-foreground");
        descriptionClasses.Should().Contain("[&>a]:underline-offset-4");
    }

    [Fact]
    public void Item_and_text_slots_match_shadcn_base_classes()
    {
        var item = Render<Item>(parameters => parameters
            .Add(p => p.Variant, ItemVariant.Outline)
            .Add(p => p.ChildContent, "Item"));
        var itemXs = Render<Item>(parameters => parameters
            .Add(p => p.Variant, ItemVariant.Outline)
            .Add(p => p.Size, ItemSize.ExtraSmall)
            .Add(p => p.ChildContent, "Item"));
        var title = Render<ItemTitle>(parameters => parameters
            .Add(p => p.ChildContent, "Title"));
        var description = Render<ItemDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Description"));

        string itemClasses = item.Find("[data-slot='item']").GetAttribute("class")!;
        string itemXsClasses = itemXs.Find("[data-slot='item']").GetAttribute("class")!;
        string titleClasses = title.Find("[data-slot='item-title']").GetAttribute("class")!;
        string descriptionClasses = description.Find("[data-slot='item-description']").GetAttribute("class")!;

        itemClasses.Should().Contain("group/item");
        itemClasses.Should().Contain("flex");
        itemClasses.Should().Contain("w-full");
        itemClasses.Should().Contain("flex-wrap");
        itemClasses.Should().Contain("items-center");
        itemClasses.Should().Contain("rounded-lg");
        itemClasses.Should().Contain("border");
        itemClasses.Should().Contain("text-sm");
        itemClasses.Should().Contain("duration-100");
        itemClasses.Should().Contain("[a]:hover:bg-muted");
        itemClasses.Should().Contain("border-border");
        itemClasses.Should().Contain("gap-2.5");
        itemClasses.Should().Contain("px-3");
        itemClasses.Should().Contain("py-2.5");
        itemClasses.Should().NotContain("q-item");

        itemXsClasses.Should().Contain("gap-2");
        itemXsClasses.Should().Contain("px-2.5");
        itemXsClasses.Should().Contain("py-2");
        itemXsClasses.Should().Contain("in-data-[slot=dropdown-menu-content]:p-0");

        titleClasses.Should().Contain("line-clamp-1");
        titleClasses.Should().Contain("flex");
        titleClasses.Should().Contain("w-fit");
        titleClasses.Should().Contain("items-center");
        titleClasses.Should().Contain("gap-2");
        titleClasses.Should().Contain("text-sm");
        titleClasses.Should().Contain("leading-snug");
        titleClasses.Should().Contain("font-medium");
        titleClasses.Should().Contain("underline-offset-4");

        descriptionClasses.Should().Contain("line-clamp-2");
        descriptionClasses.Should().Contain("text-sm");
        descriptionClasses.Should().Contain("leading-normal");
        descriptionClasses.Should().Contain("font-normal");
        descriptionClasses.Should().Contain("text-muted-foreground");
        descriptionClasses.Should().Contain("group-data-[size=xs]/item:text-xs");
        descriptionClasses.Should().Contain("[&>a]:underline-offset-4");
        descriptionClasses.Should().NotContain("q-item-description");
    }

    [Fact]
    public void DialogDescription_matches_shadcn_base_classes()
    {
        var cut = Render<DialogDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Description"));

        string classes = cut.Find("[data-slot='dialog-description']").GetAttribute("class")!;

        classes.Should().Contain("text-sm");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().Contain("*:[a]:underline");
        classes.Should().Contain("*:[a]:underline-offset-3");
        classes.Should().Contain("*:[a]:hover:text-foreground");
        classes.Should().NotContain("q-dialog-description");
    }

    [Fact]
    public void Label_matches_shadcn_base_classes()
    {
        var cut = Render<Label>(parameters => parameters
            .Add(p => p.For, "terms")
            .Add(p => p.ChildContent, "Accept"));

        string classes = cut.Find("[data-slot='label']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("items-center");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("leading-none");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("select-none");
        classes.Should().Contain("peer-disabled:cursor-not-allowed");
        classes.Should().NotContain("q-label");
    }

    [Fact]
    public void Breadcrumb_matches_shadcn_base_classes()
    {
        var breadcrumb = Render<Breadcrumb>(parameters => parameters
            .Add(p => p.Composed, false)
            .Add(p => p.ChildContent, "Trail"));
        var breadcrumbList = Render<BreadcrumbList>(parameters => parameters
            .Add(p => p.ChildContent, "Trail"));

        string breadcrumbClasses = breadcrumb.Find("[data-slot='breadcrumb']").GetAttribute("class") ?? string.Empty;
        string listClasses = breadcrumb.Find("[data-slot='breadcrumb-list']").GetAttribute("class")!;
        string standaloneListClasses = breadcrumbList.Find("[data-slot='breadcrumb-list']").GetAttribute("class")!;

        breadcrumbClasses.Should().BeEmpty();

        listClasses.Should().Contain("flex");
        listClasses.Should().Contain("flex-wrap");
        listClasses.Should().Contain("items-center");
        listClasses.Should().Contain("gap-1.5");
        listClasses.Should().Contain("text-sm");
        listClasses.Should().Contain("wrap-break-word");
        listClasses.Should().Contain("text-muted-foreground");
        listClasses.Should().NotContain("q-breadcrumb-list");
        listClasses.Should().NotContain("sm:gap-2.5");

        standaloneListClasses.Should().Contain("gap-1.5");
        standaloneListClasses.Should().Contain("text-muted-foreground");
        standaloneListClasses.Should().NotContain("q-breadcrumb-list");
    }

    [Fact]
    public void Breadcrumb_ellipsis_matches_shadcn_base_classes()
    {
        var cut = Render<BreadcrumbEllipsis>();

        string classes = cut.Find("[data-slot='breadcrumb-ellipsis']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("size-9");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("[&>svg]:size-4");
        classes.Should().NotContain("q-breadcrumb-ellipsis");
    }

    [Fact]
    public void Breadcrumb_leaf_slots_match_shadcn_base_classes()
    {
        var item = Render<BreadcrumbItem>(parameters => parameters
            .Add(p => p.ChildContent, "Home"));
        var link = Render<BreadcrumbLink>(parameters => parameters
            .Add(p => p.To, "/")
            .Add(p => p.ChildContent, "Home"));
        var page = Render<BreadcrumbPage>(parameters => parameters
            .Add(p => p.ChildContent, "Current"));
        var separator = Render<BreadcrumbSeparator>();

        string itemClasses = item.Find("[data-slot='breadcrumb-item']").GetAttribute("class")!;
        string linkClasses = link.Find("[data-slot='breadcrumb-link']").GetAttribute("class")!;
        string pageClasses = page.Find("[data-slot='breadcrumb-page']").GetAttribute("class")!;
        string separatorClasses = separator.Find("[data-slot='breadcrumb-separator']").GetAttribute("class")!;

        itemClasses.Should().Contain("inline-flex");
        itemClasses.Should().Contain("items-center");
        itemClasses.Should().Contain("gap-1");
        itemClasses.Should().NotContain("q-breadcrumb-item");

        linkClasses.Should().Contain("transition-colors");
        linkClasses.Should().Contain("hover:text-foreground");
        linkClasses.Should().NotContain("q-breadcrumb-link");

        pageClasses.Should().Contain("font-normal");
        pageClasses.Should().Contain("text-foreground");
        pageClasses.Should().NotContain("q-breadcrumb-page");

        separatorClasses.Should().Contain("[&>svg]:size-3.5");
        separatorClasses.Should().NotContain("q-breadcrumb-separator");
    }

    [Fact]
    public void Avatar_slots_match_shadcn_base_classes()
    {
        var avatar = Render<Avatar>(parameters => parameters
            .Add(p => p.ChildContent, "Avatar"));
        var fallback = Render<AvatarFallback>(parameters => parameters
            .Add(p => p.ChildContent, "AB"));
        var badge = Render<AvatarBadge>(parameters => parameters
            .Add(p => p.ChildContent, string.Empty));

        string avatarClasses = avatar.Find("[data-slot='avatar']").GetAttribute("class")!;
        string fallbackClasses = fallback.Find("[data-slot='avatar-fallback']").GetAttribute("class")!;
        string badgeClasses = badge.Find("[data-slot='avatar-badge']").GetAttribute("class")!;

        avatarClasses.Should().Contain("group/avatar");
        avatarClasses.Should().Contain("relative");
        avatarClasses.Should().Contain("flex");
        avatarClasses.Should().Contain("size-8");
        avatarClasses.Should().Contain("shrink-0");
        avatarClasses.Should().Contain("rounded-full");
        avatarClasses.Should().Contain("select-none");
        avatarClasses.Should().Contain("after:border-border");
        avatarClasses.Should().Contain("data-[size=lg]:size-10");
        avatarClasses.Should().Contain("data-[size=sm]:size-6");
        avatarClasses.Should().NotContain("q-avatar");

        fallbackClasses.Should().Contain("absolute");
        fallbackClasses.Should().Contain("inset-0");
        fallbackClasses.Should().Contain("bg-muted");
        fallbackClasses.Should().Contain("text-muted-foreground");
        fallbackClasses.Should().Contain("flex");
        fallbackClasses.Should().Contain("rounded-full");
        fallbackClasses.Should().Contain("items-center");
        fallbackClasses.Should().Contain("justify-center");
        fallbackClasses.Should().NotContain("q-avatar-fallback");

        badgeClasses.Should().Contain("absolute");
        badgeClasses.Should().Contain("right-0");
        badgeClasses.Should().Contain("bottom-0");
        badgeClasses.Should().Contain("inline-flex");
        badgeClasses.Should().Contain("items-center");
        badgeClasses.Should().Contain("justify-center");
        badgeClasses.Should().Contain("rounded-full");
        badgeClasses.Should().Contain("text-primary-foreground");
        badgeClasses.Should().Contain("bg-blend-color");
        badgeClasses.Should().Contain("ring-2");
        badgeClasses.Should().Contain("ring-background");
        badgeClasses.Should().Contain("group-data-[size=default]/avatar:size-2.5");
        badgeClasses.Should().NotContain("q-avatar-badge");
    }

    [Fact]
    public void Avatar_group_slots_match_shadcn_base_classes()
    {
        var group = Render<AvatarGroup>(parameters => parameters
            .Add(p => p.ChildContent, "Group"));
        var count = Render<AvatarGroupCount>(parameters => parameters
            .Add(p => p.ChildContent, "+3"));

        string groupClasses = group.Find("[data-slot='avatar-group']").GetAttribute("class")!;
        string countClasses = count.Find("[data-slot='avatar-group-count']").GetAttribute("class")!;

        groupClasses.Should().Contain("group/avatar-group");
        groupClasses.Should().Contain("flex");
        groupClasses.Should().Contain("-space-x-2");
        groupClasses.Should().Contain("*:data-[slot=avatar]:ring-2");
        groupClasses.Should().Contain("*:data-[slot=avatar]:ring-background");
        groupClasses.Should().NotContain("q-avatar-group");

        countClasses.Should().Contain("relative");
        countClasses.Should().Contain("flex");
        countClasses.Should().Contain("size-8");
        countClasses.Should().Contain("rounded-full");
        countClasses.Should().Contain("bg-muted");
        countClasses.Should().Contain("text-muted-foreground");
        countClasses.Should().Contain("shrink-0");
        countClasses.Should().Contain("items-center");
        countClasses.Should().Contain("justify-center");
        countClasses.Should().Contain("ring-2");
        countClasses.Should().Contain("ring-background");
        countClasses.Should().Contain("group-has-data-[size=lg]/avatar-group:size-10");
        countClasses.Should().Contain("group-has-data-[size=sm]/avatar-group:size-6");
        countClasses.Should().NotContain("q-avatar-group-count");
    }

    [Fact]
    public void AlertDialog_layout_slots_match_shadcn_base_classes()
    {
        var header = Render<AlertDialogHeader>(parameters => parameters
            .Add(p => p.ChildContent, "Header"));
        var footer = Render<AlertDialogFooter>(parameters => parameters
            .Add(p => p.ChildContent, "Footer"));

        string headerClasses = header.Find("[data-slot='alert-dialog-header']").GetAttribute("class")!;
        string footerClasses = footer.Find("[data-slot='alert-dialog-footer']").GetAttribute("class")!;

        headerClasses.Should().Contain("grid");
        headerClasses.Should().Contain("grid-rows-[auto_1fr]");
        headerClasses.Should().Contain("place-items-center");
        headerClasses.Should().Contain("gap-1.5");
        headerClasses.Should().Contain("text-center");
        headerClasses.Should().Contain("has-data-[slot=alert-dialog-media]:grid-rows-[auto_auto_1fr]");
        headerClasses.Should().NotContain("q-alert-dialog-header");

        footerClasses.Should().Contain("flex");
        footerClasses.Should().Contain("gap-2");
        footerClasses.Should().Contain("flex-col-reverse");
        footerClasses.Should().Contain("group-data-[size=sm]/alert-dialog-content:grid");
        footerClasses.Should().Contain("group-data-[size=sm]/alert-dialog-content:grid-cols-2");
        footerClasses.Should().Contain("sm:flex-row");
        footerClasses.Should().Contain("sm:justify-end");
        footerClasses.Should().NotContain("q-alert-dialog-footer");
    }

    [Fact]
    public void Alert_and_action_match_shadcn_base_classes()
    {
        var alert = Render<Alert>(parameters => parameters
            .Add(p => p.ShowDefaultIcon, true)
            .Add(p => p.ChildContent, "Alert"));
        var action = Render<AlertAction>(parameters => parameters
            .Add(p => p.ChildContent, "Undo"));

        string alertClasses = alert.Find("[data-slot='alert']").GetAttribute("class")!;
        string actionClasses = action.Find("[data-slot='alert-action']").GetAttribute("class")!;

        alertClasses.Should().Contain("grid");
        alertClasses.Should().Contain("grid-cols-[0_1fr]");
        alertClasses.Should().Contain("items-start");
        alertClasses.Should().Contain("gap-y-0.5");
        alertClasses.Should().Contain("has-[svg]:grid-cols-[calc(var(--spacing)*4)_1fr]");
        alertClasses.Should().Contain("has-[svg]:gap-x-3");
        alertClasses.Should().Contain("[&>svg]:size-4");
        alertClasses.Should().Contain("[&>svg]:translate-y-0.5");
        alertClasses.Should().Contain("[&>svg]:text-current");
        alertClasses.Should().NotContain("q-alert");

        actionClasses.Should().Contain("absolute");
        actionClasses.Should().Contain("top-4");
        actionClasses.Should().Contain("right-4");
        actionClasses.Should().NotContain("q-alert-action");
    }

    [Fact]
    public void Accordion_slots_match_shadcn_base_classes()
    {
        var cut = Render<Accordion>(parameters => parameters
            .Add(p => p.DefaultValue, "shipping")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<AccordionItem>(0);
                builder.AddAttribute(1, "Value", "shipping");
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(itemBuilder =>
                {
                    itemBuilder.OpenComponent<AccordionTrigger>(0);
                    itemBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Question")));
                    itemBuilder.CloseComponent();

                    itemBuilder.OpenComponent<AccordionContent>(2);
                    itemBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Answer")));
                    itemBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        string accordionClasses = cut.Find("[data-slot='accordion']").GetAttribute("class")!;
        string itemClasses = cut.Find("[data-slot='accordion-item']").GetAttribute("class")!;
        string triggerClasses = cut.Find("[data-slot='accordion-trigger']").GetAttribute("class")!;
        string contentClasses = cut.Find("[data-slot='accordion-content']").GetAttribute("class")!;
        string innerClasses = cut.Find("[data-slot='accordion-content-inner']").GetAttribute("class")!;

        accordionClasses.Should().Contain("flex");
        accordionClasses.Should().Contain("w-full");
        accordionClasses.Should().Contain("flex-col");
        accordionClasses.Should().NotContain("q-accordion");

        itemClasses.Should().Contain("not-last:border-b");
        itemClasses.Should().NotContain("q-accordion-item");
        itemClasses.Should().NotContain("last:border-b-0");

        triggerClasses.Should().Contain("group/accordion-trigger");
        triggerClasses.Should().Contain("relative");
        triggerClasses.Should().Contain("flex");
        triggerClasses.Should().Contain("flex-1");
        triggerClasses.Should().Contain("items-start");
        triggerClasses.Should().Contain("justify-between");
        triggerClasses.Should().Contain("rounded-lg");
        triggerClasses.Should().Contain("border");
        triggerClasses.Should().Contain("border-transparent");
        triggerClasses.Should().Contain("py-2.5");
        triggerClasses.Should().Contain("text-left");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("font-medium");
        triggerClasses.Should().Contain("transition-all");
        triggerClasses.Should().Contain("outline-none");
        triggerClasses.Should().Contain("hover:underline");
        triggerClasses.Should().Contain("focus-visible:border-ring");
        triggerClasses.Should().Contain("focus-visible:ring-[3px]");
        triggerClasses.Should().Contain("focus-visible:ring-ring/50");
        triggerClasses.Should().Contain("**:data-[slot=accordion-trigger-icon]:ms-auto");
        triggerClasses.Should().Contain("**:data-[slot=accordion-trigger-icon]:size-4");
        triggerClasses.Should().Contain("**:data-[slot=accordion-trigger-icon]:text-muted-foreground");
        triggerClasses.Should().NotContain("q-accordion-trigger");

        contentClasses.Should().Contain("overflow-hidden");
        contentClasses.Should().Contain("text-sm");
        contentClasses.Should().Contain("data-open:animate-accordion-down");
        contentClasses.Should().Contain("data-closed:animate-accordion-up");
        contentClasses.Should().NotContain("q-accordion-content");

        innerClasses.Should().Contain("h-(--radix-accordion-content-height)");
        innerClasses.Should().Contain("pt-0");
        innerClasses.Should().Contain("pb-2.5");
        innerClasses.Should().Contain("data-ending-style:h-0");
        innerClasses.Should().Contain("data-starting-style:h-0");
        innerClasses.Should().Contain("[&_a]:underline");
        innerClasses.Should().Contain("[&_a]:underline-offset-3");
        innerClasses.Should().Contain("[&_a]:hover:text-foreground");
        innerClasses.Should().Contain("[&_p:not(:last-child)]:mb-4");
    }

    [Fact]
    public void Badge_matches_shadcn_base_classes()
    {
        var cut = Render<Badge>(parameters => parameters
            .Add(p => p.Variant, BadgeVariant.Secondary)
            .Add(p => p.ChildContent, "Featured"));

        string classes = cut.Find("[data-slot='badge']").GetAttribute("class")!;

        classes.Should().Contain("group/badge");
        classes.Should().Contain("inline-flex");
        classes.Should().Contain("h-5");
        classes.Should().Contain("w-fit");
        classes.Should().Contain("rounded-4xl");
        classes.Should().Contain("border");
        classes.Should().Contain("px-2");
        classes.Should().Contain("py-0.5");
        classes.Should().Contain("text-xs");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("transition-all");
        classes.Should().Contain("has-data-[icon=inline-end]:pr-1.5");
        classes.Should().Contain("has-data-[icon=inline-start]:pl-1.5");
        classes.Should().Contain("[&>svg]:size-3!");
        classes.Should().Contain("bg-secondary");
        classes.Should().Contain("text-secondary-foreground");
        classes.Should().Contain("[a]:hover:bg-secondary/80");
        classes.Should().NotContain("q-badge");
    }

    [Fact]
    public void Separator_matches_shadcn_base_classes()
    {
        var cut = Render<Hr>(parameters => parameters
            .Add(p => p.Orientation, SeparatorOrientation.Horizontal));

        string classes = cut.Find("[data-slot='separator']").GetAttribute("class")!;

        classes.Should().Contain("bg-border");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("data-horizontal:h-px");
        classes.Should().Contain("data-horizontal:w-full");
        classes.Should().Contain("data-vertical:w-px");
        classes.Should().Contain("data-vertical:self-stretch");
        classes.Should().NotContain("q-hr");
    }

    [Fact]
    public void Select_slots_match_shadcn_base_classes()
    {
        var triggerCut = Render<Select<string>>(parameters => parameters
            .Add(p => p.DefaultItemText, "Select a fruit")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<SelectTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(triggerBuilder =>
                {
                    triggerBuilder.OpenComponent<SelectValue>(0);
                    triggerBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var label = Render<SelectLabel>(parameters => parameters
            .Add(p => p.ChildContent, "Fruits"));
        var group = Render<SelectGroup>(parameters => parameters
            .Add(p => p.ChildContent, "Items"));
        var item = Render<SelectItem<string>>(parameters => parameters
            .Add(p => p.ItemValue, "apple")
            .Add(p => p.Text, "Apple"));
        var separator = Render<SelectSeparator>();

        string selectClasses = triggerCut.Find("[data-slot='select']").GetAttribute("class")!;
        string triggerClasses = triggerCut.Find("[data-slot='select-trigger']").GetAttribute("class")!;
        string valueClasses = triggerCut.Find("[data-slot='select-value']").GetAttribute("class")!;
        string labelClasses = label.Find("[data-slot='select-label']").GetAttribute("class")!;
        string groupClasses = group.Find("[data-slot='select-group']").GetAttribute("class")!;
        string itemClasses = item.Find("[data-slot='select-item']").GetAttribute("class")!;
        string separatorClasses = separator.Find("[data-slot='select-separator']").GetAttribute("class")!;

        selectClasses.Should().Contain("group/select");
        selectClasses.Should().Contain("relative");
        selectClasses.Should().Contain("w-full");
        selectClasses.Should().Contain("max-w-full");
        selectClasses.Should().NotContain("q-select");

        triggerClasses.Should().Contain("flex");
        triggerClasses.Should().Contain("w-fit");
        triggerClasses.Should().Contain("items-center");
        triggerClasses.Should().Contain("justify-between");
        triggerClasses.Should().Contain("gap-1.5");
        triggerClasses.Should().Contain("rounded-lg");
        triggerClasses.Should().Contain("border");
        triggerClasses.Should().Contain("bg-transparent");
        triggerClasses.Should().Contain("py-2");
        triggerClasses.Should().Contain("pr-2");
        triggerClasses.Should().Contain("pl-2.5");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("transition-colors");
        triggerClasses.Should().Contain("outline-none");
        triggerClasses.Should().Contain("data-[size=default]:h-8");
        triggerClasses.Should().Contain("data-[size=sm]:h-7");
        triggerClasses.Should().Contain("*:data-[slot=select-value]:line-clamp-1");
        triggerClasses.Should().NotContain("q-select-trigger");

        valueClasses.Should().Contain("line-clamp-1");
        valueClasses.Should().Contain("text-left");
        valueClasses.Should().NotContain("q-select-value");

        labelClasses.Should().Contain("px-2");
        labelClasses.Should().Contain("py-1.5");
        labelClasses.Should().Contain("text-xs");
        labelClasses.Should().Contain("font-medium");
        labelClasses.Should().Contain("text-muted-foreground");
        labelClasses.Should().NotContain("q-select-label");

        groupClasses.Should().Contain("p-1");
        groupClasses.Should().NotContain("q-select-group");

        itemClasses.Should().Contain("relative");
        itemClasses.Should().Contain("flex");
        itemClasses.Should().Contain("w-full");
        itemClasses.Should().Contain("cursor-default");
        itemClasses.Should().Contain("items-center");
        itemClasses.Should().Contain("gap-2");
        itemClasses.Should().Contain("rounded-sm");
        itemClasses.Should().Contain("py-1.5");
        itemClasses.Should().Contain("pr-8");
        itemClasses.Should().Contain("pl-2");
        itemClasses.Should().Contain("text-sm");
        itemClasses.Should().Contain("outline-hidden");
        itemClasses.Should().Contain("select-none");
        itemClasses.Should().Contain("data-[highlighted]:bg-accent");
        itemClasses.Should().Contain("data-[disabled]:pointer-events-none");
        itemClasses.Should().Contain("*:[span]:last:flex");
        itemClasses.Should().NotContain("q-select-item");

        separatorClasses.Should().Contain("pointer-events-none");
        separatorClasses.Should().Contain("-mx-1");
        separatorClasses.Should().Contain("my-1");
        separatorClasses.Should().Contain("h-px");
        separatorClasses.Should().Contain("bg-border");
        separatorClasses.Should().NotContain("q-select-separator");
    }

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

        string commandClasses = cut.Find("[data-slot='command']").GetAttribute("class")!;
        string wrapperClasses = cut.Find("[data-slot='command-input-wrapper']").GetAttribute("class")!;
        string inputGroupClasses = cut.Find("[data-slot='input-group']").GetAttribute("class")!;
        string inputClasses = cut.Find("[data-slot='command-input']").GetAttribute("class")!;
        string listClasses = cut.Find("[data-slot='command-list']").GetAttribute("class")!;
        string groupClasses = cut.Find("[data-slot='command-group']").GetAttribute("class")!;
        string itemClasses = cut.Find("[data-slot='command-item']").GetAttribute("class")!;
        string shortcutClasses = cut.Find("[data-slot='command-shortcut']").GetAttribute("class")!;
        string separatorClasses = cut.Find("[data-slot='command-separator']").GetAttribute("class")!;

        commandClasses.Should().Contain("flex");
        commandClasses.Should().Contain("size-full");
        commandClasses.Should().Contain("flex-col");
        commandClasses.Should().Contain("overflow-hidden");
        commandClasses.Should().Contain("rounded-xl!");
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

        inputClasses.Should().Contain("w-full");
        inputClasses.Should().Contain("bg-transparent");
        inputClasses.Should().Contain("text-sm");
        inputClasses.Should().Contain("outline-hidden");
        inputClasses.Should().Contain("disabled:cursor-not-allowed");
        inputClasses.Should().NotContain("q-command-input");

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

    [Fact]
    public void Dropdown_leaf_slots_match_shadcn_base_classes()
    {
        var label = Render<DropdownLabel>(parameters => parameters
            .Add(p => p.ChildContent, "My Account"));
        var divider = Render<DropdownDivider>();
        var shortcut = Render<DropdownShortcut>(parameters => parameters
            .Add(p => p.ChildContent, "⌘K"));
        var sub = Render<DropdownSub>(parameters => parameters
            .Add(p => p.ChildContent, "Sub"));

        string labelClasses = label.Find("[data-slot='dropdown-menu-label']").GetAttribute("class")!;
        string dividerClasses = divider.Find("[data-slot='dropdown-menu-separator']").GetAttribute("class")!;
        string shortcutClasses = shortcut.Find("[data-slot='dropdown-menu-shortcut']").GetAttribute("class")!;
        string subClasses = sub.Find("[data-slot='dropdown-menu-sub']").GetAttribute("class")!;

        labelClasses.Should().Contain("px-2");
        labelClasses.Should().Contain("py-1.5");
        labelClasses.Should().Contain("text-sm");
        labelClasses.Should().Contain("font-medium");
        labelClasses.Should().NotContain("q-dropdown-label");

        dividerClasses.Should().Contain("-mx-1");
        dividerClasses.Should().Contain("my-1");
        dividerClasses.Should().Contain("h-px");
        dividerClasses.Should().Contain("bg-border");
        dividerClasses.Should().NotContain("q-dropdown-divider");

        shortcutClasses.Should().Contain("ml-auto");
        shortcutClasses.Should().Contain("text-xs");
        shortcutClasses.Should().Contain("tracking-widest");
        shortcutClasses.Should().Contain("text-muted-foreground");
        shortcutClasses.Should().NotContain("q-dropdown-shortcut");

        subClasses.Should().Contain("relative");
        subClasses.Should().NotContain("q-dropdown-sub");
    }

    [Fact]
    public void Menubar_slots_match_shadcn_base_classes()
    {
        var label = Render<MenubarLabel>(parameters => parameters.Add(p => p.ChildContent, "My Account"));
        var separator = Render<MenubarSeparator>();
        var shortcut = Render<MenubarShortcut>(parameters => parameters.Add(p => p.ChildContent, "⌘K"));
        var sub = Render<MenubarSub>(parameters => parameters.Add(p => p.ChildContent, "Sub"));

        string labelClasses = label.Find("[data-slot='menubar-label']").GetAttribute("class")!;
        string separatorClasses = separator.Find("[data-slot='menubar-separator']").GetAttribute("class")!;
        string shortcutClasses = shortcut.Find("[data-slot='menubar-shortcut']").GetAttribute("class")!;
        string subClasses = sub.Find("[data-slot='menubar-sub']").GetAttribute("class")!;

        labelClasses.Should().Contain("px-2");
        labelClasses.Should().Contain("py-1.5");
        labelClasses.Should().Contain("text-sm");
        labelClasses.Should().Contain("font-medium");
        labelClasses.Should().NotContain("q-menubar-label");

        separatorClasses.Should().Contain("-mx-1");
        separatorClasses.Should().Contain("my-1");
        separatorClasses.Should().Contain("h-px");
        separatorClasses.Should().Contain("bg-border");
        separatorClasses.Should().NotContain("q-menubar-separator");

        shortcutClasses.Should().Contain("ml-auto");
        shortcutClasses.Should().Contain("text-xs");
        shortcutClasses.Should().Contain("tracking-widest");
        shortcutClasses.Should().Contain("text-muted-foreground");
        shortcutClasses.Should().NotContain("q-menubar-shortcut");

        subClasses.Should().Contain("relative");
        subClasses.Should().NotContain("q-menubar-sub");
    }

    [Fact]
    public void ContextMenu_leaf_slots_match_shadcn_base_classes()
    {
        var trigger = Render<ContextMenuTrigger>(parameters => parameters.Add(p => p.ChildContent, "Open"));
        var label = Render<ContextMenuLabel>(parameters => parameters.Add(p => p.ChildContent, "My Account"));
        var separator = Render<ContextMenuSeparator>();
        var shortcut = Render<ContextMenuShortcut>(parameters => parameters.Add(p => p.ChildContent, "⌘K"));
        var sub = Render<ContextMenuSub>(parameters => parameters.Add(p => p.ChildContent, "Sub"));

        string? triggerClasses = trigger.Find("[data-slot='context-menu-trigger']").GetAttribute("class");
        string labelClasses = label.Find("[data-slot='context-menu-label']").GetAttribute("class")!;
        string separatorClasses = separator.Find("[data-slot='context-menu-separator']").GetAttribute("class")!;
        string shortcutClasses = shortcut.Find("[data-slot='context-menu-shortcut']").GetAttribute("class")!;
        string subClasses = sub.Find("[data-slot='context-menu-sub']").GetAttribute("class")!;

        (triggerClasses ?? string.Empty).Should().NotContain("q-context-menu-trigger");
        (triggerClasses ?? string.Empty).Should().NotContain("cursor-context-menu");

        labelClasses.Should().Contain("px-2");
        labelClasses.Should().Contain("py-1.5");
        labelClasses.Should().Contain("text-sm");
        labelClasses.Should().Contain("font-medium");
        labelClasses.Should().NotContain("q-context-menu-label");

        separatorClasses.Should().Contain("-mx-1");
        separatorClasses.Should().Contain("my-1");
        separatorClasses.Should().Contain("h-px");
        separatorClasses.Should().Contain("bg-border");
        separatorClasses.Should().NotContain("q-context-menu-separator");

        shortcutClasses.Should().Contain("ml-auto");
        shortcutClasses.Should().Contain("text-xs");
        shortcutClasses.Should().Contain("tracking-widest");
        shortcutClasses.Should().Contain("text-muted-foreground");
        shortcutClasses.Should().NotContain("q-context-menu-shortcut");

        subClasses.Should().Contain("relative");
        subClasses.Should().NotContain("q-context-menu-sub");
    }

    [Fact]
    public void Popover_slots_match_shadcn_base_classes()
    {
        var cut = Render<Popover>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<PopoverTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Open popover")));
                builder.CloseComponent();

                builder.OpenComponent<PopoverHeader>(2);
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(headerBuilder =>
                {
                    headerBuilder.OpenComponent<PopoverTitle>(0);
                    headerBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(titleBuilder => titleBuilder.AddContent(0, "Dimensions")));
                    headerBuilder.CloseComponent();

                    headerBuilder.OpenComponent<PopoverDescription>(2);
                    headerBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(descriptionBuilder => descriptionBuilder.AddContent(0, "Set the dimensions for the layer.")));
                    headerBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        string popoverClasses = cut.Find("[data-slot='popover']").GetAttribute("class")!;
        string triggerClasses = cut.Find("[data-slot='popover-trigger']").GetAttribute("class")!;
        string headerClasses = cut.Find("[data-slot='popover-header']").GetAttribute("class")!;
        string titleClasses = cut.Find("[data-slot='popover-title']").GetAttribute("class")!;
        string descriptionClasses = cut.Find("[data-slot='popover-description']").GetAttribute("class")!;

        popoverClasses.Should().Contain("relative");
        popoverClasses.Should().NotContain("q-popover");

        triggerClasses.Should().Contain("group/button");
        triggerClasses.Should().Contain("inline-flex");
        triggerClasses.Should().Contain("shrink-0");
        triggerClasses.Should().Contain("items-center");
        triggerClasses.Should().Contain("justify-center");
        triggerClasses.Should().Contain("rounded-lg");
        triggerClasses.Should().Contain("border");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("font-medium");
        triggerClasses.Should().Contain("whitespace-nowrap");
        triggerClasses.Should().Contain("transition-all");
        triggerClasses.Should().Contain("outline-none");
        triggerClasses.Should().Contain("select-none");
        triggerClasses.Should().Contain("focus-visible:border-ring");
        triggerClasses.Should().Contain("focus-visible:ring-3");
        triggerClasses.Should().Contain("border-border");
        triggerClasses.Should().Contain("bg-background");
        triggerClasses.Should().Contain("hover:bg-muted");
        triggerClasses.Should().Contain("aria-expanded:bg-muted");
        triggerClasses.Should().Contain("h-8");
        triggerClasses.Should().Contain("gap-1.5");
        triggerClasses.Should().Contain("px-2.5");
        triggerClasses.Should().NotContain("q-popover-trigger");

        headerClasses.Should().Contain("grid");
        headerClasses.Should().Contain("gap-1.5");
        headerClasses.Should().NotContain("q-popover-header");

        titleClasses.Should().Contain("font-medium");
        titleClasses.Should().Contain("leading-none");
        titleClasses.Should().NotContain("q-popover-title");

        descriptionClasses.Should().Contain("text-sm");
        descriptionClasses.Should().Contain("text-muted-foreground");
        descriptionClasses.Should().NotContain("q-popover-description");
    }

    [Fact]
    public void Drawer_slots_match_shadcn_base_classes()
    {
        var trigger = Render<DrawerTrigger>(parameters => parameters.Add(p => p.ChildContent, "Open Drawer"));
        var header = Render<DrawerHeader>(parameters => parameters.Add(p => p.ChildContent, "Header"));
        var title = Render<DrawerTitle>(parameters => parameters.Add(p => p.ChildContent, "Edit profile"));
        var description = Render<DrawerDescription>(parameters => parameters.Add(p => p.ChildContent, "Make changes to your profile here."));
        var footer = Render<DrawerFooter>(parameters => parameters.Add(p => p.ChildContent, "Actions"));

        string triggerClasses = trigger.Find("[data-slot='drawer-trigger']").GetAttribute("class")!;
        string headerClasses = header.Find("[data-slot='drawer-header']").GetAttribute("class")!;
        string titleClasses = title.Find("[data-slot='drawer-title']").GetAttribute("class")!;
        string descriptionClasses = description.Find("[data-slot='drawer-description']").GetAttribute("class")!;
        string footerClasses = footer.Find("[data-slot='drawer-footer']").GetAttribute("class")!;

        triggerClasses.Should().Contain("group/button");
        triggerClasses.Should().Contain("inline-flex");
        triggerClasses.Should().Contain("shrink-0");
        triggerClasses.Should().Contain("items-center");
        triggerClasses.Should().Contain("justify-center");
        triggerClasses.Should().Contain("rounded-lg");
        triggerClasses.Should().Contain("border");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("font-medium");
        triggerClasses.Should().Contain("whitespace-nowrap");
        triggerClasses.Should().Contain("transition-all");
        triggerClasses.Should().Contain("outline-none");
        triggerClasses.Should().Contain("select-none");
        triggerClasses.Should().Contain("focus-visible:border-ring");
        triggerClasses.Should().Contain("focus-visible:ring-3");
        triggerClasses.Should().Contain("border-border");
        triggerClasses.Should().Contain("bg-background");
        triggerClasses.Should().Contain("hover:bg-muted");
        triggerClasses.Should().Contain("aria-expanded:bg-muted");
        triggerClasses.Should().Contain("h-8");
        triggerClasses.Should().Contain("gap-1.5");
        triggerClasses.Should().Contain("px-2.5");
        triggerClasses.Should().NotContain("q-drawer-trigger");

        headerClasses.Should().Contain("flex");
        headerClasses.Should().Contain("flex-col");
        headerClasses.Should().Contain("gap-0.5");
        headerClasses.Should().Contain("p-4");
        headerClasses.Should().Contain("group-data-[direction=bottom]/drawer-content:text-center");
        headerClasses.Should().Contain("md:text-left");
        headerClasses.Should().NotContain("q-drawer-header");

        titleClasses.Should().Contain("font-semibold");
        titleClasses.Should().Contain("leading-none");
        titleClasses.Should().NotContain("q-drawer-title");

        descriptionClasses.Should().Contain("text-muted-foreground");
        descriptionClasses.Should().Contain("text-sm");
        descriptionClasses.Should().NotContain("q-drawer-description");

        footerClasses.Should().Contain("mt-auto");
        footerClasses.Should().Contain("flex");
        footerClasses.Should().Contain("flex-col");
        footerClasses.Should().Contain("gap-2");
        footerClasses.Should().Contain("p-4");
        footerClasses.Should().NotContain("q-drawer-footer");
    }

    [Fact]
    public void NavigationMenu_slots_match_shadcn_base_classes()
    {
        var cut = Render<NavigationMenu>(parameters => parameters
            .Add(p => p.Viewport, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<NavigationMenuList>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(listBuilder =>
                {
                    listBuilder.OpenComponent<NavigationMenuItem>(0);
                    listBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(itemBuilder =>
                    {
                        itemBuilder.OpenComponent<NavigationMenuTrigger>(0);
                        itemBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Getting started")));
                        itemBuilder.CloseComponent();
                    }));
                    listBuilder.CloseComponent();

                    listBuilder.OpenComponent<NavigationMenuItem>(2);
                    listBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(itemBuilder =>
                    {
                        itemBuilder.OpenComponent<NavigationMenuLink>(0);
                        itemBuilder.AddAttribute(1, "Href", "/docs");
                        itemBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Docs")));
                        itemBuilder.CloseComponent();
                    }));
                    listBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        string rootClasses = cut.Find("[data-slot='navigation-menu']").GetAttribute("class")!;
        string listClasses = cut.Find("[data-slot='navigation-menu-list']").GetAttribute("class")!;
        string itemClasses = cut.Find("[data-slot='navigation-menu-item']").GetAttribute("class")!;
        string triggerClasses = cut.Find("[data-slot='navigation-menu-trigger']").GetAttribute("class")!;
        string linkClasses = cut.Find("[data-slot='navigation-menu-link']").GetAttribute("class")!;

        rootClasses.Should().Contain("group/navigation-menu");
        rootClasses.Should().Contain("relative");
        rootClasses.Should().Contain("flex");
        rootClasses.Should().Contain("max-w-max");
        rootClasses.Should().NotContain("q-navigation-menu");

        listClasses.Should().Contain("group");
        listClasses.Should().Contain("flex");
        listClasses.Should().Contain("list-none");
        listClasses.Should().Contain("gap-0");
        listClasses.Should().NotContain("q-navigation-menu-list");

        itemClasses.Should().Contain("relative");
        itemClasses.Should().NotContain("q-navigation-menu-item");

        triggerClasses.Should().Contain("group/navigation-menu-trigger");
        triggerClasses.Should().Contain("rounded-lg");
        triggerClasses.Should().Contain("px-2.5");
        triggerClasses.Should().Contain("py-1.5");
        triggerClasses.Should().Contain("hover:bg-muted");
        triggerClasses.Should().Contain("data-popup-open:bg-muted/50");
        triggerClasses.Should().Contain("data-open:bg-muted/50");
        triggerClasses.Should().NotContain("q-navigation-menu-trigger");

        linkClasses.Should().Contain("group/navigation-menu-trigger");
        linkClasses.Should().Contain("gap-2");
        linkClasses.Should().Contain("p-2");
        linkClasses.Should().Contain("in-data-[slot=navigation-menu-content]:rounded-md");
        linkClasses.Should().Contain("data-active:bg-muted/50");
        linkClasses.Should().NotContain("q-navigation-menu-link");
    }

    [Fact]
    public void Carousel_slots_match_shadcn_base_classes()
    {
        var cut = Render<Carousel>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<CarouselContent>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<CarouselItem>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(itemBuilder => itemBuilder.AddContent(0, "One")));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();

                builder.OpenComponent<CarouselPrevious>(2);
                builder.CloseComponent();

                builder.OpenComponent<CarouselNext>(3);
                builder.CloseComponent();
            })));

        string rootClasses = cut.Find("[data-slot='carousel']").GetAttribute("class")!;
        string contentClasses = cut.Find("[data-slot='carousel-content']").GetAttribute("class")!;
        string itemClasses = cut.Find("[data-slot='carousel-item']").GetAttribute("class")!;
        string previousClasses = cut.Find("[data-slot='carousel-previous']").GetAttribute("class")!;
        string nextClasses = cut.Find("[data-slot='carousel-next']").GetAttribute("class")!;

        rootClasses.Should().Contain("relative");
        rootClasses.Should().NotContain("q-carousel");

        contentClasses.Should().Contain("overflow-hidden");
        contentClasses.Should().NotContain("q-carousel-content");

        itemClasses.Should().Contain("min-w-0");
        itemClasses.Should().Contain("shrink-0");
        itemClasses.Should().Contain("grow-0");
        itemClasses.Should().Contain("basis-full");
        itemClasses.Should().Contain("pl-4");
        itemClasses.Should().NotContain("q-carousel-item");

        previousClasses.Should().Contain("absolute");
        previousClasses.Should().Contain("touch-manipulation");
        previousClasses.Should().Contain("rounded-full");
        previousClasses.Should().Contain("top-1/2");
        previousClasses.Should().Contain("-left-12");
        previousClasses.Should().Contain("size-7");
        previousClasses.Should().NotContain("q-carousel-previous");

        nextClasses.Should().Contain("absolute");
        nextClasses.Should().Contain("touch-manipulation");
        nextClasses.Should().Contain("rounded-full");
        nextClasses.Should().Contain("top-1/2");
        nextClasses.Should().Contain("-right-12");
        nextClasses.Should().Contain("size-7");
        nextClasses.Should().NotContain("q-carousel-next");
    }

    [Fact]
    public void Table_and_form_slots_match_shadcn_base_classes()
    {
        var table = Render<TableElement>(parameters => parameters.Add(p => p.ChildContent, "Rows"));
        var thead = Render<Thead>(parameters => parameters.Add(p => p.ChildContent, "Head"));
        var tbody = Render<Tbody>(parameters => parameters.Add(p => p.ChildContent, "Body"));
        var tr = Render<Tr>(parameters => parameters.Add(p => p.ChildContent, "Row"));
        var th = Render<Th>(parameters => parameters.Add(p => p.ChildContent, "Header"));
        var td = Render<Td>(parameters => parameters.Add(p => p.ChildContent, "Cell"));
        var caption = Render<TableCaption>(parameters => parameters.Add(p => p.ChildContent, "Caption"));
        var input = Render<Input>(parameters => parameters.Add(p => p.Placeholder, "Enter text"));
        var textarea = Render<TextArea>(parameters => parameters.Add(p => p.Placeholder, "Type your message here."));

        string tableContainerClasses = table.Find("[data-slot='table-container']").GetAttribute("class")!;
        string tableClasses = table.Find("[data-slot='table']").GetAttribute("class")!;
        string theadClasses = thead.Find("[data-slot='table-header']").GetAttribute("class")!;
        string tbodyClasses = tbody.Find("[data-slot='table-body']").GetAttribute("class")!;
        string trClasses = tr.Find("[data-slot='table-row']").GetAttribute("class")!;
        string thClasses = th.Find("[data-slot='table-head']").GetAttribute("class")!;
        string tdClasses = td.Find("[data-slot='table-cell']").GetAttribute("class")!;
        string captionClasses = caption.Find("[data-slot='table-caption']").GetAttribute("class")!;
        string inputClasses = input.Find("[data-slot='input']").GetAttribute("class")!;
        string textareaClasses = textarea.Find("[data-slot='textarea']").GetAttribute("class")!;

        tableContainerClasses.Should().Contain("relative");
        tableContainerClasses.Should().Contain("w-full");
        tableContainerClasses.Should().Contain("overflow-x-auto");
        tableContainerClasses.Should().NotContain("q-table-container");

        tableClasses.Should().Contain("caption-bottom");
        tableClasses.Should().NotContain("q-table");

        theadClasses.Should().Contain("[&_tr]:border-b");
        theadClasses.Should().NotContain("q-table-thead");

        tbodyClasses.Should().Contain("[&_tr:last-child]:border-0");
        tbodyClasses.Should().NotContain("q-table-tbody");

        trClasses.Should().Contain("hover:bg-muted/50");
        trClasses.Should().Contain("data-[state=selected]:bg-muted");
        trClasses.Should().Contain("border-b");
        trClasses.Should().NotContain("q-table-tr");

        thClasses.Should().Contain("h-10");
        thClasses.Should().Contain("px-2");
        thClasses.Should().Contain("font-medium");
        thClasses.Should().NotContain("q-table-th");

        tdClasses.Should().Contain("p-2");
        tdClasses.Should().Contain("align-middle");
        tdClasses.Should().Contain("whitespace-nowrap");
        tdClasses.Should().NotContain("q-table-td");

        captionClasses.Should().Contain("mt-4");
        captionClasses.Should().Contain("text-sm");
        captionClasses.Should().Contain("text-muted-foreground");
        captionClasses.Should().NotContain("q-table-caption");

        inputClasses.Should().Contain("h-8");
        inputClasses.Should().Contain("rounded-lg");
        inputClasses.Should().Contain("border-input");
        inputClasses.Should().Contain("file:h-6");
        inputClasses.Should().Contain("focus-visible:ring-[3px]");
        inputClasses.Should().Contain("aria-invalid:ring-[3px]");
        inputClasses.Should().NotContain("q-input");

        textareaClasses.Should().Contain("min-h-16");
        textareaClasses.Should().Contain("rounded-lg");
        textareaClasses.Should().Contain("text-base");
        textareaClasses.Should().Contain("md:text-sm");
        textareaClasses.Should().Contain("aria-invalid:ring-[3px]");
        textareaClasses.Should().NotContain("q-textarea");
    }

    [Fact]
    public void Toggle_family_and_slider_slots_match_shadcn_base_classes()
    {
        var toggle = Render<Toggle>(parameters => parameters.Add(p => p.ChildContent, "Italic"));
        var toggleGroup = Render<ToggleGroup>(parameters => parameters
            .Add(p => p.Spacing, 0)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<ToggleGroupItem>(0);
                builder.AddAttribute(1, "Value", "bold");
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Bold")));
                builder.CloseComponent();
            })));
        var radioGroup = Render<RadioGroup>(parameters => parameters.Add(p => p.ChildContent, "Choice"));
        var slider = Render<Slider>(parameters => parameters.Add(p => p.SliderValue, 75d));

        string toggleClasses = toggle.Find("[data-slot='toggle']").GetAttribute("class")!;
        string toggleGroupClasses = toggleGroup.Find("[data-slot='toggle-group']").GetAttribute("class")!;
        string toggleGroupItemClasses = toggleGroup.Find("[data-slot='toggle-group-item']").GetAttribute("class")!;
        string radioGroupClasses = radioGroup.Find("[data-slot='radio-group']").GetAttribute("class")!;
        string sliderClasses = slider.Find("[data-slot='slider']").GetAttribute("class")!;
        string sliderTrackClasses = slider.Find("[data-slot='slider-track']").GetAttribute("class")!;
        string sliderRangeClasses = slider.Find("[data-slot='slider-range']").GetAttribute("class")!;
        string sliderThumbClasses = slider.Find("[data-slot='slider-thumb']").GetAttribute("class")!;

        toggleClasses.Should().Contain("group/toggle");
        toggleClasses.Should().Contain("rounded-lg");
        toggleClasses.Should().Contain("hover:bg-muted");
        toggleClasses.Should().Contain("aria-pressed:bg-muted");
        toggleClasses.Should().Contain("data-[state=on]:bg-muted");
        toggleClasses.Should().NotContain("q-toggle");

        toggleGroupClasses.Should().Contain("group/toggle-group");
        toggleGroupClasses.Should().Contain("flex");
        toggleGroupClasses.Should().Contain("w-fit");
        toggleGroupClasses.Should().Contain("gap-[--spacing(var(--gap))]");
        toggleGroupClasses.Should().NotContain("q-toggle-group");

        toggleGroupItemClasses.Should().Contain("group/toggle");
        toggleGroupItemClasses.Should().Contain("group-data-[spacing=0]/toggle-group:rounded-none");
        toggleGroupItemClasses.Should().Contain("group-data-horizontal/toggle-group:data-[spacing=0]:first:rounded-l-lg");
        toggleGroupItemClasses.Should().Contain("data-[state=on]:bg-muted");
        toggleGroupItemClasses.Should().NotContain("q-toggle-group-item");

        radioGroupClasses.Should().Contain("grid");
        radioGroupClasses.Should().Contain("gap-2");
        radioGroupClasses.Should().Contain("w-fit");
        radioGroupClasses.Should().NotContain("q-radio-group");

        sliderClasses.Should().Contain("relative");
        sliderClasses.Should().Contain("flex");
        sliderClasses.Should().Contain("touch-none");
        sliderClasses.Should().Contain("data-disabled:opacity-50");
        sliderClasses.Should().NotContain("q-slider");

        sliderTrackClasses.Should().Contain("relative");
        sliderTrackClasses.Should().Contain("grow");
        sliderTrackClasses.Should().Contain("rounded-full");
        sliderTrackClasses.Should().Contain("bg-muted");
        sliderTrackClasses.Should().Contain("data-horizontal:h-1");

        sliderRangeClasses.Should().Contain("absolute");
        sliderRangeClasses.Should().Contain("bg-primary");
        sliderRangeClasses.Should().Contain("select-none");

        sliderThumbClasses.Should().Contain("relative");
        sliderThumbClasses.Should().Contain("block");
        sliderThumbClasses.Should().Contain("size-3");
        sliderThumbClasses.Should().Contain("rounded-full");
        sliderThumbClasses.Should().Contain("border");
        sliderThumbClasses.Should().Contain("border-ring");
        sliderThumbClasses.Should().NotContain("q-slider");
    }

    [Fact]
    public void NativeSelect_and_input_otp_slots_match_shadcn_base_classes()
    {
        var nativeSelect = Render<NativeSelect>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "option");
                builder.AddAttribute(1, "value", "");
                builder.AddContent(2, "Select status");
                builder.CloseElement();
            })));

        var inputOtp = Render<InputOtp>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<InputOtpGroup>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(groupBuilder =>
                {
                    groupBuilder.OpenComponent<InputOtpSlot>(0);
                    groupBuilder.AddAttribute(1, "Index", 0);
                    groupBuilder.CloseComponent();

                    groupBuilder.OpenComponent<InputOtpSlot>(2);
                    groupBuilder.AddAttribute(3, "Index", 1);
                    groupBuilder.CloseComponent();
                }));
                builder.CloseComponent();

                builder.OpenComponent<InputOtpSeparator>(2);
                builder.CloseComponent();

                builder.OpenComponent<InputOtpInput>(3);
                builder.CloseComponent();
            })));

        string nativeSelectClasses = nativeSelect.Find("[data-slot='native-select']").GetAttribute("class")!;
        string nativeSelectIconClasses = nativeSelect.Find("[data-slot='native-select-icon']").GetAttribute("class")!;
        string inputOtpClasses = inputOtp.Find("[data-slot='input-otp']").GetAttribute("class")!;
        string inputOtpGroupClasses = inputOtp.Find("[data-slot='input-otp-group']").GetAttribute("class")!;
        string inputOtpSlotClasses = inputOtp.Find("[data-slot='input-otp-slot']").GetAttribute("class")!;
        string inputOtpSeparatorClasses = inputOtp.Find("[data-slot='input-otp-separator']").GetAttribute("class")!;

        nativeSelectClasses.Should().Contain("h-8");
        nativeSelectClasses.Should().Contain("rounded-lg");
        nativeSelectClasses.Should().Contain("border-input");
        nativeSelectClasses.Should().Contain("py-1");
        nativeSelectClasses.Should().Contain("pr-8");
        nativeSelectClasses.Should().Contain("pl-2.5");
        nativeSelectClasses.Should().Contain("select-none");
        nativeSelectClasses.Should().Contain("aria-invalid:ring-[3px]");
        nativeSelectClasses.Should().NotContain("q-native-select");

        nativeSelectIconClasses.Should().Contain("right-2.5");
        nativeSelectIconClasses.Should().Contain("size-4");
        nativeSelectIconClasses.Should().Contain("text-muted-foreground");

        inputOtpClasses.Should().Contain("cn-input-otp");
        inputOtpClasses.Should().Contain("flex");
        inputOtpClasses.Should().Contain("items-center");
        inputOtpClasses.Should().Contain("has-disabled:opacity-50");
        inputOtpClasses.Should().NotContain("q-input-otp");

        inputOtpGroupClasses.Should().Contain("flex");
        inputOtpGroupClasses.Should().Contain("items-center");
        inputOtpGroupClasses.Should().Contain("rounded-lg");
        inputOtpGroupClasses.Should().Contain("has-aria-invalid:ring-3");
        inputOtpGroupClasses.Should().NotContain("q-input-otp-group");

        inputOtpSlotClasses.Should().Contain("size-8");
        inputOtpSlotClasses.Should().Contain("border-input");
        inputOtpSlotClasses.Should().Contain("first:rounded-l-lg");
        inputOtpSlotClasses.Should().Contain("data-[active=true]:ring-3");
        inputOtpSlotClasses.Should().NotContain("q-input-otp-slot");

        inputOtpSeparatorClasses.Should().Contain("flex");
        inputOtpSeparatorClasses.Should().Contain("items-center");
        inputOtpSeparatorClasses.Should().Contain("[&_svg:not([class*='size-'])]:size-4");
        inputOtpSeparatorClasses.Should().NotContain("q-input-otp-separator");
    }

    [Fact]
    public void Tabs_slots_match_shadcn_base_classes()
    {
        var cut = Render<Tabs>(parameters => parameters
            .Add(p => p.SelectedTab, "overview")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<TabsList>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(listBuilder =>
                {
                    listBuilder.OpenComponent<TabsTrigger>(0);
                    listBuilder.AddAttribute(1, "Value", "overview");
                    listBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Overview")));
                    listBuilder.CloseComponent();

                    listBuilder.OpenComponent<TabsTrigger>(3);
                    listBuilder.AddAttribute(4, "Value", "analytics");
                    listBuilder.AddAttribute(5, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Analytics")));
                    listBuilder.CloseComponent();
                }));
                builder.CloseComponent();

                builder.OpenComponent<TabsContent>(2);
                builder.AddAttribute(3, "Value", "overview");
                builder.AddAttribute(4, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Summary")));
                builder.CloseComponent();

                builder.OpenComponent<TabsContent>(5);
                builder.AddAttribute(6, "Value", "analytics");
                builder.AddAttribute(7, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Charts")));
                builder.CloseComponent();
            })));

        string tabsClasses = cut.Find("[data-slot='tabs']").GetAttribute("class")!;
        string listClasses = cut.Find("[data-slot='tabs-list']").GetAttribute("class")!;
        string triggerClasses = cut.Find("[data-slot='tabs-trigger']").GetAttribute("class")!;
        string contentClasses = cut.Find("[data-slot='tabs-content']").GetAttribute("class")!;

        tabsClasses.Should().Contain("group/tabs");
        tabsClasses.Should().Contain("flex");
        tabsClasses.Should().Contain("gap-2");
        tabsClasses.Should().Contain("data-horizontal:flex-col");
        tabsClasses.Should().NotContain("q-tabs");

        listClasses.Should().Contain("group/tabs-list");
        listClasses.Should().Contain("inline-flex");
        listClasses.Should().Contain("items-center");
        listClasses.Should().Contain("justify-center");
        listClasses.Should().Contain("rounded-lg");
        listClasses.Should().Contain("p-[3px]");
        listClasses.Should().Contain("text-muted-foreground");
        listClasses.Should().Contain("group-data-horizontal/tabs:h-8");
        listClasses.Should().Contain("bg-muted");
        listClasses.Should().NotContain("q-tabs-list");

        triggerClasses.Should().Contain("relative");
        triggerClasses.Should().Contain("inline-flex");
        triggerClasses.Should().Contain("h-[calc(100%-1px)]");
        triggerClasses.Should().Contain("flex-1");
        triggerClasses.Should().Contain("rounded-md");
        triggerClasses.Should().Contain("border-transparent");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("font-medium");
        triggerClasses.Should().Contain("transition-all");
        triggerClasses.Should().Contain("data-active:bg-background");
        triggerClasses.Should().Contain("group-data-[variant=default]/tabs-list:data-active:shadow-sm");
        triggerClasses.Should().NotContain("q-tabs-trigger");

        contentClasses.Should().Contain("flex-1");
        contentClasses.Should().Contain("text-sm");
        contentClasses.Should().Contain("outline-none");
        contentClasses.Should().NotContain("q-tabs-content");
    }

    [Fact]
    public void AlertDialog_interactive_and_content_slots_match_shadcn_base_classes()
    {
        var title = Render<AlertDialogTitle>(parameters => parameters
            .Add(p => p.ChildContent, "Delete item"));
        var media = Render<AlertDialogMedia>(parameters => parameters
            .Add(p => p.ChildContent, "Icon"));
        var action = Render<AlertDialogAction>(parameters => parameters
            .Add(p => p.ChildContent, "Continue"));
        var cancel = Render<AlertDialogCancel>(parameters => parameters
            .Add(p => p.ChildContent, "Cancel"));

        string titleClasses = title.Find("[data-slot='alert-dialog-title']").GetAttribute("class")!;
        string mediaClasses = media.Find("[data-slot='alert-dialog-media']").GetAttribute("class")!;
        string actionClasses = action.Find("[data-slot='alert-dialog-action']").GetAttribute("class")!;
        string cancelClasses = cancel.Find("[data-slot='alert-dialog-cancel']").GetAttribute("class")!;

        titleClasses.Should().Contain("cn-font-heading");
        titleClasses.Should().Contain("text-lg");
        titleClasses.Should().Contain("leading-none");
        titleClasses.Should().Contain("font-semibold");
        titleClasses.Should().Contain("sm:group-data-[size=default]/alert-dialog-content:group-has-data-[slot=alert-dialog-media]/alert-dialog-content:col-start-2");
        titleClasses.Should().NotContain("q-alert-dialog-title");

        mediaClasses.Should().Contain("mb-2");
        mediaClasses.Should().Contain("inline-flex");
        mediaClasses.Should().Contain("size-16");
        mediaClasses.Should().Contain("items-center");
        mediaClasses.Should().Contain("justify-center");
        mediaClasses.Should().Contain("rounded-md");
        mediaClasses.Should().Contain("bg-muted");
        mediaClasses.Should().Contain("sm:group-data-[size=default]/alert-dialog-content:row-span-2");
        mediaClasses.Should().Contain("*:[svg:not([class*='size-'])]:size-8");
        mediaClasses.Should().NotContain("q-alert-dialog-media");

        actionClasses.Should().Contain("group/button");
        actionClasses.Should().Contain("inline-flex");
        actionClasses.Should().Contain("rounded-lg");
        actionClasses.Should().Contain("bg-primary");
        actionClasses.Should().Contain("text-primary-foreground");
        actionClasses.Should().Contain("h-8");
        actionClasses.Should().Contain("gap-1.5");
        actionClasses.Should().Contain("px-2.5");
        actionClasses.Should().NotContain("q-alert-dialog-action");

        cancelClasses.Should().Contain("inline-flex");
        cancelClasses.Should().Contain("rounded-lg");
        cancelClasses.Should().Contain("border-border");
        cancelClasses.Should().Contain("bg-background");
        cancelClasses.Should().Contain("hover:bg-muted");
        cancelClasses.Should().Contain("h-8");
        cancelClasses.Should().Contain("gap-1.5");
        cancelClasses.Should().Contain("px-2.5");
        cancelClasses.Should().NotContain("q-alert-dialog-cancel");
        cancelClasses.Should().NotContain("q-button");
    }

    [Fact]
    public void Dialog_layout_slots_match_shadcn_base_classes()
    {
        var header = Render<DialogHeader>(parameters => parameters
            .Add(p => p.ChildContent, "Header"));
        var footer = Render<DialogFooter>(parameters => parameters
            .Add(p => p.ChildContent, "Footer"));

        string headerClasses = header.Find("[data-slot='dialog-header']").GetAttribute("class")!;
        string footerClasses = footer.Find("[data-slot='dialog-footer']").GetAttribute("class")!;

        headerClasses.Should().Contain("flex");
        headerClasses.Should().Contain("flex-col");
        headerClasses.Should().Contain("gap-2");
        headerClasses.Should().Contain("text-center");
        headerClasses.Should().Contain("sm:text-left");
        headerClasses.Should().NotContain("q-dialog-header");

        footerClasses.Should().Contain("flex");
        footerClasses.Should().Contain("flex-col-reverse");
        footerClasses.Should().Contain("gap-2");
        footerClasses.Should().Contain("sm:flex-row");
        footerClasses.Should().Contain("sm:justify-end");
        footerClasses.Should().NotContain("q-dialog-footer");
    }

    [Fact]
    public void Sidebar_slots_match_shadcn_base_classes()
    {
        var sidebar = Render<Sidebar>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<SidebarHeader>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Workspace")));
                builder.CloseComponent();

                builder.OpenComponent<SidebarContent>(2);
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<SidebarGroup>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(groupBuilder =>
                    {
                        groupBuilder.OpenComponent<SidebarGroupLabel>(0);
                        groupBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(labelBuilder => labelBuilder.AddContent(0, "Platform")));
                        groupBuilder.CloseComponent();

                        groupBuilder.OpenComponent<SidebarGroupContent>(2);
                        groupBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(groupContentBuilder =>
                        {
                            groupContentBuilder.OpenComponent<SidebarMenu>(0);
                            groupContentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(menuBuilder =>
                            {
                                menuBuilder.OpenComponent<SidebarMenuItem>(0);
                                menuBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(itemBuilder =>
                                {
                                    itemBuilder.OpenComponent<SidebarMenuButton>(0);
                                    itemBuilder.AddAttribute(1, "Active", true);
                                    itemBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(buttonBuilder => buttonBuilder.AddContent(0, "Dashboard")));
                                    itemBuilder.CloseComponent();

                                    itemBuilder.OpenComponent<SidebarMenuAction>(3);
                                    itemBuilder.AddAttribute(4, "ChildContent", (RenderFragment)(actionBuilder => actionBuilder.AddContent(0, "+")));
                                    itemBuilder.CloseComponent();

                                    itemBuilder.OpenComponent<SidebarMenuBadge>(5);
                                    itemBuilder.AddAttribute(6, "ChildContent", (RenderFragment)(badgeBuilder => badgeBuilder.AddContent(0, "4")));
                                    itemBuilder.CloseComponent();
                                }));
                                menuBuilder.CloseComponent();
                            }));
                            groupContentBuilder.CloseComponent();
                        }));
                        groupBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var trigger = Render<SidebarTrigger>();
        var input = Render<SidebarInput>();
        var separator = Render<SidebarSeparator>();

        string sidebarClasses = sidebar.Find("[data-slot='sidebar']").GetAttribute("class")!;
        string gapClasses = sidebar.Find("[data-slot='sidebar-gap']").GetAttribute("class")!;
        string containerClasses = sidebar.Find("[data-slot='sidebar-container']").GetAttribute("class")!;
        string innerClasses = sidebar.Find("[data-slot='sidebar-inner']").GetAttribute("class")!;
        string headerClasses = sidebar.Find("[data-slot='sidebar-header']").GetAttribute("class")!;
        string contentClasses = sidebar.Find("[data-slot='sidebar-content']").GetAttribute("class")!;
        string groupClasses = sidebar.Find("[data-slot='sidebar-group']").GetAttribute("class")!;
        string groupLabelClasses = sidebar.Find("[data-slot='sidebar-group-label']").GetAttribute("class")!;
        string menuClasses = sidebar.Find("[data-slot='sidebar-menu']").GetAttribute("class")!;
        string menuItemClasses = sidebar.Find("[data-slot='sidebar-menu-item']").GetAttribute("class")!;
        string menuButtonClasses = sidebar.Find("[data-slot='sidebar-menu-button']").GetAttribute("class")!;
        string menuActionClasses = sidebar.Find("[data-slot='sidebar-menu-action']").GetAttribute("class")!;
        string menuBadgeClasses = sidebar.Find("[data-slot='sidebar-menu-badge']").GetAttribute("class")!;
        string triggerClasses = trigger.Find("[data-slot='sidebar-trigger']").GetAttribute("class")!;
        string inputClasses = input.Find("[data-slot='sidebar-input']").GetAttribute("class")!;
        string separatorClasses = separator.Find("[data-slot='sidebar-separator']").GetAttribute("class")!;

        sidebarClasses.Should().Contain("group");
        sidebarClasses.Should().Contain("peer");
        sidebarClasses.Should().Contain("hidden");
        sidebarClasses.Should().Contain("text-sidebar-foreground");
        sidebarClasses.Should().Contain("md:block");
        sidebarClasses.Should().NotContain("q-sidebar");

        gapClasses.Should().Contain("relative");
        gapClasses.Should().Contain("transition-[width]");
        gapClasses.Should().Contain("group-data-[collapsible=offcanvas]:w-0");

        containerClasses.Should().Contain("fixed");
        containerClasses.Should().Contain("inset-y-0");
        containerClasses.Should().Contain("md:flex");

        innerClasses.Should().Contain("flex");
        innerClasses.Should().Contain("h-full");
        innerClasses.Should().Contain("w-full");
        innerClasses.Should().Contain("bg-sidebar");

        headerClasses.Should().Contain("flex");
        headerClasses.Should().Contain("flex-col");
        headerClasses.Should().Contain("gap-2");
        headerClasses.Should().Contain("p-2");
        headerClasses.Should().NotContain("q-sidebar-header");

        contentClasses.Should().Contain("no-scrollbar");
        contentClasses.Should().Contain("flex-1");
        contentClasses.Should().Contain("overflow-y-auto");
        contentClasses.Should().NotContain("q-sidebar-content");

        groupClasses.Should().Contain("relative");
        groupClasses.Should().Contain("flex");
        groupClasses.Should().Contain("flex-col");
        groupClasses.Should().NotContain("q-sidebar-group");

        groupLabelClasses.Should().Contain("text-sidebar-foreground/70");
        groupLabelClasses.Should().Contain("h-8");
        groupLabelClasses.Should().Contain("text-xs");
        groupLabelClasses.Should().NotContain("q-sidebar-group-label");

        menuClasses.Should().Contain("flex");
        menuClasses.Should().Contain("w-full");
        menuClasses.Should().Contain("flex-col");
        menuClasses.Should().NotContain("q-sidebar-menu");

        menuItemClasses.Should().Contain("group/menu-item");
        menuItemClasses.Should().Contain("relative");
        menuItemClasses.Should().NotContain("q-sidebar-menu-item");

        menuButtonClasses.Should().Contain("peer/menu-button");
        menuButtonClasses.Should().Contain("w-full");
        menuButtonClasses.Should().Contain("rounded-md");
        menuButtonClasses.Should().Contain("data-[active=true]:bg-sidebar-accent");
        menuButtonClasses.Should().NotContain("q-sidebar-menu-button");

        menuActionClasses.Should().Contain("absolute");
        menuActionClasses.Should().Contain("top-1.5");
        menuActionClasses.Should().Contain("right-1");
        menuActionClasses.Should().Contain("rounded-md");
        menuActionClasses.Should().NotContain("q-sidebar-menu-action");

        menuBadgeClasses.Should().Contain("absolute");
        menuBadgeClasses.Should().Contain("right-1");
        menuBadgeClasses.Should().Contain("text-xs");
        menuBadgeClasses.Should().NotContain("q-sidebar-menu-badge");

        triggerClasses.Should().Contain("inline-flex");
        triggerClasses.Should().Contain("rounded-md");
        triggerClasses.Should().Contain("size-7");
        triggerClasses.Should().NotContain("q-sidebar-trigger");

        inputClasses.Should().Contain("h-8");
        inputClasses.Should().Contain("rounded-md");
        inputClasses.Should().Contain("border-input");
        inputClasses.Should().NotContain("q-sidebar-input");

        separatorClasses.Should().Contain("bg-sidebar-border");
        separatorClasses.Should().Contain("h-px");
        separatorClasses.Should().Contain("mx-2");
        separatorClasses.Should().NotContain("q-sidebar-separator");
    }

    [Fact]
    public void Pagination_slots_match_shadcn_base_classes()
    {
        var pagination = Render<Pagination>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<PaginationContent>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<PaginationItem>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(itemBuilder =>
                    {
                        itemBuilder.OpenComponent<PaginationLink>(0);
                        itemBuilder.AddAttribute(1, "Active", true);
                        itemBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(linkBuilder => linkBuilder.AddContent(0, "1")));
                        itemBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();

                    contentBuilder.OpenComponent<PaginationItem>(2);
                    contentBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(itemBuilder =>
                    {
                        itemBuilder.OpenComponent<PaginationEllipsis>(0);
                        itemBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        string paginationClasses = pagination.Find("[data-slot='pagination']").GetAttribute("class")!;
        string contentClasses = pagination.Find("[data-slot='pagination-content']").GetAttribute("class")!;
        string itemClasses = pagination.Find("[data-slot='pagination-item']").GetAttribute("class")!;
        string linkClasses = pagination.Find("[data-slot='pagination-link']").GetAttribute("class")!;
        string ellipsisClasses = pagination.Find("[data-slot='pagination-ellipsis']").GetAttribute("class")!;
        string? ariaCurrent = pagination.Find("[data-slot='pagination-link']").GetAttribute("aria-current");

        paginationClasses.Should().Contain("mx-auto");
        paginationClasses.Should().Contain("flex");
        paginationClasses.Should().Contain("w-full");
        paginationClasses.Should().Contain("justify-center");
        paginationClasses.Should().NotContain("q-pagination");

        contentClasses.Should().Contain("flex");
        contentClasses.Should().Contain("items-center");
        contentClasses.Should().Contain("gap-1");
        contentClasses.Should().NotContain("q-pagination-content");

        itemClasses.Should().Contain("list-none");
        itemClasses.Should().NotContain("q-pagination-item");
        itemClasses.Should().NotContain("q-pagination-item-active");

        linkClasses.Should().Contain("inline-flex");
        linkClasses.Should().Contain("rounded-md");
        linkClasses.Should().Contain("size-8");
        linkClasses.Should().Contain("border-border");
        linkClasses.Should().Contain("bg-background");
        linkClasses.Should().NotContain("q-pagination-link");
        ariaCurrent.Should().Be("page");

        ellipsisClasses.Should().Contain("flex");
        ellipsisClasses.Should().Contain("size-9");
        ellipsisClasses.Should().Contain("items-center");
        ellipsisClasses.Should().Contain("justify-center");
        ellipsisClasses.Should().NotContain("q-pagination-ellipsis");
    }
}
