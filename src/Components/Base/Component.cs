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
    protected ILogger<Component>? Logger { get; set; }

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

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            return OnElementRefReady.InvokeIfHasDelegate(ElementRef);

        return Task.CompletedTask;
    }

    protected virtual Dictionary<string, object> BuildAttributes()
    {
        // Apply theme styles first (lowest precedence)
        ApplyThemeStyles();

        int guess = 14 + (Attributes?.Count ?? 0);
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
                foreach (KeyValuePair<string, object> kv in Attributes)
                {
                    string keyLower = kv.Key?.ToLowerInvariant() ?? string.Empty;

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
                            if (kv.Value is EventCallback<MouseEventArgs> userClick && OnClick.HasDelegate)
                                attrs["onclick"] = Compose(this, HandleClick, userClick);
                            else
                                attrs["onclick"] = kv.Value!;
                            break;

                        case "ondblclick":
                            userOnDblClick = true;
                            if (kv.Value is EventCallback<MouseEventArgs> userDbl && OnDoubleClick.HasDelegate)
                                attrs["ondblclick"] = Compose(this, HandleDoubleClick, userDbl);
                            else
                                attrs["ondblclick"] = kv.Value!;
                            break;

                        case "onmouseover":
                            userOnMouseOver = true;
                            if (kv.Value is EventCallback<MouseEventArgs> userOver && OnMouseOver.HasDelegate)
                                attrs["onmouseover"] = Compose(this, HandleMouseOver, userOver);
                            else
                                attrs["onmouseover"] = kv.Value!;
                            break;

                        case "onmouseout":
                            userOnMouseOut = true;
                            if (kv.Value is EventCallback<MouseEventArgs> userOut && OnMouseOut.HasDelegate)
                                attrs["onmouseout"] = Compose(this, HandleMouseOut, userOut);
                            else
                                attrs["onmouseout"] = kv.Value!;
                            break;

                        case "onkeydown":
                            userOnKeyDown = true;
                            if (kv.Value is EventCallback<KeyboardEventArgs> userKey && OnKeyDown.HasDelegate)
                                attrs["onkeydown"] = Compose(this, HandleKeyDown, userKey);
                            else
                                attrs["onkeydown"] = kv.Value!;
                            break;

                        case "onfocus":
                            userOnFocus = true;
                            if (kv.Value is EventCallback<FocusEventArgs> userFocus && OnFocus.HasDelegate)
                                attrs["onfocus"] = Compose(this, HandleFocus, userFocus);
                            else
                                attrs["onfocus"] = kv.Value!;
                            break;

                        case "onblur":
                            userOnBlur = true;
                            if (kv.Value is EventCallback<FocusEventArgs> userBlur && OnBlur.HasDelegate)
                                attrs["onblur"] = Compose(this, HandleBlur, userBlur);
                            else
                                attrs["onblur"] = kv.Value!;
                            break;

                        default:
                            attrs[kv.Key!] = kv.Value!;
                            break;
                    }
                }
            }

            if (cls.Length > 0) attrs["class"] = cls.ToString();
            if (sty.Length > 0) attrs["style"] = sty.ToString();

            if (!userOnClick && OnClick.HasDelegate) attrs["onclick"] = OnClick;
            if (!userOnDblClick && OnDoubleClick.HasDelegate) attrs["ondblclick"] = OnDoubleClick;
            if (!userOnMouseOver && OnMouseOver.HasDelegate) attrs["onmouseover"] = OnMouseOver;
            if (!userOnMouseOut && OnMouseOut.HasDelegate) attrs["onmouseout"] = OnMouseOut;
            if (!userOnKeyDown && OnKeyDown.HasDelegate) attrs["onkeydown"] = OnKeyDown;
            if (!userOnFocus && OnFocus.HasDelegate) attrs["onfocus"] = OnFocus;
            if (!userOnBlur && OnBlur.HasDelegate) attrs["onblur"] = OnBlur;

            return attrs;
        }
        finally
        {
            sty.Dispose();
            cls.Dispose();
        }
    }

    private static EventCallback<TArgs> Compose<TArgs>(ComponentBase owner, Func<TArgs, Task> ours, EventCallback<TArgs> users)
    {
        EventCallback<TArgs> usersCopy = users; // stabilize
        return EventCallback.Factory.Create<TArgs>(owner, async e =>
        {
            await ours(e);
            await usersCopy.InvokeAsync(e); // Since it's a user attribute we shouldn't check if it has a delegate, because that'd be unusual
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AppendClass(ref PooledStringBuilder b, string s)
    {
        if (s.IsNullOrEmpty())
            return;

        if (b.Length != 0)
            b.Append(' ');

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
        if (fullDecl.IsNullOrEmpty())
            return;

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

            if (s.Length == 0)
                return;

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
            var result = v.Value.ToString();

            if (!result.HasContent())
                return;

            if (v.Value.IsCssStyle)
                AppendStyleDecl(ref styB, $"{cssProperty}: {result}");
            else
                AppendClass(ref clsB, $"{classPrefix}-{result}");
        }
    }

    protected virtual Task HandleClick(MouseEventArgs e) => OnClick.InvokeIfHasDelegate(e);
    protected virtual Task HandleDoubleClick(MouseEventArgs e) => OnDoubleClick.InvokeIfHasDelegate(e);
    protected virtual Task HandleMouseOver(MouseEventArgs e) => OnMouseOver.InvokeIfHasDelegate(e);
    protected virtual Task HandleMouseOut(MouseEventArgs e) => OnMouseOut.InvokeIfHasDelegate(e);
    protected virtual Task HandleKeyDown(KeyboardEventArgs e) => OnKeyDown.InvokeIfHasDelegate(e);
    protected virtual Task HandleFocus(FocusEventArgs e) => OnFocus.InvokeIfHasDelegate(e);
    protected virtual Task HandleBlur(FocusEventArgs e) => OnBlur.InvokeIfHasDelegate(e);


    // === Attribute helpers ===
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static string EnsureClass(string? existing, string? toAdd)
    {
        if (toAdd.IsNullOrEmpty())
            return existing ?? string.Empty;

        if (existing.IsNullOrEmpty())
            return toAdd!;

        // Cheap dup guard, ordinal match (class tokens are ASCII-ish)
        return existing.Contains(toAdd!, StringComparison.Ordinal) ? existing : $"{existing} {toAdd}";
    }

    /// <summary>
    /// Append a class token without duplicate checks (use when you know it's unique).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static string AppendToClass(string? existing, string toAdd)
    {
        if (toAdd.IsNullOrEmpty())
            return existing ?? string.Empty;

        if (existing.IsNullOrEmpty())
            return toAdd;

        return $"{existing} {toAdd}";
    }

    /// <summary>
    /// Ensure a class token exists in the attributes["class"] value exactly once.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void EnsureClassAttr(IDictionary<string, object> attrs, string token)
    {
        attrs.TryGetValue("class", out object? clsObj);
        string cls = EnsureClass(clsObj?.ToString(), token);

        if (cls.Length > 0)
            attrs["class"] = cls;
    }

    /// <summary>
    /// Append a class token to attributes["class"] (no dup check).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendToClassAttr(IDictionary<string, object> attrs, string token)
    {
        attrs.TryGetValue("class", out object? clsObj);
        string cls = AppendToClass(clsObj?.ToString(), token);

        if (cls.Length > 0)
            attrs["class"] = cls;
    }

    /// <summary>
    /// Set an attribute if it's not already present.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void EnsureAttr<T>(IDictionary<string, object> attrs, string name, T value)
    {
        if (!attrs.ContainsKey(name))
            attrs[name] = value!;
    }

    /// <summary>
    /// Set attribute to a value when condition is true; otherwise remove it.
    /// Useful for aria flags, boolean attrs, etc.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void SetOrRemove(IDictionary<string, object> attrs, string name, bool condition, object trueValue)
    {
        if (condition)
            attrs[name] = trueValue;
        else
            attrs.Remove(name);
    }

    /// <summary>
    /// Append a style declaration (e.g., "color: red") onto attributes["style"] with proper "; " separation.
    /// No duplicate guarding—use higher-level logic if you need that.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendStyleDeclAttr(IDictionary<string, object> attrs, string fullDecl)
    {
        if (fullDecl.IsNullOrWhiteSpace())
            return;

        attrs.TryGetValue("style", out object? styleObj);
        var existing = styleObj?.ToString();

        if (existing.IsNullOrEmpty())
        {
            attrs["style"] = fullDecl;
            return;
        }

        // ensure delimiter
        if (existing!.EndsWith(';'))
            attrs["style"] = $"{existing} {fullDecl}";
        else
            attrs["style"] = $"{existing}; {fullDecl}";
    }

    private void ApplyThemeStyles()
    {
        if (ThemeProvider?.Themes == null)
            return;

        // Get the current theme name
        string? themeName = ThemeProvider.CurrentTheme;

        if (themeName.IsNullOrEmpty())
            return;

        if (ThemeProvider.Themes.TryGetValue(themeName, out Theme? theme))
        {
            ApplyThemeToComponent(theme);
        }
    }

    private void ApplyThemeToComponent(Theme theme)
    {
        // Get the appropriate component options based on component name
        ComponentOptions? componentOptions = GetComponentOptionsFromTheme(theme);

        if (componentOptions == null)
            return;

        ApplyThemeProperty(componentOptions.Display, () => Display, value => Display = value);
        ApplyThemeProperty(componentOptions.Visibility, () => Visibility, value => Visibility = value);
        ApplyThemeProperty(componentOptions.Float, () => Float, value => Float = value);
        ApplyThemeProperty(componentOptions.VerticalAlign, () => VerticalAlign, value => VerticalAlign = value);
        ApplyThemeProperty(componentOptions.TextOverflow, () => TextOverflow, value => TextOverflow = value);
        ApplyThemeProperty(componentOptions.BoxShadow, () => BoxShadow, value => BoxShadow = value);
        ApplyThemeProperty(componentOptions.Margin, () => Margin, value => Margin = value);
        ApplyThemeProperty(componentOptions.Padding, () => Padding, value => Padding = value);
        ApplyThemeProperty(componentOptions.Position, () => Position, value => Position = value);
        ApplyThemeProperty(componentOptions.Offset, () => Offset, value => Offset = value);
        ApplyThemeProperty(componentOptions.TextSize, () => TextSize, value => TextSize = value);
        ApplyThemeProperty(componentOptions.Width, () => Width, value => Width = value);
        ApplyThemeProperty(componentOptions.MinWidth, () => MinWidth, value => MinWidth = value);
        ApplyThemeProperty(componentOptions.MaxWidth, () => MaxWidth, value => MaxWidth = value);
        ApplyThemeProperty(componentOptions.Height, () => Height, value => Height = value);
        ApplyThemeProperty(componentOptions.MinHeight, () => MinHeight, value => MinHeight = value);
        ApplyThemeProperty(componentOptions.MaxHeight, () => MaxHeight, value => MaxHeight = value);
        ApplyThemeProperty(componentOptions.Overflow, () => Overflow, value => Overflow = value);
        ApplyThemeProperty(componentOptions.OverflowX, () => OverflowX, value => OverflowX = value);
        ApplyThemeProperty(componentOptions.OverflowY, () => OverflowY, value => OverflowY = value);
        ApplyThemeProperty(componentOptions.ObjectFit, () => ObjectFit, value => ObjectFit = value);
        ApplyThemeProperty(componentOptions.TextAlignment, () => TextAlignment, value => TextAlignment = value);
        ApplyThemeProperty(componentOptions.TextDecorationLine, () => TextDecorationLine, value => TextDecorationLine = value);
        ApplyThemeProperty(componentOptions.TextDecorationCss, () => TextDecorationCss, value => TextDecorationCss = value);
        ApplyThemeProperty(componentOptions.Flex, () => Flex, value => Flex = value);
        ApplyThemeProperty(componentOptions.Gap, () => Gap, value => Gap = value);
        ApplyThemeProperty(componentOptions.Border, () => Border, value => Border = value);
        ApplyThemeProperty(componentOptions.Opacity, () => Opacity, value => Opacity = value);
        ApplyThemeProperty(componentOptions.ZIndex, () => ZIndex, value => ZIndex = value);
        ApplyThemeProperty(componentOptions.PointerEvents, () => PointerEvents, value => PointerEvents = value);
        ApplyThemeProperty(componentOptions.UserSelect, () => UserSelect, value => UserSelect = value);
        ApplyThemeProperty(componentOptions.TextTransform, () => TextTransform, value => TextTransform = value);
        ApplyThemeProperty(componentOptions.FontWeight, () => FontWeight, value => FontWeight = value);
        ApplyThemeProperty(componentOptions.FontStyle, () => FontStyle, value => FontStyle = value);
        ApplyThemeProperty(componentOptions.LineHeight, () => LineHeight, value => LineHeight = value);
        ApplyThemeProperty(componentOptions.TextWrap, () => TextWrap, value => TextWrap = value);
        ApplyThemeProperty(componentOptions.TextBreak, () => TextBreak, value => TextBreak = value);
        ApplyThemeProperty(componentOptions.TextColor, () => TextColor, value => TextColor = value);
        ApplyThemeProperty(componentOptions.BackgroundColor, () => BackgroundColor, value => BackgroundColor = value);
        ApplyThemeProperty(componentOptions.TextBackgroundColor, () => TextBackgroundColor, value => TextBackgroundColor = value);
        ApplyThemeProperty(componentOptions.Animation, () => Animation, value => Animation = value);
        ApplyThemeProperty(componentOptions.AspectRatio, () => AspectRatio, value => AspectRatio = value);
        ApplyThemeProperty(componentOptions.BackdropFilter, () => BackdropFilter, value => BackdropFilter = value);
        ApplyThemeProperty(componentOptions.BorderRadius, () => BorderRadius, value => BorderRadius = value);
        ApplyThemeProperty(componentOptions.Clearfix, () => Clearfix, value => Clearfix = value);
        ApplyThemeProperty(componentOptions.ClipPath, () => ClipPath, value => ClipPath = value);
        ApplyThemeProperty(componentOptions.Cursor, () => Cursor, value => Cursor = value);
        ApplyThemeProperty(componentOptions.Filter, () => Filter, value => Filter = value);
        ApplyThemeProperty(componentOptions.Interaction, () => Interaction, value => Interaction = value);
        ApplyThemeProperty(componentOptions.ObjectPosition, () => ObjectPosition, value => ObjectPosition = value);
        ApplyThemeProperty(componentOptions.Resize, () => Resize, value => Resize = value);
        ApplyThemeProperty(componentOptions.ScreenReader, () => ScreenReader, value => ScreenReader = value);
        ApplyThemeProperty(componentOptions.ScrollBehavior, () => ScrollBehavior, value => ScrollBehavior = value);
        ApplyThemeProperty(componentOptions.StretchedLink, () => StretchedLink, value => StretchedLink = value);
        ApplyThemeProperty(componentOptions.Transform, () => Transform, value => Transform = value);
        ApplyThemeProperty(componentOptions.Transition, () => Transition, value => Transition = value);
        ApplyThemeProperty(componentOptions.Truncate, () => Truncate, value => Truncate = value);
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
            Logger?.LogWarning("ThemeKey is null for {type}; cannot apply theme styles.", GetType().FullName);
            return null;
        }

        return ThemeProvider.ComponentOptions.TryGetValue(ThemeKey, out Func<Theme, ComponentOptions?>? getter) ? getter(theme) : null;
    }
}