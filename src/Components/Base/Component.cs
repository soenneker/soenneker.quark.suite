using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Soenneker.Blazor.Extensions.EventCallback;
using Soenneker.Extensions.String;
using Soenneker.Quark.Utilities;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

///<inheritdoc cref="IComponent"/>
public abstract class Component : CoreComponent, IComponent
{
    /// <summary>
    /// Gets or sets the logger instance for this component.
    /// </summary>
    [Inject]
    protected ILogger<Component> Logger { get; set; } = null!;

    /// <summary>
    /// Gets or sets the Quark configuration options for this component.
    /// </summary>
    [Inject]
    protected QuarkOptions QuarkOptions { get; set; } = null!;

    /// <summary>
    /// Gets or sets additional CSS class names to be merged into the rendered element's class attribute.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Gets or sets additional inline CSS declarations to be merged into the element's style attribute.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }

    /// <summary>
    /// Gets or sets the HTML title attribute (commonly used for native tooltips).
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets whether the element is hidden using the boolean HTML hidden attribute.
    /// </summary>
    [Parameter]
    public bool Hidden { get; set; }

    /// <summary>
    /// Gets or sets the CSS display configuration (e.g., block, inline, flex, grid, none).
    /// </summary>
    [Parameter]
    public CssValue<DisplayBuilder>? Display { get; set; }

    /// <summary>
    /// Gets or sets the CSS visibility configuration (e.g., visible, hidden, collapse).
    /// </summary>
    [Parameter]
    public CssValue<VisibilityBuilder>? Visibility { get; set; }

    /// <summary>
    /// Gets or sets the CSS float configuration (e.g., left, right, none).
    /// </summary>
    [Parameter]
    public CssValue<FloatBuilder>? Float { get; set; }

