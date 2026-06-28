using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;


namespace Soenneker.Quark.Suite.Tests;

public sealed class BuilderRuntimeContractTests : BunitContext
{
    public BuilderRuntimeContractTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
    }

    [Test]
    public void Cursor_default_maps_to_cursor_default()
    {
        Cursor.Default.ToClass().Should().Be("cursor-default");
    }

    [Test]
    public void Overflow_axis_builders_emit_axis_specific_classes()
    {
        Overflow.X.Hidden.ToClass().Should().Be("overflow-x-hidden");
        Overflow.Y.Auto.ToClass().Should().Be("overflow-y-auto");
    }

    [Test]
    public void Padding_builder_supports_axis_specific_arbitrary_spacing_tokens()
    {
        Padding.Is2.OnX.Token("1.5").OnY.ToClass().Should().Be("px-2 py-1.5");
    }

    [Test]
    public void Physical_side_builders_use_left_and_right_tokens_instead_of_inline_start_end()
    {
        Padding.Token("8").FromRight.Is2.FromLeft.Token("1.5").OnY.ToClass().Should().Be("pr-8 pl-2 py-1.5");
        Border.Is1.ToClass().Should().Be("border");
        Border.Is1.FromBottom.ToClass().Should().Be("border-b");
        Border.Is4.FromLeft.ToClass().Should().Be("border-l-4");
        Margin.Is3.FromRight.ToClass().Should().Be("mr-3");
        Margin.Token("-1").OnX.ToClass().Should().Be("-mx-1");
        Margin.Token("1.5").FromTop.ToClass().Should().Be("mt-1.5");
    }

    [Test]
    public void MaxHeight_uses_height_builder_output_without_rewriting()
    {
        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.MaxHeight, Height.Token("72")));

        var box = cut.Find("[data-slot='test-box']");
        var classes = box.GetAttribute("class")!;
        var style = box.GetAttribute("style");
        var classTokens = classes.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        classTokens.Should().Contain("h-72");
        classTokens.Should().NotContain("max-h-72");
        style.Should().BeNull();
    }

    [Test]
    public void Component_cursor_and_axis_overflow_properties_use_builder_output()
    {
        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.Cursor, Cursor.Default)
            .Add(p => p.OverflowX, Overflow.X.Hidden)
            .Add(p => p.OverflowY, Overflow.Y.Auto)
            .Add(p => p.Padding, Padding.Is2.OnX.Token("1.5").OnY));

        var box = cut.Find("[data-slot='test-box']");
        var classes = box.GetAttribute("class")!;

        classes.Should().Contain("cursor-default");
        classes.Should().Contain("overflow-x-hidden");
        classes.Should().Contain("overflow-y-auto");
        classes.Should().Contain("px-2");
        classes.Should().Contain("py-1.5");
    }

    [Test]
    public void Component_position_offset_side_properties_use_builder_output()
    {
        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.Position, Position.Absolute)
            .Add(p => p.Top, Top.Is1)
            .Add(p => p.Right, Right.Token("2.5"))
            .Add(p => p.Bottom, Bottom.Is0)
            .Add(p => p.Left, Left.Px));

        var box = cut.Find("[data-slot='test-box']");
        var classes = box.GetAttribute("class")!;

        classes.Should().Contain("absolute");
        classes.Should().Contain("top-1");
        classes.Should().Contain("right-2.5");
        classes.Should().Contain("bottom-0");
        classes.Should().Contain("left-px");
    }

    [Test]
    public void Component_shrink_property_uses_builder_output()
    {
        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.Shrink, Shrink.Is0.OnMd.Is1));

        var box = cut.Find("[data-slot='test-box']");
        var classes = box.GetAttribute("class")!;

        classes.Should().Contain("shrink-0");
        classes.Should().Contain("md:shrink");
    }

    [Test]
    public void Component_flex_direction_wrap_and_grow_properties_use_builder_output()
    {
        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.FlexDirection, FlexDirection.Col.OnMd.Row)
            .Add(p => p.FlexWrap, FlexWrap.Wrap.OnLg.NoWrap)
            .Add(p => p.Grow, Grow.Is0.OnSm.Is1));

        var box = cut.Find("[data-slot='test-box']");
        var classes = box.GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("md:flex");
        classes.Should().Contain("md:flex-row");
        classes.Should().Contain("flex-wrap");
        classes.Should().Contain("lg:flex");
        classes.Should().Contain("lg:flex-nowrap");
        classes.Should().Contain("grow-0");
        classes.Should().Contain("sm:grow");
    }

    [Test]
    public void Component_duration_property_uses_builder_output()
    {
        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.Duration, Duration.Is150.OnHover.Token("duration-[375ms]")));

        var box = cut.Find("[data-slot='test-box']");
        var classes = box.GetAttribute("class")!;

        classes.Should().Contain("duration-150");
        classes.Should().Contain("hover:duration-[375ms]");
    }

    [Test]
    public void Theme_component_options_convert_duration_to_transition_duration()
    {
        var theme = new Theme
        {
            Divs = new DivOptions
            {
                Duration = Duration.Is300
            }
        };

        var result = ComponentsCssGenerator.Generate(theme);

        result.Should().Be("[data-slot='div'] {\n  transition-duration: 300ms;\n}");
    }

    [Test]
    public void DataTable_theme_options_generate_scoped_child_component_css()
    {
        var theme = new Theme
        {
            DataTables = new DataTableThemeOptions
            {
                Width = Width.IsFull,
                Anchors = new AnchorOptions
                {
                    Display = Display.InlineFlex,
                    DecorationLine = DecorationLine.None
                },
                AnchorDivs = new DivOptions
                {
                    MinWidth = Width.Is0
                },
                AnchorLeadingSpans = new SpanOptions
                {
                    Shrink = Shrink.Is0
                },
                AnchorSmalls = new SmallOptions
                {
                    Display = Display.Block,
                    TextOverflow = TextOverflow.Ellipsis
                },
                AnchorSpans = new SpanOptions
                {
                    Display = Display.Block,
                    TextOverflow = TextOverflow.Ellipsis
                },
                Tds = new TdOptions
                {
                    Padding = Padding.OnY.Is3
                },
                Inputs = new InputOptions
                {
                    Width = Width.IsFull
                },
                PaginationLinks = new PaginationLinkOptions
                {
                    Display = Display.InlineFlex
                },
                Searches = new DataTableSearchOptions
                {
                    Display = Display.Flex
                },
                Selects = new SelectOptions
                {
                    Width = Width.IsFull
                },
                Spans = new SpanOptions
                {
                    Display = Display.Block,
                    TextOverflow = TextOverflow.Ellipsis
                }
            }
        };

        string result = ComponentsCssGenerator.Generate(theme);

        result.Should().Contain(".q-datatable {\n  width: 100%;\n}");
        result.Should().Contain(".q-datatable tbody td > [data-slot='anchor'] {\n  display: inline-flex;\n  text-decoration: none;\n}");
        result.Should().Contain(".q-datatable tbody td > [data-slot='anchor'] > [data-slot='div'] {\n  min-width: 0;\n}");
        result.Should().Contain(".q-datatable tbody td > [data-slot='anchor'] > [data-slot='span']:first-child {\n  flex-shrink: 0;\n}");
        result.Should().Contain(".q-datatable tbody td > [data-slot='anchor'] > [data-slot='div'] > [data-slot='small'] {\n  display: block;\n  text-overflow: ellipsis;\n}");
        result.Should().Contain(".q-datatable tbody td > [data-slot='anchor'] > [data-slot='div'] > [data-slot='span'] {\n  display: block;\n  text-overflow: ellipsis;\n}");
        result.Should().Contain(".q-datatable [data-slot='datatable-search-input'] {\n  width: 100%;\n}");
        result.Should().Contain(".q-datatable [data-slot='datatable-pagination'] [data-slot='pagination-link'] {\n  display: inline-flex;\n}");
        result.Should().Contain(".q-datatable [data-slot='datatable-search'] {\n  display: flex;\n}");
        result.Should().Contain(".q-datatable [data-slot='datatable-page-size-select'] {\n  width: 100%;\n}");
        result.Should().Contain(".q-datatable tbody [data-slot='table-cell'] {\n  padding-top: 0.75rem;\n  padding-bottom: 0.75rem;\n}");
        result.Should().Contain(".q-datatable tbody td [data-slot='span'] {\n  display: block;\n  text-overflow: ellipsis;\n}");
    }

    [Test]
    public void DataTable_scoped_child_options_allow_relative_selector_overrides()
    {
        var theme = new Theme
        {
            DataTables = new DataTableThemeOptions
            {
                Anchors = new AnchorOptions
                {
                    Selector = "& tbody td > a[data-entity-link]",
                    MinWidth = Width.Is0
                }
            }
        };

        string result = ComponentsCssGenerator.Generate(theme);

        result.Should().Be(".q-datatable tbody td > a[data-entity-link] {\n  min-width: 0;\n}");
    }

    [Test]
    public void Composite_theme_options_generate_nested_child_component_css()
    {
        var theme = new Theme
        {
            Cards = new CardOptions
            {
                Headers = new CardHeaderOptions
                {
                    Display = Display.Flex
                },
                Titles = new CardTitleOptions
                {
                    FontWeight = FontWeight.Semibold
                }
            },
            Fields = new FieldOptions
            {
                Labels = new FieldLabelOptions
                {
                    Display = Display.Block
                },
                Inputs = new InputOptions
                {
                    Width = Width.IsFull
                }
            },
            Dialogs = new DialogOptions
            {
                Titles = new DialogTitleOptions
                {
                    FontWeight = FontWeight.Semibold
                },
                Descriptions = new DialogDescriptionOptions
                {
                    TextSize = TextSize.Sm
                }
            },
            Dropdowns = new DropdownOptions
            {
                Items = new DropdownItemOptions
                {
                    Display = Display.Flex
                },
                Shortcuts = new DropdownShortcutOptions
                {
                    TextSize = TextSize.Xs
                }
            },
            Selects = new SelectOptions
            {
                Triggers = new SelectTriggerOptions
                {
                    Width = Width.IsFull
                },
                Items = new SelectItemOptions
                {
                    Display = Display.Flex
                }
            },
            Alerts = new AlertOptions
            {
                Titles = new AlertTitleOptions
                {
                    FontWeight = FontWeight.Semibold
                },
                Icons = new IconOptions
                {
                    Display = Display.InlineBlock
                }
            },
            Breadcrumbs = new BreadcrumbOptions
            {
                Links = new BreadcrumbLinkOptions
                {
                    TextColor = TextColor.Foreground
                },
                Separators = new BreadcrumbSeparatorOptions
                {
                    Display = Display.InlineFlex
                }
            },
            ButtonGroups = new ButtonGroupOptions
            {
                Buttons = new ButtonOptions
                {
                    Width = Width.IsFull
                },
                Texts = new ButtonGroupTextOptions
                {
                    TextSize = TextSize.Sm
                }
            },
            Paginations = new PaginationOptions
            {
                Contents = new PaginationContentOptions
                {
                    Gap = Gap.Is1
                },
                Links = new PaginationLinkOptions
                {
                    Display = Display.InlineFlex
                }
            },
            OrderedLists = new OrderedListOptions
            {
                Items = new OrderedListItemOptions
                {
                    Padding = Padding.FromLeft.Is2
                }
            },
            Progresses = new ProgressOptions
            {
                Indicators = new ProgressIndicatorOptions
                {
                    BackgroundColor = BackgroundColor.Primary
                }
            },
            Tabs = new TabsOptions
            {
                Lists = new TabsListOptions
                {
                    Display = Display.Flex
                },
                Triggers = new TabOptions
                {
                    FontWeight = FontWeight.Medium
                }
            },
            Tables = new TableOptions
            {
                Containers = new TableContainerOptions
                {
                    Display = Display.Block
                },
                Tds = new TdOptions
                {
                    Padding = Padding.Is2
                },
                Ths = new ThOptions
                {
                    FontWeight = FontWeight.Semibold
                }
            },
            Trees = new TreeOptions
            {
                ItemLabels = new TreeItemLabelOptions
                {
                    Padding = Padding.OnX.Is2
                },
                DragLines = new TreeDragLineOptions
                {
                    Display = Display.Block
                }
            },
            UnorderedLists = new UnorderedListOptions
            {
                Items = new UnorderedListItemOptions
                {
                    Display = Display.ListItem
                }
            }
        };

        string result = ComponentsCssGenerator.Generate(theme);

        result.Should().Contain("[data-slot='card'] [data-slot='card-header'] {\n  display: flex;\n}");
        result.Should().Contain("[data-slot='card'] [data-slot='card-title'] {\n  font-weight: 600;\n}");
        result.Should().Contain("[data-slot='field'] [data-slot='field-label'] {\n  display: block;\n}");
        result.Should().Contain("[data-slot='field'] [data-slot='input'] {\n  width: 100%;\n}");
        result.Should().Contain("[data-slot='dialog-title'] {\n  font-weight: 600;\n}");
        result.Should().Contain("[data-slot='dialog-description'] {\n  font-size: var(--text-sm);\n}");
        result.Should().Contain("[data-slot='dropdown-menu-item'] {\n  display: flex;\n}");
        result.Should().Contain("[data-slot='dropdown-menu-shortcut'] {\n  font-size: var(--text-xs);\n}");
        result.Should().Contain("[data-slot='select-trigger'] {\n  width: 100%;\n}");
        result.Should().Contain("[data-slot='select-item'] {\n  display: flex;\n}");
        result.Should().Contain("[data-slot='alert'] [data-slot='alert-title'] {\n  font-weight: 600;\n}");
        result.Should().Contain("[data-slot='alert'] [data-slot='icon'] {\n  display: inline-block;\n}");
        result.Should().Contain("[data-slot='breadcrumb'] [data-slot='breadcrumb-link'] {\n  color: var(--foreground);\n}");
        result.Should().Contain("[data-slot='breadcrumb'] [data-slot='breadcrumb-separator'] {\n  display: inline-flex;\n}");
        result.Should().Contain("[data-slot='button-group'] [data-slot='button'] {\n  width: 100%;\n}");
        result.Should().Contain("[data-slot='button-group'] [data-slot='button-group-text'] {\n  font-size: var(--text-sm);\n}");
        result.Should().Contain("[data-slot='ordered-list'] [data-slot='ordered-list-item'] {\n  padding-left: 0.5rem;\n}");
        result.Should().Contain("[data-slot='pagination'] [data-slot='pagination-content'] {\n  gap: 0.25rem;\n}");
        result.Should().Contain("[data-slot='pagination'] [data-slot='pagination-link'] {\n  display: inline-flex;\n}");
        result.Should().Contain("[data-slot='progress'] [data-slot='progress-indicator'] {\n  background-color: var(--primary);\n}");
        result.Should().Contain("[data-slot='tabs'] [data-slot='tabs-list'] {\n  display: flex;\n}");
        result.Should().Contain("[data-slot='tabs'] [data-slot='tabs-trigger'] {\n  font-weight: 500;\n}");
        result.Should().Contain("[data-slot='table-container'] {\n  display: block;\n}");
        result.Should().Contain("[data-slot='table'] [data-slot='table-cell'] {\n  padding: 0.5rem;\n}");
        result.Should().Contain("[data-slot='table'] [data-slot='table-head'] {\n  font-weight: 600;\n}");
        result.Should().Contain("[data-slot='tree'] [data-slot='tree-item-label'] {\n  padding-left: 0.5rem;\n  padding-right: 0.5rem;\n}");
        result.Should().Contain("[data-slot='tree'] [data-slot='tree-drag-line'] {\n  display: block;\n}");
        result.Should().Contain("[data-slot='unordered-list'] [data-slot='unordered-list-item'] {\n  display: list-item;\n}");
    }

    [Test]
    public void Class_merging_preserves_identical_tokens()
    {
        CssValue<FlexBuilder> flex = "flex flex-col";

        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.Display, Display.Flex)
            .Add(p => p.Flex, flex)
            .Add(p => p.Class, "flex flex flex-col"));

        var box = cut.Find("[data-slot='test-box']");
        var tokens = box.GetAttribute("class")!
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        tokens.Count(token => token == "flex").Should().Be(4);
        tokens.Count(token => token == "flex-col").Should().Be(2);
    }

    [Test]
    public void Component_data_slot_parameter_overrides_component_default_slot()
    {
        var cut = Render<TestRenderBox>(parameters => parameters
            .Add(p => p.DataSlot, "custom-box"));

        cut.Find("[data-slot='custom-box']").Should().NotBeNull();
    }

    [Test]
    public void Section_has_no_default_padding_class()
    {
        var cut = Render<Section>();

        var section = cut.Find("section");
        var classes = section.GetAttribute("class");

        classes.Should().BeNullOrEmpty();
    }

    private sealed class TestRenderBox : Element
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, BuildAttributes());
            builder.CloseElement();
        }

        protected override void BuildAttributesCore(Dictionary<string, object> attributes)
        {
            base.BuildAttributesCore(attributes);
            attributes["data-slot"] = "test-box";
        }
    }
}
