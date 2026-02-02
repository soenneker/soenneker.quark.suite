using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Soenneker.Blazor.Extensions.EventCallback;
using Soenneker.Extensions.String;
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
    /// Gets or sets the cursor style when hovering over the element.
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
    /// Gets or sets the CSS clearfix configuration.
    /// </summary>
    [Parameter]
    public CssValue<ClearfixBuilder>? Clearfix { get; set; }

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
    /// Gets or sets the CSS stretched link configuration.
    /// </summary>
    [Parameter]
    public CssValue<StretchedLinkBuilder>? StretchedLink { get; set; }

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
    /// Gets or sets the callback invoked when the component is clicked.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the element reference is ready after the first render.
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

        var key = ComputeRenderKey();
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
        // Refresh render key here so internal state changes invalidate the cache
        var currentKey = _lastRenderKey;
        if (!QuarkOptions.AlwaysRender)
        {
            currentKey = ComputeRenderKey();
            if (currentKey != _lastRenderKey)
                _lastRenderKey = currentKey;
        }

        // Use cached attributes if render key hasn't changed
        if (!QuarkOptions.AlwaysRender && _cachedAttrs is not null && _cachedAttrsKey == currentKey)
            return _cachedAttrs;

        var guess = 14 + (Attributes?.Count ?? 0);
        var attrs = new Dictionary<string, object>(guess);

        var cls = new PooledStringBuilder(64);
        var sty = new PooledStringBuilder(128);

        try
        {
            if (Class.HasContent()) 
                cls.Append(Class!);

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
            AddCss(ref sty, ref cls, Clearfix);
            AddCss(ref sty, ref cls, ClipPath);
            AddCss(ref sty, ref cls, Filter);
            AddCss(ref sty, ref cls, Ratio);
            AddCss(ref sty, ref cls, Resize);
            AddCss(ref sty, ref cls, ScreenReader);
            AddCss(ref sty, ref cls, ScrollBehavior);
            AddCss(ref sty, ref cls, StretchedLink);
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

            if (Attributes is not null)
            {
                foreach (var kv in Attributes)
                {
                    var k = kv.Key;

                    if (k is null)
                        continue;

                    if (k.EqualsIgnoreCase("class"))
                    {
                        AppendClass(ref cls, kv.Value?.ToString() ?? "");
                    }
                    else if (k.EqualsIgnoreCase("style"))
                    {
                        AppendStyleDecl(ref sty, kv.Value?.ToString() ?? "");
                    }
                    else
                    {
                        attrs[k] = kv.Value!;
                    }
                }
            }

            if (cls.Length > 0) attrs["class"] = cls.ToString();
            if (sty.Length > 0) attrs["style"] = sty.ToString();

            BuildAttributesCore(attrs);

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
        AddIf(ref hc, Opacity);
        AddIf(ref hc, ZIndex);
        AddIf(ref hc, PointerEvents);
        AddIf(ref hc, UserSelect);
        AddIf(ref hc, Cursor);
        AddIf(ref hc, Animation);
        AddIf(ref hc, BackdropFilter);
        AddIf(ref hc, Clearfix);
        AddIf(ref hc, ClipPath);
        AddIf(ref hc, Filter);
        AddIf(ref hc, Resize);
        AddIf(ref hc, ScreenReader);
        AddIf(ref hc, ScrollBehavior);
        AddIf(ref hc, StretchedLink);
        AddIf(ref hc, Transform);
        AddIf(ref hc, Transition);
        AddIf(ref hc, LinkOpacity);
        AddIf(ref hc, LinkOffset);
        AddIf(ref hc, LinkUnderline);

        if (Attributes is not null)
        {
            if (Attributes.TryGetValue("class", out var cls)) hc.Add(cls?.ToString());
            if (Attributes.TryGetValue("style", out var sty)) hc.Add(sty?.ToString());
            if (Attributes.TryGetValue("id", out var id)) hc.Add(id?.ToString());
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
        var usersCopy = users; // stabilize
        return EventCallback.Factory.Create<TArgs>(owner, async e =>
        {
            await ours(e);
            await usersCopy.InvokeAsync(e);
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
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<HeightBuilder>? v, string propertyName)
    {
        if (v is { IsEmpty: false })
        {
            var s = v.Value.ToString();
            if (s.Length == 0) return;

            if (v.Value.IsCssStyle)
            {
                // If it's already a full declaration (contains colon), use it as-is
                if (s.Contains(':'))
                    AppendStyleDecl(ref styB, s);
                else
                    // Format as property: value
                    AppendStyleDecl(ref styB, $"{propertyName}: {s}");
            }
            else
                AppendClass(ref clsB, s);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<WidthBuilder>? v, string propertyName)
    {
        if (v is { IsEmpty: false })
        {
            var s = v.Value.ToString();
            if (s.Length == 0) return;

            if (v.Value.IsCssStyle)
            {
                // If it's already a full declaration (contains colon), use it as-is
                if (s.Contains(':'))
                    AppendStyleDecl(ref styB, s);
                else
                    // Format as property: value
                    AppendStyleDecl(ref styB, $"{propertyName}: {s}");
            }
            else
                AppendClass(ref clsB, s);
        }
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
    protected static void EnsureClassAttr(IDictionary<string, object> attrs, string token)
    {
        attrs.TryGetValue("class", out var clsObj);
        var cls = EnsureClass(clsObj?.ToString(), token);
        if (cls.Length > 0) attrs["class"] = cls;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendToClassAttr(IDictionary<string, object> attrs, string token)
    {
        attrs.TryGetValue("class", out var clsObj);
        var cls = AppendToClass(clsObj?.ToString(), token);

        if (cls.Length > 0) 
            attrs["class"] = cls;
    }

    /// <summary>
    /// Appends to the class attribute using PooledStringBuilder internally, accepting multiple class strings.
    /// Merges with any existing class attribute. Null or empty strings are automatically skipped.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendClassAttribute(IDictionary<string, object> attrs, params string?[] classes)
    {
        var cls = new PooledStringBuilder(64);

        try
        {
            // Include existing class first
            if (attrs.TryGetValue("class", out var existing))
            {
                var existingStr = existing.ToString();

                if (!existingStr.IsNullOrWhiteSpace())
                    cls.Append(existingStr);
            }

            foreach (var c in classes)
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
    protected static void BuildClassAttribute(IDictionary<string, object> attrs, BuildClassAction builder)
    {
        var cls = new PooledStringBuilder(64);
        try
        {
            // Include existing class first
            if (attrs.TryGetValue("class", out var existing))
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
    protected static void BuildStyleAttribute(IDictionary<string, object> attrs, BuildStyleAction builder)
    {
        var sty = new PooledStringBuilder(64);
        try
        {
            // Include existing style first
            if (attrs.TryGetValue("style", out var existing))
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
    protected static void BuildClassAndStyleAttributes(IDictionary<string, object> attrs, BuildClassAndStyleAction builder)
    {
        var cls = new PooledStringBuilder(64);
        var sty = new PooledStringBuilder(64);
        try
        {
            // Include existing class first
            if (attrs.TryGetValue("class", out var existingClass))
            {
                var existingStr = existingClass.ToString();
                if (existingStr.HasContent())
                    cls.Append(existingStr);
            }

            // Include existing style first
            if (attrs.TryGetValue("style", out var existingStyle))
            {
                var existingStr = existingStyle.ToString();
                if (existingStr.HasContent())
                    sty.Append(existingStr);
            }
            
            builder(ref cls, ref sty); // Let the component add classes and styles
            
            if (cls.Length > 0)
                attrs["class"] = cls.ToString();
            if (sty.Length > 0)
                attrs["style"] = sty.ToString();
        }
        finally
        {
            cls.Dispose();
            sty.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void EnsureAttr<T>(IDictionary<string, object> attrs, string name, T value)
    {
        if (!attrs.ContainsKey(name)) attrs[name] = value!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void SetOrRemove(IDictionary<string, object> attrs, string name, bool condition, object trueValue)
    {
        if (condition) attrs[name] = trueValue;
        else attrs.Remove(name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendStyleDeclAttr(IDictionary<string, object> attrs, string fullDecl)
    {
        if (fullDecl.IsNullOrWhiteSpace()) return;
        attrs.TryGetValue("style", out var styleObj);
        var existing = styleObj?.ToString();

        if (existing.IsNullOrEmpty())
        {
            attrs["style"] = fullDecl;
            return;
        }

        if (existing!.EndsWith(';'))
            attrs["style"] = $"{existing} {fullDecl}";
        else
            attrs["style"] = $"{existing}; {fullDecl}";
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
    private static void AddColorCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<BackgroundColorBuilder>? v, string classPrefix,
        string cssProperty)
    {
        if (v is { IsEmpty: false })
        {
            var isTheme = v.Value.TryGetBootstrapThemeToken(out var token);

            if (isTheme && token is not null)
            {
                AppendClass(ref clsB, $"{classPrefix}-{token}");
            }
            else
            {
                var result = v.Value.ToString();
                if (!result.HasContent()) return;

                if (v.Value.IsCssStyle)
                    AppendStyleDecl(ref styB, $"{cssProperty}: {result}");
                else
                    AppendClass(ref clsB, $"{classPrefix}-{result}");
            }
        }
    }
}

/// <summary>
/// Delegate for building class attributes with a ref PooledStringBuilder
/// </summary>
public delegate void BuildClassAction(ref PooledStringBuilder builder);

/// <summary>
/// Delegate for building style attributes with a ref PooledStringBuilder  
/// </summary>
public delegate void BuildStyleAction(ref PooledStringBuilder builder);

/// <summary>
/// Delegate for building both class and style attributes with ref PooledStringBuilders
/// </summary>
public delegate void BuildClassAndStyleAction(ref PooledStringBuilder classBuilder, ref PooledStringBuilder styleBuilder);