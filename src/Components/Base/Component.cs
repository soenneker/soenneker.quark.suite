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

///<inheritdoc cref="IComponent"/>
public abstract class Component : CoreComponent, IComponent
{
    [Inject]
    protected IThemeProvider? ThemeProvider { get; set; }

    [Inject]
    protected ILogger<Component> Logger { get; set; } = null!;

    [Inject]
    protected QuarkOptions QuarkOptions { get; set; } = null!;

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public bool Hidden { get; set; }

    [Parameter]
    public CssValue<DisplayBuilder>? Display { get; set; }

    [Parameter]
    public CssValue<VisibilityBuilder>? Visibility { get; set; }

    [Parameter]
    public CssValue<FloatBuilder>? Float { get; set; }

    [Parameter]
    public CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }

    [Parameter]
    public CssValue<MarginBuilder>? Margin { get; set; }

    [Parameter]
    public CssValue<PaddingBuilder>? Padding { get; set; }

    [Parameter]
    public CssValue<PositionBuilder>? Position { get; set; }

    [Parameter]
    public CssValue<PositionOffsetBuilder>? Offset { get; set; }

    [Parameter]
    public CssValue<WidthBuilder>? Width { get; set; }

    [Parameter]
    public CssValue<WidthBuilder>? MinWidth { get; set; }

    [Parameter]
    public CssValue<WidthBuilder>? MaxWidth { get; set; }

    [Parameter]
    public CssValue<HeightBuilder>? Height { get; set; }

    [Parameter]
    public CssValue<HeightBuilder>? MinHeight { get; set; }

    [Parameter]
    public CssValue<HeightBuilder>? MaxHeight { get; set; }

    [Parameter]
    public CssValue<OverflowBuilder>? Overflow { get; set; }

    [Parameter]
    public CssValue<OverflowBuilder>? OverflowX { get; set; }

    [Parameter]
    public CssValue<OverflowBuilder>? OverflowY { get; set; }

    [Parameter]
    public CssValue<FlexBuilder>? Flex { get; set; }

    [Parameter]
    public CssValue<GapBuilder>? Gap { get; set; }

    [Parameter]
    public CssValue<OpacityBuilder>? Opacity { get; set; }

    [Parameter]
    public CssValue<ZIndexBuilder>? ZIndex { get; set; }

    [Parameter]
    public CssValue<PointerEventsBuilder>? PointerEvents { get; set; }

    [Parameter]
    public CssValue<UserSelectBuilder>? UserSelect { get; set; }

    [Parameter]
    public CssValue<BackgroundColorBuilder>? BackgroundColor { get; set; }

    [Parameter]
    public CssValue<BorderBuilder>? Border { get; set; }

    [Parameter]
    public CssValue<BorderColorBuilder>? BorderColor { get; set; }

    [Parameter]
    public CssValue<BorderRadiusBuilder>? BorderRadius { get; set; }

    [Parameter]
    public CssValue<TextAlignmentBuilder>? TextAlignment { get; set; }

    [Parameter]
    public CssValue<AnimationBuilder>? Animation { get; set; }

    [Parameter]
    public CssValue<BackdropFilterBuilder>? BackdropFilter { get; set; }

    [Parameter]
    public CssValue<ClearfixBuilder>? Clearfix { get; set; }

    [Parameter]
    public CssValue<ClipPathBuilder>? ClipPath { get; set; }

    [Parameter]
    public CssValue<FilterBuilder>? Filter { get; set; }

    [Parameter]
    public CssValue<RatioBuilder>? Ratio { get; set; }

    [Parameter]
    public CssValue<ResizeBuilder>? Resize { get; set; }

    [Parameter]
    public CssValue<ScreenReaderBuilder>? ScreenReader { get; set; }

    [Parameter]
    public CssValue<ScrollBehaviorBuilder>? ScrollBehavior { get; set; }

    [Parameter]
    public CssValue<StretchedLinkBuilder>? StretchedLink { get; set; }

    [Parameter]
    public CssValue<TransformBuilder>? Transform { get; set; }

    [Parameter]
    public CssValue<TransitionBuilder>? Transition { get; set; }

    [Parameter]
    public CssValue<LinkOpacityBuilder>? LinkOpacity { get; set; }

    [Parameter]
    public CssValue<LinkOffsetBuilder>? LinkOffset { get; set; }

    [Parameter]
    public CssValue<LinkUnderlineBuilder>? LinkUnderline { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter]
    public EventCallback<ElementReference> OnElementRefReady { get; set; }

    [Parameter]
    public virtual string? ThemeKey { get; set; }

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
        // Let theme push defaults before computing the render key (so the key reflects the actual values)
        ApplyThemeStyles();

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
    protected virtual Dictionary<string, object> BuildAttributes()
    {
        // Use cached attributes if render key hasn't changed
        if (!QuarkOptions.AlwaysRender && _cachedAttrs != null && _cachedAttrsKey == _lastRenderKey)
        {
            // Return a copy to prevent derived classes from mutating the cache
            return new Dictionary<string, object>(_cachedAttrs);
        }

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
            AddCss(ref sty, ref cls, Flex);
            AddCss(ref sty, ref cls, Gap);
            AddCss(ref sty, ref cls, VerticalAlign);
            AddCss(ref sty, ref cls, Float);
            AddCss(ref sty, ref cls, Visibility);
            AddCss(ref sty, ref cls, Opacity);
            AddCss(ref sty, ref cls, ZIndex);
            AddCss(ref sty, ref cls, PointerEvents);
            AddCss(ref sty, ref cls, UserSelect);
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
            AddCss(ref sty, ref cls, Offset);
            AddCss(ref sty, ref cls, Width);
            AddCss(ref sty, ref cls, MinWidth);
            AddCss(ref sty, ref cls, MaxWidth);
            AddCss(ref sty, ref cls, Height);
            AddCss(ref sty, ref cls, MinHeight);
            AddCss(ref sty, ref cls, MaxHeight);
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

            // Cache the computed attributes keyed by the render key
            _cachedAttrs = attrs;
            _cachedAttrsKey = _lastRenderKey;

            return attrs;
        }
        finally
        {
            sty.Dispose();
            cls.Dispose();
        }
    }

    // ---------- Render key computation ----------
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AddIf<T>(ref HashCode hc, CssValue<T>? v) where T : class, ICssBuilder
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
        AddIf(ref hc, Margin);
        AddIf(ref hc, Padding);
        AddIf(ref hc, Position);
        AddIf(ref hc, BackgroundColor);
        AddIf(ref hc, Border);
        AddIf(ref hc, BorderColor);
        AddIf(ref hc, BorderRadius);
        AddIf(ref hc, Offset);
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

        return hc.ToHashCode();
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

    // === Attribute helpers (same surface as your original) ===
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static string EnsureClass(string? existing, string? toAdd)
    {
        if (toAdd.IsNullOrEmpty()) 
            return existing ?? string.Empty;

        if (existing.IsNullOrEmpty()) 
            return toAdd;

        return existing.Contains(toAdd!, StringComparison.Ordinal) ? existing : $"{existing} {toAdd}";
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

    // ---------- Theme plumbing ----------
    private void ApplyThemeStyles()
    {
        if (ThemeProvider?.Themes == null) return;

        var themeName = ThemeProvider.CurrentTheme;
        if (themeName.IsNullOrEmpty()) return;

        if (ThemeProvider.Themes.TryGetValue(themeName, out var theme))
            ApplyThemeToComponent(theme);
    }

    private void ApplyThemeToComponent(Theme theme)
    {
        var componentOptions = GetComponentOptionsFromTheme(theme);

        if (componentOptions == null)
            return;

        ApplyThemeProperty(componentOptions.Display, () => Display, v => Display = v);
        ApplyThemeProperty(componentOptions.Visibility, () => Visibility, v => Visibility = v);
        ApplyThemeProperty(componentOptions.BackgroundColor, () => BackgroundColor, v => BackgroundColor = v);
        ApplyThemeProperty(componentOptions.Border, () => Border, v => Border = v);
        ApplyThemeProperty(componentOptions.BorderColor, () => BorderColor, v => BorderColor = v);
        ApplyThemeProperty(componentOptions.BorderRadius, () => BorderRadius, v => BorderRadius = v);
        ApplyThemeProperty(componentOptions.TextAlignment, () => TextAlignment, v => TextAlignment = v);
        ApplyThemeProperty(componentOptions.Float, () => Float, v => Float = v);
        ApplyThemeProperty(componentOptions.VerticalAlign, () => VerticalAlign, v => VerticalAlign = v);
        ApplyThemeProperty(componentOptions.Margin, () => Margin, v => Margin = v);
        ApplyThemeProperty(componentOptions.Padding, () => Padding, v => Padding = v);
        ApplyThemeProperty(componentOptions.Position, () => Position, v => Position = v);
        ApplyThemeProperty(componentOptions.Offset, () => Offset, v => Offset = v);
        ApplyThemeProperty(componentOptions.Width, () => Width, v => Width = v);
        ApplyThemeProperty(componentOptions.MinWidth, () => MinWidth, v => MinWidth = v);
        ApplyThemeProperty(componentOptions.MaxWidth, () => MaxWidth, v => MaxWidth = v);
        ApplyThemeProperty(componentOptions.Height, () => Height, v => Height = v);
        ApplyThemeProperty(componentOptions.MinHeight, () => MinHeight, v => MinHeight = v);
        ApplyThemeProperty(componentOptions.MaxHeight, () => MaxHeight, v => MaxHeight = v);
        ApplyThemeProperty(componentOptions.Overflow, () => Overflow, v => Overflow = v);
        ApplyThemeProperty(componentOptions.OverflowX, () => OverflowX, v => OverflowX = v);
        ApplyThemeProperty(componentOptions.OverflowY, () => OverflowY, v => OverflowY = v);
        ApplyThemeProperty(componentOptions.Flex, () => Flex, v => Flex = v);
        ApplyThemeProperty(componentOptions.Gap, () => Gap, v => Gap = v);
        ApplyThemeProperty(componentOptions.Opacity, () => Opacity, v => Opacity = v);
        ApplyThemeProperty(componentOptions.ZIndex, () => ZIndex, v => ZIndex = v);
        ApplyThemeProperty(componentOptions.PointerEvents, () => PointerEvents, v => PointerEvents = v);
        ApplyThemeProperty(componentOptions.UserSelect, () => UserSelect, v => UserSelect = v);
        ApplyThemeProperty(componentOptions.Animation, () => Animation, v => Animation = v);
        ApplyThemeProperty(componentOptions.BackdropFilter, () => BackdropFilter, v => BackdropFilter = v);
        ApplyThemeProperty(componentOptions.Clearfix, () => Clearfix, v => Clearfix = v);
        ApplyThemeProperty(componentOptions.ClipPath, () => ClipPath, v => ClipPath = v);
        ApplyThemeProperty(componentOptions.Filter, () => Filter, v => Filter = v);
        ApplyThemeProperty(componentOptions.Resize, () => Resize, v => Resize = v);
        ApplyThemeProperty(componentOptions.ScreenReader, () => ScreenReader, v => ScreenReader = v);
        ApplyThemeProperty(componentOptions.ScrollBehavior, () => ScrollBehavior, v => ScrollBehavior = v);
        ApplyThemeProperty(componentOptions.StretchedLink, () => StretchedLink, v => StretchedLink = v);
        ApplyThemeProperty(componentOptions.Transform, () => Transform, v => Transform = v);
        ApplyThemeProperty(componentOptions.Transition, () => Transition, v => Transition = v);
        ApplyThemeProperty(componentOptions.LinkOpacity, () => LinkOpacity, v => LinkOpacity = v);
        ApplyThemeProperty(componentOptions.LinkOffset, () => LinkOffset, v => LinkOffset = v);
        ApplyThemeProperty(componentOptions.LinkUnderline, () => LinkUnderline, v => LinkUnderline = v);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ApplyThemeProperty<T>(T? themeValue, Func<T?> getCurrentValue, Action<T> setValue) where T : struct
    {
        if (themeValue.HasValue && !getCurrentValue().HasValue)
            setValue(themeValue.Value);
    }

    private ComponentOptions? GetComponentOptionsFromTheme(Theme theme)
    {
        if (ThemeProvider == null)
        {
            Logger?.LogWarning("ThemeProvider is null; cannot apply theme styles.");
            return null;
        }

        if (ThemeKey == null)
        {
            Logger?.LogWarning("ThemeKey is null for {type}; cannot apply theme styles.", GetType()
                .FullName);
            return null;
        }

        return ThemeProvider.ComponentOptions.TryGetValue(ThemeKey, out var getter) ? getter(theme) : null;
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
            string? token = null;
            var isTheme = v.Value.TryGetBootstrapThemeToken(out token);

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