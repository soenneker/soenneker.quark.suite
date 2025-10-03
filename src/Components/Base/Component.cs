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
    [Inject]
    protected IThemeProvider? ThemeProvider { get; set; }

    [Inject]
    protected ILogger<Component> Logger { get; set; } = null!;

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public int? TabIndex { get; set; }

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
    public CssValue<TextOverflowBuilder>? TextOverflow { get; set; }

    [Parameter]
    public CssValue<BoxShadowBuilder>? BoxShadow { get; set; }

    [Parameter]
    public CssValue<MarginBuilder>? Margin { get; set; }

    [Parameter]
    public CssValue<PaddingBuilder>? Padding { get; set; }

    [Parameter]
    public CssValue<PositionBuilder>? Position { get; set; }

    [Parameter]
    public CssValue<PositionOffsetBuilder>? Offset { get; set; }

    [Parameter]
    public CssValue<TextSizeBuilder>? TextSize { get; set; }

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
    public CssValue<ObjectFitBuilder>? ObjectFit { get; set; }

    [Parameter]
    public CssValue<TextAlignmentBuilder>? TextAlignment { get; set; }

    [Parameter]
    public CssValue<TextDecorationBuilder>? TextDecorationLine { get; set; }

    [Parameter]
    public CssValue<TextDecorationBuilder>? TextDecorationCss { get; set; }

    [Parameter]
    public CssValue<FlexBuilder>? Flex { get; set; }

    [Parameter]
    public CssValue<GapBuilder>? Gap { get; set; }

    [Parameter]
    public CssValue<BorderBuilder>? Border { get; set; }

    [Parameter]
    public CssValue<OpacityBuilder>? Opacity { get; set; }

    [Parameter]
    public CssValue<ZIndexBuilder>? ZIndex { get; set; }

    [Parameter]
    public CssValue<PointerEventsBuilder>? PointerEvents { get; set; }

    [Parameter]
    public CssValue<UserSelectBuilder>? UserSelect { get; set; }

    [Parameter]
    public CssValue<TextTransformBuilder>? TextTransform { get; set; }

    [Parameter]
    public CssValue<FontWeightBuilder>? FontWeight { get; set; }

    [Parameter]
    public CssValue<FontStyleBuilder>? FontStyle { get; set; }

    [Parameter]
    public CssValue<LineHeightBuilder>? LineHeight { get; set; }

    [Parameter]
    public CssValue<TextWrapBuilder>? TextWrap { get; set; }

    [Parameter]
    public CssValue<TextBreakBuilder>? TextBreak { get; set; }

    [Parameter]
    public CssValue<ColorBuilder>? TextColor { get; set; }

    [Parameter]
    public CssValue<ColorBuilder>? BackgroundColor { get; set; }

    [Parameter]
    public CssValue<ColorBuilder>? TextBackgroundColor { get; set; }

    [Parameter]
    public CssValue<AnimationBuilder>? Animation { get; set; }

    [Parameter]
    public CssValue<AspectRatioBuilder>? AspectRatio { get; set; }

    [Parameter]
    public CssValue<BackdropFilterBuilder>? BackdropFilter { get; set; }

    [Parameter]
    public CssValue<BorderRadiusBuilder>? BorderRadius { get; set; }

    [Parameter]
    public CssValue<ClearfixBuilder>? Clearfix { get; set; }

    [Parameter]
    public CssValue<ClipPathBuilder>? ClipPath { get; set; }

    [Parameter]
    public CssValue<CursorBuilder>? Cursor { get; set; }

    [Parameter]
    public CssValue<FilterBuilder>? Filter { get; set; }

    [Parameter]
    public CssValue<InteractionBuilder>? Interaction { get; set; }

    [Parameter]
    public CssValue<ObjectPositionBuilder>? ObjectPosition { get; set; }

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
    public CssValue<TruncateBuilder>? Truncate { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnDoubleClick { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnMouseOver { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnMouseOut { get; set; }

    [Parameter]
    public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

    [Parameter]
    public EventCallback<FocusEventArgs> OnFocus { get; set; }

    [Parameter]
    public EventCallback<FocusEventArgs> OnBlur { get; set; }

    [Parameter]
    public EventCallback<ElementReference> OnElementRefReady { get; set; }

    [Parameter]
    public string? Role { get; set; }

    [Parameter]
    public string? AriaLabel { get; set; }

    [Parameter]
    public string? AriaDescribedBy { get; set; }

    [Parameter]
    public virtual string? ThemeKey { get; set; }

    protected ElementReference ElementRef { get; set; }

    // -------- Render gate + attribute cache --------
    private bool _shouldRender = true;
    private int _lastRenderKey;
    private Dictionary<string, object>? _cachedAttrs;
    private int _cachedAttrsKey;

    protected override bool ShouldRender() => true;

    protected override void OnParametersSet()
    {
        // Let theme push defaults before computing the render key (so the key reflects the actual values)
        ApplyThemeStyles();

        var key = ComputeRenderKey();
        _shouldRender = key != _lastRenderKey;
        _lastRenderKey = key;
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _ = OnElementRefReady.InvokeIfHasDelegate(ElementRef);
        }

        return Task.CompletedTask;
    }

    // ---- Event handlers: default to pass-through (no visual state change) ----
    protected virtual Task HandleClick(MouseEventArgs e)
    {
        _shouldRender = false;
        return OnClick.InvokeIfHasDelegate(e);
    }

    protected virtual Task HandleDoubleClick(MouseEventArgs e)
    {
        _shouldRender = false;
        return OnDoubleClick.InvokeIfHasDelegate(e);
    }

    protected virtual Task HandleMouseOver(MouseEventArgs e)
    {
        _shouldRender = false;
        return OnMouseOver.InvokeIfHasDelegate(e);
    }

    protected virtual Task HandleMouseOut(MouseEventArgs e)
    {
        _shouldRender = false;
        return OnMouseOut.InvokeIfHasDelegate(e);
    }

    protected virtual Task HandleKeyDown(KeyboardEventArgs e)
    {
        _shouldRender = false;
        return OnKeyDown.InvokeIfHasDelegate(e);
    }

    protected virtual Task HandleFocus(FocusEventArgs e)
    {
        _shouldRender = false;
        return OnFocus.InvokeIfHasDelegate(e);
    }

    protected virtual Task HandleBlur(FocusEventArgs e)
    {
        _shouldRender = false;
        return OnBlur.InvokeIfHasDelegate(e);
    }

    // -------- Attributes building (cached by render key) --------
    protected virtual Dictionary<string, object> BuildAttributes()
    {
        // Always rebuild attributes to reflect dynamic state changes reliably

        var guess = 14 + (Attributes?.Count ?? 0);
        var attrs = new Dictionary<string, object>(guess);

        var cls = new PooledStringBuilder(64);
        var sty = new PooledStringBuilder(128);

        bool userOnClick = false,
            userOnDblClick = false,
            userOnMouseOver = false,
            userOnMouseOut = false,
            userOnKeyDown = false,
            userOnFocus = false,
            userOnBlur = false;

        try
        {
            if (Class.HasContent()) cls.Append(Class!);
            if (Style.HasContent()) sty.Append(Style!);

            if (Id.HasContent()) attrs["id"] = Id!;
            if (Title.HasContent()) attrs["title"] = Title!;
            if (TabIndex.HasValue) attrs["tabindex"] = TabIndex.Value;
            if (Hidden) attrs["hidden"] = true;
            if (Role.HasContent()) attrs["role"] = Role!;
            if (AriaLabel.HasContent()) attrs["aria-label"] = AriaLabel!;
            if (AriaDescribedBy.HasContent()) attrs["aria-describedby"] = AriaDescribedBy!;

            AddCss(ref sty, ref cls, Display);
            AddColorCss(ref sty, ref cls, TextColor, "text", "color");
            AddColorCss(ref sty, ref cls, BackgroundColor, "bg", "background-color");
            AddColorCss(ref sty, ref cls, TextBackgroundColor, "text-bg", "background-color");
            AddCss(ref sty, ref cls, Flex);
            AddCss(ref sty, ref cls, Gap);
            AddCss(ref sty, ref cls, Border);
            AddCss(ref sty, ref cls, TextOverflow);
            AddCss(ref sty, ref cls, TextAlignment);
            AddCss(ref sty, ref cls, TextDecorationCss);
            AddCss(ref sty, ref cls, VerticalAlign);
            AddCss(ref sty, ref cls, Float);
            AddCss(ref sty, ref cls, Visibility);
            AddCss(ref sty, ref cls, BoxShadow);
            AddCss(ref sty, ref cls, Opacity);
            AddCss(ref sty, ref cls, ZIndex);
            AddCss(ref sty, ref cls, PointerEvents);
            AddCss(ref sty, ref cls, UserSelect);
            AddCss(ref sty, ref cls, TextTransform);
            AddCss(ref sty, ref cls, FontWeight);
            AddCss(ref sty, ref cls, FontStyle);
            AddCss(ref sty, ref cls, LineHeight);
            AddCss(ref sty, ref cls, TextWrap);
            AddCss(ref sty, ref cls, TextBreak);
            AddCss(ref sty, ref cls, Animation);
            AddCss(ref sty, ref cls, AspectRatio);
            AddCss(ref sty, ref cls, BackdropFilter);
            AddCss(ref sty, ref cls, BorderRadius);
            AddCss(ref sty, ref cls, Clearfix);
            AddCss(ref sty, ref cls, ClipPath);
            AddCss(ref sty, ref cls, Cursor);
            AddCss(ref sty, ref cls, Filter);
            AddCss(ref sty, ref cls, Interaction);
            AddCss(ref sty, ref cls, ObjectPosition);
            AddCss(ref sty, ref cls, Resize);
            AddCss(ref sty, ref cls, ScreenReader);
            AddCss(ref sty, ref cls, ScrollBehavior);
            AddCss(ref sty, ref cls, StretchedLink);
            AddCss(ref sty, ref cls, Transform);
            AddCss(ref sty, ref cls, Transition);
            AddCss(ref sty, ref cls, Truncate);

            AddCss(ref sty, ref cls, Margin);
            AddCss(ref sty, ref cls, Padding);
            AddCss(ref sty, ref cls, Position);
            AddCss(ref sty, ref cls, Offset);
            AddCss(ref sty, ref cls, TextSize);
            AddCss(ref sty, ref cls, Width);
            AddCss(ref sty, ref cls, MinWidth);
            AddCss(ref sty, ref cls, MaxWidth);
            AddCss(ref sty, ref cls, Height);
            AddCss(ref sty, ref cls, MinHeight);
            AddCss(ref sty, ref cls, MaxHeight);
            AddCss(ref sty, ref cls, Overflow);
            AddCss(ref sty, ref cls, OverflowX);
            AddCss(ref sty, ref cls, OverflowY);
            AddCss(ref sty, ref cls, ObjectFit);

            if (Attributes is not null)
            {
                foreach (var kv in Attributes)
                {
                    var keyLower = kv.Key?.ToLowerInvariant() ?? string.Empty;

                    switch (keyLower)
                    {
                        case "class":
                            AppendClass(ref cls, kv.Value?.ToString() ?? string.Empty);
                            break;

                        case "style":
                            AppendStyleDecl(ref sty, kv.Value?.ToString() ?? string.Empty);
                            break;

                        case "onclick":
                            userOnClick = true;
                            attrs["onclick"] = OnClick.HasDelegate ? OnClick : kv.Value!;
                            break;

                        case "ondblclick":
                            userOnDblClick = true;
                            attrs["ondblclick"] = OnDoubleClick.HasDelegate ? OnDoubleClick : kv.Value!;
                            break;

                        case "onmouseover":
                            userOnMouseOver = true;
                            attrs["onmouseover"] = OnMouseOver.HasDelegate ? OnMouseOver : kv.Value!;
                            break;

                        case "onmouseout":
                            userOnMouseOut = true;
                            attrs["onmouseout"] = OnMouseOut.HasDelegate ? OnMouseOut : kv.Value!;
                            break;

                        case "onkeydown":
                            userOnKeyDown = true;
                            attrs["onkeydown"] = OnKeyDown.HasDelegate ? OnKeyDown : kv.Value!;
                            break;

                        case "onfocus":
                            userOnFocus = true;
                            attrs["onfocus"] = OnFocus.HasDelegate ? OnFocus : kv.Value!;
                            break;

                        case "onblur":
                            userOnBlur = true;
                            attrs["onblur"] = OnBlur.HasDelegate ? OnBlur : kv.Value!;
                            break;

                        default:
                            attrs[kv.Key!] = kv.Value!;
                            break;
                    }
                }
            }

            if (cls.Length > 0) attrs["class"] = cls.ToString();
            if (sty.Length > 0) attrs["style"] = sty.ToString();

            if (!userOnClick && OnClick.HasDelegate && !attrs.ContainsKey("onclick")) attrs["onclick"] = OnClick;
            if (!userOnDblClick && OnDoubleClick.HasDelegate) attrs["ondblclick"] = OnDoubleClick;
            if (!userOnMouseOver && OnMouseOver.HasDelegate) attrs["onmouseover"] = OnMouseOver;
            if (!userOnMouseOut && OnMouseOut.HasDelegate) attrs["onmouseout"] = OnMouseOut;
            if (!userOnKeyDown && OnKeyDown.HasDelegate) attrs["onkeydown"] = OnKeyDown;
            if (!userOnFocus && OnFocus.HasDelegate) attrs["onfocus"] = OnFocus;
            if (!userOnBlur && OnBlur.HasDelegate) attrs["onblur"] = OnBlur;

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
        if (v.HasValue && !v.Value.IsEmpty)
            hc.Add(v.Value); // struct add; no boxing
    }

    private int ComputeRenderKey()
    {
        var hc = new HashCode();

        hc.Add(Class);
        hc.Add(Style);
        hc.Add(Title);
        hc.Add(TabIndex);
        hc.Add(Hidden);
        hc.Add(Role);
        hc.Add(AriaLabel);
        hc.Add(AriaDescribedBy);

        AddIf(ref hc, Display);
        AddIf(ref hc, Visibility);
        AddIf(ref hc, Float);
        AddIf(ref hc, VerticalAlign);
        AddIf(ref hc, TextOverflow);
        AddIf(ref hc, BoxShadow);
        AddIf(ref hc, Margin);
        AddIf(ref hc, Padding);
        AddIf(ref hc, Position);
        AddIf(ref hc, Offset);
        AddIf(ref hc, TextSize);
        AddIf(ref hc, Width);
        AddIf(ref hc, MinWidth);
        AddIf(ref hc, MaxWidth);
        AddIf(ref hc, Height);
        AddIf(ref hc, MinHeight);
        AddIf(ref hc, MaxHeight);
        AddIf(ref hc, Overflow);
        AddIf(ref hc, OverflowX);
        AddIf(ref hc, OverflowY);
        AddIf(ref hc, ObjectFit);
        AddIf(ref hc, TextAlignment);
        AddIf(ref hc, TextDecorationLine);
        AddIf(ref hc, TextDecorationCss);
        AddIf(ref hc, Flex);
        AddIf(ref hc, Gap);
        AddIf(ref hc, Border);
        AddIf(ref hc, Opacity);
        AddIf(ref hc, ZIndex);
        AddIf(ref hc, PointerEvents);
        AddIf(ref hc, UserSelect);
        AddIf(ref hc, TextTransform);
        AddIf(ref hc, FontWeight);
        AddIf(ref hc, FontStyle);
        AddIf(ref hc, LineHeight);
        AddIf(ref hc, TextWrap);
        AddIf(ref hc, TextBreak);
        AddIf(ref hc, Animation);
        AddIf(ref hc, AspectRatio);
        AddIf(ref hc, BackdropFilter);
        AddIf(ref hc, BorderRadius);
        AddIf(ref hc, Clearfix);
        AddIf(ref hc, ClipPath);
        AddIf(ref hc, Cursor);
        AddIf(ref hc, Filter);
        AddIf(ref hc, Interaction);
        AddIf(ref hc, ObjectPosition);
        AddIf(ref hc, Resize);
        AddIf(ref hc, ScreenReader);
        AddIf(ref hc, ScrollBehavior);
        AddIf(ref hc, StretchedLink);
        AddIf(ref hc, Transform);
        AddIf(ref hc, Transition);
        AddIf(ref hc, Truncate);
        AddIf(ref hc, TextColor);
        AddIf(ref hc, BackgroundColor);
        AddIf(ref hc, TextBackgroundColor);

        if (Attributes is not null)
        {
            if (Attributes.TryGetValue("class", out var cls)) hc.Add(cls?.ToString());
            if (Attributes.TryGetValue("style", out var sty)) hc.Add(sty?.ToString());
            if (Attributes.TryGetValue("id", out var id)) hc.Add(id?.ToString());
            if (Attributes.TryGetValue("role", out var role)) hc.Add(role?.ToString());
            if (Attributes.TryGetValue("aria-label", out var al)) hc.Add(al?.ToString());
            if (Attributes.TryGetValue("aria-describedby", out var ad)) hc.Add(ad?.ToString());
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
    private static void AppendClass(ref PooledStringBuilder b, string s)
    {
        if (s.IsNullOrEmpty()) return;
        if (b.Length != 0) b.Append(' ');
        b.Append(s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AppendStyleDecl(ref PooledStringBuilder b, string nameColonSpace, object value)
    {
        if (b.Length != 0)
        {
            b.Append(';');
            b.Append(' ');
        }

        b.Append(nameColonSpace);
        b.Append(value.ToString()!);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AppendStyleDecl(ref PooledStringBuilder b, string fullDecl)
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
    private static void AddCss<T>(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<T>? v) where T : class, ICssBuilder
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
    private static void AddColorCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<ColorBuilder>? v, string classPrefix,
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

    // === Attribute helpers (same surface as your original) ===
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static string EnsureClass(string? existing, string? toAdd)
    {
        if (toAdd.IsNullOrEmpty()) return existing ?? string.Empty;
        if (existing.IsNullOrEmpty()) return toAdd!;
        return existing.Contains(toAdd!, StringComparison.Ordinal) ? existing : $"{existing} {toAdd}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static string AppendToClass(string? existing, string toAdd)
    {
        if (toAdd.IsNullOrEmpty()) return existing ?? string.Empty;
        if (existing.IsNullOrEmpty()) return toAdd;
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
        if (cls.Length > 0) attrs["class"] = cls;
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
        if (componentOptions == null) return;

        ApplyThemeProperty(componentOptions.Display, () => Display, v => Display = v);
        ApplyThemeProperty(componentOptions.Visibility, () => Visibility, v => Visibility = v);
        ApplyThemeProperty(componentOptions.Float, () => Float, v => Float = v);
        ApplyThemeProperty(componentOptions.VerticalAlign, () => VerticalAlign, v => VerticalAlign = v);
        ApplyThemeProperty(componentOptions.TextOverflow, () => TextOverflow, v => TextOverflow = v);
        ApplyThemeProperty(componentOptions.BoxShadow, () => BoxShadow, v => BoxShadow = v);
        ApplyThemeProperty(componentOptions.Margin, () => Margin, v => Margin = v);
        ApplyThemeProperty(componentOptions.Padding, () => Padding, v => Padding = v);
        ApplyThemeProperty(componentOptions.Position, () => Position, v => Position = v);
        ApplyThemeProperty(componentOptions.Offset, () => Offset, v => Offset = v);
        ApplyThemeProperty(componentOptions.TextSize, () => TextSize, v => TextSize = v);
        ApplyThemeProperty(componentOptions.Width, () => Width, v => Width = v);
        ApplyThemeProperty(componentOptions.MinWidth, () => MinWidth, v => MinWidth = v);
        ApplyThemeProperty(componentOptions.MaxWidth, () => MaxWidth, v => MaxWidth = v);
        ApplyThemeProperty(componentOptions.Height, () => Height, v => Height = v);
        ApplyThemeProperty(componentOptions.MinHeight, () => MinHeight, v => MinHeight = v);
        ApplyThemeProperty(componentOptions.MaxHeight, () => MaxHeight, v => MaxHeight = v);
        ApplyThemeProperty(componentOptions.Overflow, () => Overflow, v => Overflow = v);
        ApplyThemeProperty(componentOptions.OverflowX, () => OverflowX, v => OverflowX = v);
        ApplyThemeProperty(componentOptions.OverflowY, () => OverflowY, v => OverflowY = v);
        ApplyThemeProperty(componentOptions.ObjectFit, () => ObjectFit, v => ObjectFit = v);
        ApplyThemeProperty(componentOptions.TextAlignment, () => TextAlignment, v => TextAlignment = v);
        ApplyThemeProperty(componentOptions.TextDecorationLine, () => TextDecorationLine, v => TextDecorationLine = v);
        ApplyThemeProperty(componentOptions.TextDecorationCss, () => TextDecorationCss, v => TextDecorationCss = v);
        ApplyThemeProperty(componentOptions.Flex, () => Flex, v => Flex = v);
        ApplyThemeProperty(componentOptions.Gap, () => Gap, v => Gap = v);
        ApplyThemeProperty(componentOptions.Border, () => Border, v => Border = v);
        ApplyThemeProperty(componentOptions.Opacity, () => Opacity, v => Opacity = v);
        ApplyThemeProperty(componentOptions.ZIndex, () => ZIndex, v => ZIndex = v);
        ApplyThemeProperty(componentOptions.PointerEvents, () => PointerEvents, v => PointerEvents = v);
        ApplyThemeProperty(componentOptions.UserSelect, () => UserSelect, v => UserSelect = v);
        ApplyThemeProperty(componentOptions.TextTransform, () => TextTransform, v => TextTransform = v);
        ApplyThemeProperty(componentOptions.FontWeight, () => FontWeight, v => FontWeight = v);
        ApplyThemeProperty(componentOptions.FontStyle, () => FontStyle, v => FontStyle = v);
        ApplyThemeProperty(componentOptions.LineHeight, () => LineHeight, v => LineHeight = v);
        ApplyThemeProperty(componentOptions.TextWrap, () => TextWrap, v => TextWrap = v);
        ApplyThemeProperty(componentOptions.TextBreak, () => TextBreak, v => TextBreak = v);
        ApplyThemeProperty(componentOptions.TextColor, () => TextColor, v => TextColor = v);
        ApplyThemeProperty(componentOptions.BackgroundColor, () => BackgroundColor, v => BackgroundColor = v);
        ApplyThemeProperty(componentOptions.TextBackgroundColor, () => TextBackgroundColor, v => TextBackgroundColor = v);
        ApplyThemeProperty(componentOptions.Animation, () => Animation, v => Animation = v);
        ApplyThemeProperty(componentOptions.AspectRatio, () => AspectRatio, v => AspectRatio = v);
        ApplyThemeProperty(componentOptions.BackdropFilter, () => BackdropFilter, v => BackdropFilter = v);
        ApplyThemeProperty(componentOptions.BorderRadius, () => BorderRadius, v => BorderRadius = v);
        ApplyThemeProperty(componentOptions.Clearfix, () => Clearfix, v => Clearfix = v);
        ApplyThemeProperty(componentOptions.ClipPath, () => ClipPath, v => ClipPath = v);
        ApplyThemeProperty(componentOptions.Cursor, () => Cursor, v => Cursor = v);
        ApplyThemeProperty(componentOptions.Filter, () => Filter, v => Filter = v);
        ApplyThemeProperty(componentOptions.Interaction, () => Interaction, v => Interaction = v);
        ApplyThemeProperty(componentOptions.ObjectPosition, () => ObjectPosition, v => ObjectPosition = v);
        ApplyThemeProperty(componentOptions.Resize, () => Resize, v => Resize = v);
        ApplyThemeProperty(componentOptions.ScreenReader, () => ScreenReader, v => ScreenReader = v);
        ApplyThemeProperty(componentOptions.ScrollBehavior, () => ScrollBehavior, v => ScrollBehavior = v);
        ApplyThemeProperty(componentOptions.StretchedLink, () => StretchedLink, v => StretchedLink = v);
        ApplyThemeProperty(componentOptions.Transform, () => Transform, v => Transform = v);
        ApplyThemeProperty(componentOptions.Transition, () => Transition, v => Transition = v);
        ApplyThemeProperty(componentOptions.Truncate, () => Truncate, v => Truncate = v);
    }

    private static void ApplyThemeProperty<T>(T? themeValue, Func<T?> getCurrentValue, Action<T> setValue) where T : struct
    {
        if (themeValue.HasValue && !getCurrentValue()
                .HasValue)
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
}