    /// <summary>
    /// Gets or sets the CSS vertical-align value to apply inline.
    /// </summary>
    [Parameter]
    public CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }

    /// <summary>
    /// Gets or sets the margin configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    [Parameter]
    public CssValue<MarginBuilder>? Margin { get; set; }

    /// <summary>
    /// Gets or sets the padding configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    [Parameter]
    public CssValue<PaddingBuilder>? Padding { get; set; }

    /// <summary>
    /// Gets or sets the CSS position configuration (e.g., static, relative, absolute, fixed, sticky).
    /// </summary>
    [Parameter]
    public CssValue<PositionBuilder>? Position { get; set; }

    /// <summary>
    /// Gets or sets the CSS position offset configuration (top, right, bottom, left).
    /// </summary>
    [Parameter]
    public CssValue<PositionOffsetBuilder>? PositionOffset { get; set; }

    /// <summary>
    /// Gets or sets the CSS width configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    [Parameter]
    public CssValue<WidthBuilder>? Width { get; set; }

    /// <summary>
    /// Gets or sets the CSS min-width configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    [Parameter]
    public CssValue<WidthBuilder>? MinWidth { get; set; }

    /// <summary>
    /// Gets or sets the CSS max-width configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    [Parameter]
    public CssValue<WidthBuilder>? MaxWidth { get; set; }

    /// <summary>
    /// Gets or sets the CSS height configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    [Parameter]
    public CssValue<HeightBuilder>? Height { get; set; }

    /// <summary>
    /// Gets or sets the CSS min-height configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    [Parameter]
    public CssValue<HeightBuilder>? MinHeight { get; set; }

    /// <summary>
    /// Gets or sets the CSS max-height configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    [Parameter]
    public CssValue<HeightBuilder>? MaxHeight { get; set; }

    /// <summary>
    /// Gets or sets the CSS overflow configuration for all axes.
    /// </summary>
    [Parameter]
    public CssValue<OverflowBuilder>? Overflow { get; set; }

    /// <summary>
    /// Gets or sets the CSS overflow-x configuration.
    /// </summary>
    [Parameter]
    public CssValue<OverflowBuilder>? OverflowX { get; set; }

    /// <summary>
    /// Gets or sets the CSS overflow-y configuration.
    /// </summary>
    [Parameter]
    public CssValue<OverflowBuilder>? OverflowY { get; set; }

    /// <summary>
    /// Gets or sets the CSS flex configuration.
    /// </summary>
    [Parameter]
    public CssValue<FlexBuilder>? Flex { get; set; }

    /// <summary>
    /// Gets or sets the CSS gap configuration for flex and grid layouts.
    /// </summary>
    [Parameter]
    public CssValue<GapBuilder>? Gap { get; set; }

    /// <summary>
    /// Gets or sets grid utility classes (grid-cols, col-span, place-items, and related).
    /// </summary>
    [Parameter]
    public CssValue<GridBuilder>? Grid { get; set; }

    /// <summary>
    /// Gets or sets spacing-between-children utility classes (space-x/space-y and reverse variants).
    /// </summary>
    [Parameter]
    public CssValue<SpaceBuilder>? Space { get; set; }

    /// <summary>
    /// Gets or sets divide utility classes (divide-x/y, color, opacity, style, reverse).
    /// </summary>
    [Parameter]
    public CssValue<DivideBuilder>? Divide { get; set; }

    /// <summary>
    /// Gets or sets ring-offset utility classes.
    /// </summary>
    [Parameter]
    public CssValue<RingOffsetBuilder>? RingOffset { get; set; }

    /// <summary>
    /// Gets or sets SVG fill utility classes.
    /// </summary>
    [Parameter]
    public CssValue<FillBuilder>? Fill { get; set; }

    /// <summary>
    /// Gets or sets SVG stroke utility classes.
    /// </summary>
    [Parameter]
    public CssValue<StrokeBuilder>? Stroke { get; set; }

    /// <summary>
    /// Gets or sets gradient utility classes (bg-gradient-to/from/via/to).
    /// </summary>
    [Parameter]
    public CssValue<GradientBuilder>? Gradient { get; set; }

    /// <summary>
    /// Gets or sets letter-spacing utility classes (tracking-*).
    /// </summary>
    [Parameter]
    public CssValue<LetterSpacingBuilder>? LetterSpacing { get; set; }

    /// <summary>
    /// Gets or sets alignment utility classes (justify/items/content/self/justify-items/justify-self).
    /// </summary>
    [Parameter]
    public CssValue<AlignBuilder>? AlignUtility { get; set; }

    /// <summary>
    /// Gets or sets the CSS opacity configuration.
    /// </summary>
    [Parameter]
    public CssValue<OpacityBuilder>? Opacity { get; set; }

    /// <summary>
    /// Gets or sets the CSS z-index configuration.
    /// </summary>
    [Parameter]
    public CssValue<ZIndexBuilder>? ZIndex { get; set; }

    /// <summary>
    /// Gets or sets the CSS pointer-events configuration.
    /// </summary>
    [Parameter]
    public CssValue<PointerEventsBuilder>? PointerEvents { get; set; }

    /// <summary>
    /// Gets or sets the CSS user-select configuration.
    /// </summary>
    [Parameter]
    public CssValue<UserSelectBuilder>? UserSelect { get; set; }

    /// <summary>
    /// Gets or sets the cursor style When hovering over the element.
    /// </summary>
    [Parameter]
    public CssValue<CursorBuilder>? Cursor { get; set; }

    /// <summary>
    /// Gets or sets the CSS background color configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    [Parameter]
    public CssValue<BackgroundColorBuilder>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS border configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    [Parameter]
    public CssValue<BorderBuilder>? Border { get; set; }

    /// <summary>
    /// Gets or sets the CSS border color configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    [Parameter]
    public CssValue<BorderColorBuilder>? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS border radius configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    [Parameter]
    public CssValue<BorderRadiusBuilder>? BorderRadius { get; set; }

    /// <summary>
    /// Gets or sets the CSS text alignment configuration.
    /// </summary>
    [Parameter]
    public CssValue<TextAlignmentBuilder>? TextAlignment { get; set; }

    /// <summary>
    /// Gets or sets the CSS text color configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    [Parameter]
    public CssValue<TextColorBuilder>? TextColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS animation configuration.
    /// </summary>
    [Parameter]
    public CssValue<AnimationBuilder>? Animation { get; set; }

    /// <summary>
    /// Gets or sets the CSS backdrop filter configuration.
    /// </summary>
    [Parameter]
    public CssValue<BackdropFilterBuilder>? BackdropFilter { get; set; }

    /// <summary>
    /// Gets or sets the CSS clip-path configuration.
    /// </summary>
    [Parameter]
    public CssValue<ClipPathBuilder>? ClipPath { get; set; }

    /// <summary>
    /// Gets or sets the CSS filter configuration.
    /// </summary>
    [Parameter]
    public CssValue<FilterBuilder>? Filter { get; set; }

    /// <summary>
    /// Gets or sets the CSS aspect ratio configuration.
    /// </summary>
    [Parameter]
    public CssValue<RatioBuilder>? Ratio { get; set; }

    /// <summary>
    /// Gets or sets the CSS resize configuration.
    /// </summary>
    [Parameter]
    public CssValue<ResizeBuilder>? Resize { get; set; }

    /// <summary>
    /// Gets or sets the CSS screen reader configuration.
    /// </summary>
    [Parameter]
    public CssValue<ScreenReaderBuilder>? ScreenReader { get; set; }

    /// <summary>
    /// Gets or sets the CSS scroll behavior configuration.
    /// </summary>
    [Parameter]
    public CssValue<ScrollBehaviorBuilder>? ScrollBehavior { get; set; }

    /// <summary>
    /// Gets or sets the CSS transform configuration.
    /// </summary>
    [Parameter]
    public CssValue<TransformBuilder>? Transform { get; set; }

    /// <summary>
    /// Gets or sets the CSS transition configuration.
    /// </summary>
    [Parameter]
    public CssValue<TransitionBuilder>? Transition { get; set; }

    /// <summary>
    /// Gets or sets the CSS link opacity configuration.
    /// </summary>
    [Parameter]
    public CssValue<LinkOpacityBuilder>? LinkOpacity { get; set; }

    /// <summary>
    /// Gets or sets the CSS link offset configuration.
    /// </summary>
    [Parameter]
    public CssValue<LinkOffsetBuilder>? LinkOffset { get; set; }

    /// <summary>
    /// Gets or sets the CSS link underline configuration.
    /// </summary>
    [Parameter]
    public CssValue<LinkUnderlineBuilder>? LinkUnderline { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the component is clicked.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the element reference is ready after the first render.
    /// </summary>
    [Parameter]
    public EventCallback<ElementReference> OnElementRefReady { get; set; }

    /// <summary>
    /// Gets or sets the element reference for this component's root element.
    /// </summary>
    protected ElementReference ElementRef { get; set; }

    // -------- Render gate + attribute cache --------
    private bool _shouldRender = true;
    private int _lastRenderKey;
    private Dictionary<string, object>? _cachedAttrs;
    private int _cachedAttrsKey;

    // Invalidate When internal state changes (not Parameters)
    private int _renderVersion;

    public void Refresh()
    {
        InvalidateRender();
        StateHasChanged();
    }

    public Task RefreshOffThread()
    {
        InvalidateRender();
        return InvokeAsync(StateHasChanged);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void InvalidateRender()
    {
        unchecked { _renderVersion++; }

        // recompute once per invalidation (rare), keeps BuildAttributes fast
        _lastRenderKey = ComputeRenderKey();

        // guarantee cache miss even if something odd happens
        _cachedAttrs = null;
        _cachedAttrsKey = 0;

        _shouldRender = true;
    }

    protected override bool ShouldRender()
    {
        if (QuarkOptions.AlwaysRender)
            return true;

        return _shouldRender;
    }

    protected override void OnParametersSet()
    {
        if (QuarkOptions.AlwaysRender)
        {
            _shouldRender = true;
            // still update the key for downstream caches if needed
            _lastRenderKey = ComputeRenderKey();
            return;
        }

        int key = ComputeRenderKey();
        _shouldRender = key != _lastRenderKey;
        _lastRenderKey = key;
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            _ = OnElementRefReady.InvokeIfHasDelegate(ElementRef);

        return Task.CompletedTask;
    }

    // ---- Click handler (OnClick is common enough to be on Component) ----
    protected virtual Task HandleClick(MouseEventArgs e)
    {
        if (!QuarkOptions.AlwaysRender)
            _shouldRender = false;
        return OnClick.InvokeIfHasDelegate(e);
    }


    // -------- Attributes building (cached by render key) --------
    protected virtual IReadOnlyDictionary<string, object> BuildAttributes()
    {
        // We rely on OnParametersSet() + InvalidateRender() to keep _lastRenderKey current.
        int currentKey = _lastRenderKey;

        // Use cached attributes if render key hasn't changed
        if (!QuarkOptions.AlwaysRender && _cachedAttrs is not null && _cachedAttrsKey == currentKey)
            return _cachedAttrs;

        int guess = 14 + (Attributes?.Count ?? 0);
        var attrs = new Dictionary<string, object>(guess);

        var cls = new PooledStringBuilder(64);
        var sty = new PooledStringBuilder(128);

        try
        {
            // Build class order: component/CssValues first, then Class (user override), then Attributes/AdditionalAttributes
            if (Style.HasContent())
                sty.Append(Style!);

            if (Id.HasContent()) attrs["id"] = Id!;
            if (Title.HasContent()) attrs["title"] = Title!;
            if (Hidden) attrs["hidden"] = true;

            AddCss(ref sty, ref cls, Display);
            AddCss(ref sty, ref cls, Border);
            ApplyBorderColor(ref sty, ref cls);
            ApplyBackgroundColor(ref sty, ref cls);
            AddCss(ref sty, ref cls, BorderRadius);
            AddCss(ref sty, ref cls, TextAlignment);
            ApplyTextColor(ref sty, ref cls);
            AddCss(ref sty, ref cls, Flex);
            AddCss(ref sty, ref cls, Gap);
            AddCss(ref sty, ref cls, Grid);
            AddCss(ref sty, ref cls, Space);
            AddCss(ref sty, ref cls, Divide);
            AddCss(ref sty, ref cls, RingOffset);
            AddCss(ref sty, ref cls, Fill);
            AddCss(ref sty, ref cls, Stroke);
            AddCss(ref sty, ref cls, Gradient);
            AddCss(ref sty, ref cls, LetterSpacing);
            AddCss(ref sty, ref cls, AlignUtility);
            AddCss(ref sty, ref cls, VerticalAlign);
            AddCss(ref sty, ref cls, Float);
            AddCss(ref sty, ref cls, Visibility);
            AddCss(ref sty, ref cls, Opacity);
            AddCss(ref sty, ref cls, ZIndex);
            AddCss(ref sty, ref cls, PointerEvents);
            AddCss(ref sty, ref cls, UserSelect);
            AddCss(ref sty, ref cls, Cursor);
            AddCss(ref sty, ref cls, Animation);
            AddCss(ref sty, ref cls, BackdropFilter);
            AddCss(ref sty, ref cls, ClipPath);
            AddCss(ref sty, ref cls, Filter);
            AddCss(ref sty, ref cls, Ratio);
            AddCss(ref sty, ref cls, Resize);
            AddCss(ref sty, ref cls, ScreenReader);
            AddCss(ref sty, ref cls, ScrollBehavior);
            AddCss(ref sty, ref cls, Transform);
            AddCss(ref sty, ref cls, Transition);
            AddCss(ref sty, ref cls, LinkOpacity);
            AddCss(ref sty, ref cls, LinkOffset);
            AddCss(ref sty, ref cls, LinkUnderline);

            AddCss(ref sty, ref cls, Margin);
            AddCss(ref sty, ref cls, Padding);
            AddCss(ref sty, ref cls, Position);
            AddCss(ref sty, ref cls, PositionOffset);
            AddCss(ref sty, ref cls, Width, "width");
            AddCss(ref sty, ref cls, MinWidth, "min-width");
            AddCss(ref sty, ref cls, MaxWidth, "max-width");
            AddCss(ref sty, ref cls, Height, "height");
            AddCss(ref sty, ref cls, MinHeight, "min-height");
            AddCss(ref sty, ref cls, MaxHeight, "max-height");
            AddCss(ref sty, ref cls, Overflow);
            AddCss(ref sty, ref cls, OverflowX);
            AddCss(ref sty, ref cls, OverflowY);

            // User Class and Attributes class last so overrides win
            if (Class.HasContent())
                AppendClass(ref cls, Class!);

            if (Attributes is not null)
            {
                foreach (KeyValuePair<string, object> kv in Attributes)
                {
                    string k = kv.Key;
                    object? v = kv.Value;

                    if (k is "class")
                    {
                        string? s = v as string ?? v?.ToString();

                        if (!string.IsNullOrEmpty(s))
                            AppendClass(ref cls, s);
                    }
                    else if (k is "style")
                    {
                        string? s = v as string ?? v?.ToString();
                        if (!string.IsNullOrEmpty(s))
                            AppendStyleDecl(ref sty, s);
                    }
                    else if (k.Equals("class", StringComparison.OrdinalIgnoreCase))
                    {
                        string? s = v as string ?? v?.ToString();
                        if (!string.IsNullOrEmpty(s))
                            AppendClass(ref cls, s);
                    }
                    else if (k.Equals("style", StringComparison.OrdinalIgnoreCase))
                    {
                        string? s = v as string ?? v?.ToString();
                        if (!string.IsNullOrEmpty(s))
                            AppendStyleDecl(ref sty, s);
                    }
                    else
                    {
                        attrs[k] = v!;
                    }
                }
            }

            if (cls.Length > 0) attrs["class"] = cls.ToString();
            if (sty.Length > 0) attrs["style"] = sty.ToString();

            BuildAttributesCore(attrs);

            // Resolve Tailwind conflicts so user/override classes win
            if (attrs.TryGetValue("class", out var classObj) && classObj is string classStr && classStr.Length > 0)
                attrs["class"] = ClassNames.Combine(classStr);

            // Cache the computed attributes keyed by the render key
            _cachedAttrs = attrs;
            _cachedAttrsKey = currentKey;

            return attrs;
        }
        finally
        {
            sty.Dispose();
            cls.Dispose();
        }
    }

    /// <summary>
    /// Override to add component-specific attributes/classes/styles.
    /// Called before caching. Do not store <paramref name="attrs"/>.
    /// </summary>
    protected virtual void BuildAttributesCore(Dictionary<string, object> attrs) { }

    // ---------- Render key computation ----------
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddIf<T>(ref HashCode hc, CssValue<T>? v) where T : class, ICssBuilder
    {
        if (v is { IsEmpty: false })
            hc.Add(v.Value); // struct add; no boxing
    }

    private int ComputeRenderKey()
    {
        var hc = new HashCode();

        // Internal invalidation version (forces key to change When you call InvalidateRender())
        hc.Add(_renderVersion);

        hc.Add(Class);
        hc.Add(Style);
        hc.Add(Title);
        hc.Add(Hidden);

        AddIf(ref hc, Display);
        AddIf(ref hc, Visibility);
        AddIf(ref hc, Float);
        AddIf(ref hc, VerticalAlign);
        AddIf(ref hc, TextAlignment);
        AddIf(ref hc, TextColor);
        AddIf(ref hc, Margin);
        AddIf(ref hc, Padding);
        AddIf(ref hc, Position);
        AddIf(ref hc, BackgroundColor);
        AddIf(ref hc, Border);
        AddIf(ref hc, BorderColor);
        AddIf(ref hc, BorderRadius);
        AddIf(ref hc, PositionOffset);
        AddIf(ref hc, Width);
        AddIf(ref hc, MinWidth);
        AddIf(ref hc, MaxWidth);
        AddIf(ref hc, Height);
        AddIf(ref hc, MinHeight);
        AddIf(ref hc, MaxHeight);
        AddIf(ref hc, Overflow);
        AddIf(ref hc, OverflowX);
        AddIf(ref hc, OverflowY);
        AddIf(ref hc, Flex);
        AddIf(ref hc, Gap);
        AddIf(ref hc, Grid);
        AddIf(ref hc, Space);
        AddIf(ref hc, Divide);
        AddIf(ref hc, RingOffset);
        AddIf(ref hc, Fill);
        AddIf(ref hc, Stroke);
        AddIf(ref hc, Gradient);
        AddIf(ref hc, LetterSpacing);
        AddIf(ref hc, AlignUtility);
        AddIf(ref hc, Opacity);
        AddIf(ref hc, ZIndex);
        AddIf(ref hc, PointerEvents);
        AddIf(ref hc, UserSelect);
        AddIf(ref hc, Cursor);
        AddIf(ref hc, Animation);
        AddIf(ref hc, BackdropFilter);
        AddIf(ref hc, ClipPath);
        AddIf(ref hc, Filter);
        AddIf(ref hc, Resize);
        AddIf(ref hc, ScreenReader);
        AddIf(ref hc, ScrollBehavior);
        AddIf(ref hc, Transform);
        AddIf(ref hc, Transition);
        AddIf(ref hc, LinkOpacity);
        AddIf(ref hc, LinkOffset);
        AddIf(ref hc, LinkUnderline);

        if (Attributes is not null)
        {
            if (Attributes.TryGetValue("class", out object? clsObj))
                hc.Add(clsObj is string s ? s : clsObj);

            if (Attributes.TryGetValue("style", out object? styObj))
                hc.Add(styObj is string s ? s : styObj);

            if (Attributes.TryGetValue("id", out object? idObj))
                hc.Add(idObj is string s ? s : idObj);
        }

        // derived components add their stuff here
        ComputeRenderKeyCore(ref hc);

        return hc.ToHashCode();
    }

    protected virtual void ComputeRenderKeyCore(ref HashCode hc)
    {

    }

    // ---------- Helpers ----------
    private static EventCallback<TArgs> Compose<TArgs>(ComponentBase owner, Func<TArgs, Task> ours, EventCallback<TArgs> users)
    {
        if (!users.HasDelegate)
            return EventCallback.Factory.Create(owner, ours);

        return EventCallback.Factory.Create<TArgs>(owner, e =>
        {
            Task t = ours(e);
            if (t.IsCompletedSuccessfully)
                return users.InvokeAsync(e);

            return AwaitBoth(t, users, e);

            static async Task AwaitBoth(Task first, EventCallback<TArgs> u, TArgs arg)
            {
                await first.ConfigureAwait(false);
                await u.InvokeAsync(arg).ConfigureAwait(false);
            }
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendClass(ref PooledStringBuilder b, string s)
    {
        if (s.IsNullOrEmpty())
            return;

        if (b.Length != 0)
            b.Append(' ');

        b.Append(s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendStyleDecl(ref PooledStringBuilder b, string nameColonSpace, string value)
    {
        if (b.Length != 0)
        {
            b.Append(';');
            b.Append(' ');
        }

        b.Append(nameColonSpace);
        b.Append(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendStyleDecl(ref PooledStringBuilder b, string fullDecl)
    {
        if (fullDecl.IsNullOrEmpty()) return;
        if (b.Length != 0)
        {
            b.Append(';');
            b.Append(' ');
        }

        b.Append(fullDecl);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss<T>(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<T>? v) where T : class, ICssBuilder
    {
        if (v is { IsEmpty: false })
        {
            var s = v.Value.ToString();
            if (s.Length == 0) return;

            if (v.Value.IsCssStyle)
                AppendStyleDecl(ref styB, s);
            else
                AppendClass(ref clsB, s);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB,
        CssValue<WidthBuilder>? v, ReadOnlySpan<char> propertyName)
    {
        if (v is not { IsEmpty: false }) return;

        var s = v.Value.ToString();

        if (s.Length == 0) return;

        if (!v.Value.IsCssStyle)
        {
            AppendClass(ref clsB, s);
            return;
        }

        // raw decl already? keep as-is
        if (s.AsSpan().IndexOf(':') >= 0)
            AppendStyleDecl(ref styB, s);
        else
            AppendStyleDecl(ref styB, propertyName, s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB,
        CssValue<HeightBuilder>? v, ReadOnlySpan<char> propertyName)
    {
        if (v is not { IsEmpty: false }) return;

        var s = v.Value.ToString();
        if (s.Length == 0) return;

        if (!v.Value.IsCssStyle)
        {
            AppendClass(ref clsB, s);
            return;
        }

        if (s.AsSpan().IndexOf(':') >= 0)
            AppendStyleDecl(ref styB, s);
        else
            AppendStyleDecl(ref styB, propertyName, s);
    }

    // === Attribute helpers (same surface as your original) ===
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static string EnsureClass(string? existing, string? toAdd)
    {
        if (toAdd.IsNullOrEmpty())
            return existing ?? string.Empty;

        if (existing.IsNullOrEmpty())
            return toAdd;

        return existing.Contains(toAdd!, StringComparison.Ordinal) ? existing : string.Concat(existing, " ", toAdd);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static string AppendToClass(string? existing, string toAdd)
    {
        if (toAdd.IsNullOrEmpty())
            return existing ?? string.Empty;

        if (existing.IsNullOrEmpty())
            return toAdd;

        return $"{existing} {toAdd}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void EnsureClassAttr(Dictionary<string, object> attrs, string token)
    {
        attrs.TryGetValue("class", out object? clsObj);
        string cls = EnsureClass(clsObj?.ToString(), token);
        if (cls.Length > 0) attrs["class"] = cls;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendToClassAttr(Dictionary<string, object> attrs, string token)
    {
        attrs.TryGetValue("class", out object? clsObj);
        string cls = AppendToClass(clsObj?.ToString(), token);

        if (cls.Length > 0)
            attrs["class"] = cls;
    }

    /// <summary>
    /// Appends to the class attribute using PooledStringBuilder internally, accepting multiple class strings.
    /// Merges with any existing class attribute. Null or empty strings are automatically skipped.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendClassAttribute(Dictionary<string, object> attrs, params string?[] classes)
    {
        var cls = new PooledStringBuilder(64);

        try
        {
            // Include existing class first
            if (attrs.TryGetValue("class", out object? existing))
            {
                var existingStr = existing.ToString();

                if (!existingStr.IsNullOrWhiteSpace())
                    cls.Append(existingStr);
            }

            foreach (string? c in classes)
            {
                if (!c.IsNullOrWhiteSpace())
                    AppendClass(ref cls, c!);
            }

            if (cls.Length > 0)
                attrs["class"] = cls.ToString();
        }
        finally
        {
            cls.Dispose();
        }
    }

    /// <summary>
    /// Builds the class attribute by providing a PooledStringBuilder to the callback.
    /// Merges with any existing class attribute. The framework manages pooling lifecycle.
    /// Use AppendClass(ref cls, "yourClass") within the callback to add classes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void BuildClassAttribute(Dictionary<string, object> attrs, BuildClassAction builder)
    {
        var cls = new PooledStringBuilder(64);
        try
        {
            // Include existing class first
            if (attrs.TryGetValue("class", out object? existing))
            {
                var existingStr = existing.ToString();

                if (existingStr.HasContent())
                    cls.Append(existingStr);
            }

            builder(ref cls); // Let the component add classes

            if (cls.Length > 0)
                attrs["class"] = cls.ToString();
        }
        finally
        {
            cls.Dispose();
        }
    }

    /// <summary>
    /// Builds the style attribute by providing a PooledStringBuilder to the callback.
    /// Merges with any existing style attribute. The framework manages pooling lifecycle.
    /// Use AppendStyle(ref sty, "name", "value") or AppendStyleDecl(ref sty, "full: decl") within the callback to add styles.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void BuildStyleAttribute(Dictionary<string, object> attrs, BuildStyleAction builder)
    {
        var sty = new PooledStringBuilder(64);
        try
        {
            // Include existing style first
            if (attrs.TryGetValue("style", out object? existing))
            {
                var existingStr = existing.ToString();

                if (existingStr.HasContent())
                    sty.Append(existingStr);
            }

            builder(ref sty); // Let the component add styles

            if (sty.Length > 0)
                attrs["style"] = sty.ToString();
        }
        finally
        {
            sty.Dispose();
        }
    }

    /// <summary>
    /// Builds both class and style attributes by providing PooledStringBuilders to the callback.
    /// Merges with any existing class and style attributes. The framework manages pooling lifecycle.
    /// Use AppendClass(ref cls, "yourClass") and AppendStyleDecl(ref sty, "full: decl") within the callback.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void BuildClassAndStyleAttributes(Dictionary<string, object> attrs, BuildClassAndStyleAction builder)
    {
        // Grab existing values with the lowest possible overhead.
        attrs.TryGetValue("class", out object? existingClassObj);
        attrs.TryGetValue("style", out object? existingStyleObj);

        string? existingClassStr = existingClassObj as string ?? existingClassObj?.ToString();
        string? existingStyleStr = existingStyleObj as string ?? existingStyleObj?.ToString();

        int existingClassLen = existingClassStr?.Length ?? 0;
        int existingStyleLen = existingStyleStr?.Length ?? 0;

        // If you want to avoid renting larger buffers, seed capacity from existing lengths.
        var cls = new PooledStringBuilder(Math.Max(32, existingClassLen + 32));
        var sty = new PooledStringBuilder(Math.Max(32, existingStyleLen + 32));

        try
        {
            if (existingClassLen != 0)
                cls.Append(existingClassStr!);

            if (existingStyleLen != 0)
                sty.Append(existingStyleStr!);

            builder(ref cls, ref sty);

            // ---- CLASS ----
            if (cls.Length == 0)
            {
                // Optional: remove to avoid rendering empty attributes
                // attrs.Remove("class");
            }
            else if (existingClassObj is string && cls.Length == existingClassLen)
            {
                // Builder didn't change class -> keep the original string instance (no new string alloc).
                attrs["class"] = existingClassObj;
            }
            else
            {
                attrs["class"] = cls.ToString();
            }

            // ---- STYLE ----
            if (sty.Length == 0)
            {
                // Optional: remove to avoid rendering empty attributes
                // attrs.Remove("style");
            }
            else if (existingStyleObj is string && sty.Length == existingStyleLen)
            {
                attrs["style"] = existingStyleObj;
            }
            else
            {
                attrs["style"] = sty.ToString();
            }
        }
        finally
        {
            cls.Dispose();
            sty.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void SetOrRemove(Dictionary<string, object> attrs, string name, bool condition, object trueValue)
    {
        if (condition) attrs[name] = trueValue;
        else attrs.Remove(name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendStyleDeclAttr(Dictionary<string, object> attrs, string fullDecl)
    {
        if (string.IsNullOrWhiteSpace(fullDecl)) return;

        attrs.TryGetValue("style", out object? styleObj);

        if (styleObj is string existing && existing.Length != 0)
        {
            using var b = new PooledStringBuilder(existing.Length + 2 + fullDecl.Length);

            b.Append(existing);

            if (existing.Length != 0)
            {
                if (existing[^1] != ';')
                    b.Append(';');

                b.Append(' ');
            }

            b.Append(fullDecl);
            attrs["style"] = b.ToString();

            return;
        }

        // no existing
        attrs["style"] = fullDecl;
    }

    /// <summary>
    /// Applies border color styling. Override this method in derived components to customize border color application.
    /// </summary>
    /// <param name="sty">String builder for inline styles</param>
    /// <param name="cls">String builder for CSS classes</param>
    protected virtual void ApplyBorderColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        AddCss(ref sty, ref cls, BorderColor);
    }

    /// <summary>
    /// Applies text color styling. Override this method in derived components to customize text color application.
    /// </summary>
    /// <param name="sty">String builder for inline styles</param>
    /// <param name="cls">String builder for CSS classes</param>
    protected virtual void ApplyTextColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        AddCss(ref sty, ref cls, TextColor);
    }

    /// <summary>
    /// Applies background color styling. Override this method in derived components to customize background color application.
    /// </summary>
    /// <param name="sty">String builder for inline styles</param>
    /// <param name="cls">String builder for CSS classes</param>
    protected virtual void ApplyBackgroundColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        AddColorCss(ref sty, ref cls, BackgroundColor, "bg", "background-color");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AddColorCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB,
        CssValue<BackgroundColorBuilder>? v, ReadOnlySpan<char> classPrefix, ReadOnlySpan<char> cssProperty)
    {
        if (v is not { IsEmpty: false }) return;

        if (v.Value.TryGetThemeToken(out string? token) && token is not null)
        {
            AppendClassToken(ref clsB, classPrefix, token);
            return;
        }

        var result = v.Value.ToString();
        if (result.Length == 0) return;

        if (v.Value.IsCssStyle)
            AppendStyleDecl(ref styB, cssProperty, result);
        else
            AppendClassToken(ref clsB, classPrefix, result);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AppendClassToken(ref PooledStringBuilder b, ReadOnlySpan<char> prefix, string token)
    {
        if (token.Length == 0) return;

        if (b.Length != 0)
            b.Append(' ');

        b.Append(prefix);
        b.Append('-');
        b.Append(token);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendStyleDecl(ref PooledStringBuilder b, ReadOnlySpan<char> name, string value)
    {
        if (value.Length == 0) return;

        if (b.Length != 0)
        {
            b.Append(';');
            b.Append(' ');
        }

        b.Append(name);
        b.Append(": ");
        b.Append(value);
    }
}