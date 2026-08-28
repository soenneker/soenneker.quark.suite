using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Extensions.String;
using Soenneker.Lepton.Suite;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Minimal suite-level render base that owns render invalidation, render-key computation,
/// attribute caching, attribute merging, and helper utilities.
/// </summary>
public abstract class RenderComponent : LeptonDisposableIdentifiableContentElement, IHandleEvent
{
    private static readonly ConcurrentDictionary<Type, bool> _mutationSensitiveCascadingParameterTypes = new();
    private bool _shouldRender = true;
    private int _lastRenderKey;
    private Dictionary<string, object>? _cachedAttrs;
    private Dictionary<string, object>? _attrsA;
    private Dictionary<string, object>? _attrsB;
    private int _cachedAttrsKey;
    private int _renderVersion;
    private int _incomingParametersKey;
    private int _lastIncomingParametersKey;
    private bool _renderKeyDirty;
    private bool _useAttrsA;
    private bool _hasIncomingParametersKey;
    private bool _incomingParametersChanged;
    private bool _defaultsApplied;
    private bool? _hasMutationSensitiveCascadingParameters;

    /// <summary>
    /// Quark-level explicit attribute bag. Unmatched attributes are still captured by the inherited
    /// <c>AdditionalAttributes</c> parameter.
    /// </summary>
    [Parameter]
    public IReadOnlyDictionary<string, object>? Attributes { get; set; }

    /// <summary>
    /// Allows higher-level bases to opt into always-render behavior without coupling the core render pipeline
    /// to suite-specific services or options.
    /// </summary>
    protected virtual bool AlwaysRender => true;

    /// <inheritdoc />
    public override Task SetParametersAsync(ParameterView parameters)
    {
        _defaultsApplied = false;
        _incomingParametersKey = ComputeIncomingParametersKey(parameters);
        _incomingParametersChanged = !_hasIncomingParametersKey || _incomingParametersKey != _lastIncomingParametersKey;

        if (_incomingParametersChanged)
        {
            _cachedAttrs = null;
            _cachedAttrsKey = 0;
        }

        return base.SetParametersAsync(parameters);
    }

    /// <summary>
    /// Executes the refresh operation.
    /// </summary>
    public void Refresh()
    {
        InvalidateRender();
        StateHasChanged();
    }

    /// <summary>
    /// Executes the refresh off thread operation.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task RefreshOffThread()
    {
        InvalidateRender();
        return InvokeAsync(StateHasChanged);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void InvalidateRender()
    {
        unchecked
        {
            _renderVersion++;
        }

        // Multiple state changes can be coalesced into one render. Defer the expensive
        // component-wide hash until attributes are actually requested for that render.
        _renderKeyDirty = true;
        _defaultsApplied = false;
        _cachedAttrs = null;
        _cachedAttrsKey = 0;
        _shouldRender = true;
    }

    protected override bool ShouldRender()
    {
        if (AlwaysRender)
            return true;

        return _shouldRender;
    }

    protected override void OnParametersSet()
    {
        EnsureDefaultParameters();

        if (AlwaysRender)
        {
            _shouldRender = true;
            _renderKeyDirty = false;
            CommitIncomingParametersKey();
            return;
        }

        var hasMutationSensitiveCascadingParameters = HasMutationSensitiveCascadingParameters();

        // An unchanged parameter set is the hot path in large component trees. The inexpensive
        // ParameterView fingerprint lets us avoid recomputing every inherited and component-local
        // render-key value. Conversely, any changed parameter forces a render, so a parameter that
        // a component forgot to include in ComputeRenderKeyCore cannot leave stale UI.
        if (!_incomingParametersChanged && !_renderKeyDirty && !hasMutationSensitiveCascadingParameters)
        {
            _shouldRender = false;
            return;
        }

        // Direct parameter changes already guarantee a render and invalidate the attribute cache.
        // Avoid the detailed component-wide key walk unless a mutable cascading context can affect
        // output without changing its reference.
        if (_incomingParametersChanged && !hasMutationSensitiveCascadingParameters)
        {
            _lastRenderKey = HashCode.Combine(_incomingParametersKey, _renderVersion);
            _shouldRender = true;
            _renderKeyDirty = false;
            CommitIncomingParametersKey();
            return;
        }

        var key = ComputeRenderKey();
        _shouldRender = _incomingParametersChanged || key != _lastRenderKey;
        _lastRenderKey = key;
        _renderKeyDirty = false;
        CommitIncomingParametersKey();
    }

    protected override Dictionary<string, object> BuildAttributes()
    {
        EnsureDefaultParameters();

        if (!AlwaysRender && _renderKeyDirty)
        {
            _lastRenderKey = ComputeRenderKey();
            _renderKeyDirty = false;
        }

        var currentKey = _lastRenderKey;

        if (!AlwaysRender && _cachedAttrs is not null && _cachedAttrsKey == currentKey)
            return _cachedAttrs;

        var attrs = BeginAttributeBuild(8 + (AdditionalAttributes?.Count ?? 0) + (Attributes?.Count ?? 0));
        var cls = new PooledStringBuilder(64);
        var sty = new PooledStringBuilder(128);

        try
        {
            BuildOwnedAttributes(attrs);
            BuildOwnedClassAndStyle(ref sty, ref cls);
            MergeAttributes(AdditionalAttributes, attrs, ref sty, ref cls);
            MergeAttributes(Attributes, attrs, ref sty, ref cls);

            if (cls.Length > 0)
                attrs["class"] = cls.ToString();

            if (sty.Length > 0)
                attrs["style"] = sty.ToString();

            BuildAttributesCore(attrs);
            BuildFinalAttributes(attrs);

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

    private Dictionary<string, object> BeginAttributeBuild(int capacity)
    {
        _useAttrsA = !_useAttrsA;
        ref var buffer = ref (_useAttrsA ? ref _attrsA : ref _attrsB);

        if (buffer is null)
        {
            buffer = new Dictionary<string, object>(capacity, StringComparer.OrdinalIgnoreCase);
            return buffer;
        }

        buffer.Clear();
        buffer.EnsureCapacity(capacity);
        return buffer;
    }

    protected virtual void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        if (Id.HasContent())
            attrs["id"] = Id!;
    }

    protected virtual void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
    }

    /// <summary>
    /// Applies default parameter values before render-key computation and attribute emission.
    /// Components should set inherited builder-backed defaults here instead of hard-coding
    /// competing utility classes in their emitted class contracts.
    /// </summary>
    /// <remarks>
    /// The render pipeline invokes this hook at most once for each parameter or internal invalidation generation, even when attributes
    /// are requested more than once. Implementations should remain idempotent and use default-only assignments such as <c>??=</c>.
    /// </remarks>
    protected virtual void ApplyDefaultParameters()
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnsureDefaultParameters()
    {
        if (_defaultsApplied)
            return;

        ApplyDefaultParameters();
        _defaultsApplied = true;
    }

    /// <summary>
    /// Final attribute hook for the concrete component. Use this for component-local attributes,
    /// class tweaks, or style tweaks after shared ownership has been applied.
    /// </summary>
    protected virtual void BuildAttributesCore(Dictionary<string, object> attrs)
    {
    }

    /// <summary>
    /// Final shared attribute hook after concrete component defaults have been applied.
    /// </summary>
    protected virtual void BuildFinalAttributes(Dictionary<string, object> attrs)
    {
    }

    // Keep this generic nullable-struct path out of large component methods. Mono AOT can
    // otherwise inline every closed CssValue<T> instantiation into the caller and create
    // enormous native frames that corrupt managed references under AOT.
    [MethodImpl(MethodImplOptions.NoInlining)]
    protected static void AddIf<T>(ref HashCode hc, CssValue<T>? v) where T : class, ICssBuilder
    {
        if (v is { IsEmpty: false })
            hc.Add(v.Value);
    }

    private int ComputeRenderKey()
    {
        var hc = new HashCode();

        hc.Add(_renderVersion);
        hc.Add(Id);
        AddAttributesToRenderKey(ref hc, AdditionalAttributes);
        AddAttributesToRenderKey(ref hc, Attributes);
        ComputeRenderKeyCore(ref hc);

        return hc.ToHashCode();
    }

    private static int ComputeIncomingParametersKey(ParameterView parameters)
    {
        var hashCode = new HashCode();

        foreach (var parameter in parameters)
        {
            hashCode.Add(parameter.Name, StringComparer.Ordinal);
            AddIncomingParameterValue(ref hashCode, parameter.Value);
        }

        return hashCode.ToHashCode();
    }

    private static void AddIncomingParameterValue(ref HashCode hashCode, object? value)
    {
        if (value is IReadOnlyDictionary<string, object> attributes)
        {
            AddAttributesToRenderKey(ref hashCode, attributes);
            return;
        }

        hashCode.Add(value);
    }

    private void CommitIncomingParametersKey()
    {
        _lastIncomingParametersKey = _incomingParametersKey;
        _hasIncomingParametersKey = true;
        _incomingParametersChanged = false;
    }

    private bool HasMutationSensitiveCascadingParameters()
    {
        if (_hasMutationSensitiveCascadingParameters.HasValue)
            return _hasMutationSensitiveCascadingParameters.Value;

        _hasMutationSensitiveCascadingParameters = _mutationSensitiveCascadingParameterTypes.GetOrAdd(GetType(), static componentType =>
        {
            for (Type? type = componentType; type is not null && type != typeof(RenderComponent); type = type.BaseType)
            {
                var properties = type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public |
                                                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.DeclaredOnly);

                for (var index = 0; index < properties.Length; index++)
                {
                    if (properties[index].IsDefined(typeof(CascadingParameterAttribute), inherit: true) &&
                        !IsKnownImmutableCascadingType(properties[index].PropertyType))
                        return true;
                }
            }

            return false;
        });

        return _hasMutationSensitiveCascadingParameters.Value;
    }

    private static bool IsKnownImmutableCascadingType(Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;

        return type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime) ||
               type == typeof(DateTimeOffset) || type == typeof(TimeSpan) || type == typeof(DateOnly) || type == typeof(TimeOnly) ||
               type == typeof(Guid) || type == typeof(Type);
    }

    async Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem callback, object? argument)
    {
        Task callbackTask = callback.InvokeAsync(argument);

        // Event handlers are allowed to mutate component-local state. Make that mutation visible
        // without requiring every handler in every component to remember InvalidateRender().
        InvalidateRender();
        StateHasChanged();

        if (callbackTask.IsCompletedSuccessfully || callbackTask.IsCanceled)
            return;

        try
        {
            await callbackTask;
        }
        catch
        {
            if (callbackTask.IsCanceled)
                return;

            throw;
        }

        InvalidateRender();
        StateHasChanged();
    }

    protected virtual void ComputeRenderKeyCore(ref HashCode hc)
    {
    }

    private static void AddAttributesToRenderKey(ref HashCode hc, IReadOnlyDictionary<string, object>? attributes)
    {
        if (attributes is null || attributes.Count == 0)
            return;

        if (attributes is Dictionary<string, object> dictionary)
        {
            foreach (var kv in dictionary)
            {
                hc.Add(kv.Key, StringComparer.OrdinalIgnoreCase);
                hc.Add(kv.Value);
            }

            return;
        }

        foreach (var kv in attributes)
        {
            hc.Add(kv.Key, StringComparer.OrdinalIgnoreCase);
            hc.Add(kv.Value);
        }
    }

    private static void MergeAttributes(IReadOnlyDictionary<string, object>? attributes, Dictionary<string, object> attrs, ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        if (attributes is null)
            return;

        if (attributes is Dictionary<string, object> dictionary)
        {
            foreach (var kv in dictionary)
                MergeAttribute(kv, attrs, ref sty, ref cls);

            return;
        }

        foreach (var kv in attributes)
            MergeAttribute(kv, attrs, ref sty, ref cls);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void MergeAttribute(KeyValuePair<string, object> kv, Dictionary<string, object> attrs, ref PooledStringBuilder sty,
        ref PooledStringBuilder cls)
    {
        var k = kv.Key;
        var v = kv.Value;

        if (k.Equals("class", StringComparison.OrdinalIgnoreCase))
        {
            var s = v as string ?? v?.ToString();

            if (!string.IsNullOrEmpty(s))
                AppendClass(ref cls, s);

            return;
        }

        if (k.Equals("style", StringComparison.OrdinalIgnoreCase))
        {
            var s = v as string ?? v?.ToString();

            if (!string.IsNullOrEmpty(s))
                AppendStyleDecl(ref sty, s);

            return;
        }

        attrs[k] = v;
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
        if (fullDecl.IsNullOrEmpty())
            return;

        if (b.Length != 0)
        {
            b.Append(';');
            b.Append(' ');
        }

        b.Append(fullDecl);
    }

    // This is intentionally a hard AOT boundary. Component.BuildOwnedClassAndStyle invokes
    // many closed generic versions of this method; forced inlining produced a ~939 KB AOT
    // function with a ~96 KB stack frame in a production build.
    [MethodImpl(MethodImplOptions.NoInlining)]
    protected static void AddCss<T>(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<T>? v) where T : class, ICssBuilder
    {
        if (v is not { IsEmpty: false })
            return;

        if (v.Value.IsCssStyle)
        {
            var style = v.Value.StyleValue;

            if (style.Length != 0)
                AppendStyleDecl(ref styB, style);

            return;
        }

        var classText = v.Value.ToString();

        if (classText.Length != 0)
            AppendClass(ref clsB, classText);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static string EnsureClass(string? existing, string? toAdd)
    {
        if (toAdd.IsNullOrEmpty())
            return existing ?? string.Empty;

        if (existing.IsNullOrEmpty())
            return toAdd;

        return string.Concat(existing, " ", toAdd);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static string AppendToClass(string? existing, string toAdd)
    {
        if (toAdd.IsNullOrEmpty())
            return existing ?? string.Empty;

        if (existing.IsNullOrEmpty())
            return toAdd;

        return string.Concat(existing, " ", toAdd);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void EnsureClassAttr(Dictionary<string, object> attrs, string token)
    {
        attrs.TryGetValue("class", out var clsObj);
        var cls = EnsureClass(clsObj?.ToString(), token);

        if (cls.Length > 0)
            attrs["class"] = cls;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendToClassAttr(Dictionary<string, object> attrs, string token)
    {
        attrs.TryGetValue("class", out var clsObj);
        var cls = AppendToClass(clsObj?.ToString(), token);

        if (cls.Length > 0)
            attrs["class"] = cls;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendClassAttribute(Dictionary<string, object> attrs, string? className)
    {
        if (string.IsNullOrWhiteSpace(className))
            return;

        attrs.TryGetValue("class", out var existingObj);
        var existing = existingObj as string ?? existingObj?.ToString();

        attrs["class"] = existing.IsNullOrEmpty()
            ? className
            : string.Concat(existing, " ", className);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendClassAttribute(Dictionary<string, object> attrs, params string?[] classes)
    {
        attrs.TryGetValue("class", out var existingObj);
        var existing = existingObj?.ToString();
        var builder = new PooledStringBuilder(64);

        try
        {
            if (classes is not null)
            {
                for (var i = 0; i < classes.Length; i++)
                {
                    var className = classes[i];

                    if (!string.IsNullOrWhiteSpace(className))
                        AppendClass(ref builder, className);
                }
            }

            var cls = AppendToClass(existing, builder.ToString());

            if (cls.Length > 0)
                attrs["class"] = cls;
        }
        finally
        {
            builder.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void BuildClassAttribute(Dictionary<string, object> attrs, BuildClassAction builder)
    {
        var cls = new PooledStringBuilder(64);

        try
        {
            builder(ref cls);

            attrs.TryGetValue("class", out var existing);
            var existingString = existing as string ?? existing?.ToString();

            if (cls.Length == 0)
            {
                if (existingString.HasContent())
                    attrs["class"] = existingString!;
                return;
            }

            if (existingString.HasContent())
                AppendClass(ref cls, existingString!);

            attrs["class"] = cls.ToString();
        }
        finally
        {
            cls.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void BuildStyleAttribute(Dictionary<string, object> attrs, BuildStyleAction builder)
    {
        var sty = new PooledStringBuilder(64);

        try
        {
            builder(ref sty);

            if (attrs.TryGetValue("style", out var existing))
            {
                var existingStr = existing.ToString();

                if (existingStr.HasContent())
                    AppendStyleDecl(ref sty, existingStr);
            }

            if (sty.Length > 0)
                attrs["style"] = sty.ToString();
        }
        finally
        {
            sty.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    protected static void BuildClassAndStyleAttributes(Dictionary<string, object> attrs, BuildClassAndStyleAction builder)
    {
        attrs.TryGetValue("class", out var existingClassObj);
        attrs.TryGetValue("style", out var existingStyleObj);

        var existingClassStr = existingClassObj as string ?? existingClassObj?.ToString();
        var existingStyleStr = existingStyleObj as string ?? existingStyleObj?.ToString();

        var existingClassLen = existingClassStr?.Length ?? 0;
        var existingStyleLen = existingStyleStr?.Length ?? 0;

        var cls = new PooledStringBuilder(Math.Max(32, existingClassLen + 32));
        var sty = new PooledStringBuilder(Math.Max(32, existingStyleLen + 32));

        try
        {
            builder(ref cls, ref sty);

            if (existingClassLen != 0)
                AppendClass(ref cls, existingClassStr!);

            if (existingStyleLen != 0)
                AppendStyleDecl(ref sty, existingStyleStr!);

            if (cls.Length > 0)
                attrs["class"] = existingClassObj is string && cls.Length == existingClassLen
                    ? existingClassObj
                    : cls.ToString();

            if (sty.Length > 0)
                attrs["style"] = existingStyleObj is string && sty.Length == existingStyleLen ? existingStyleObj : sty.ToString();
        }
        finally
        {
            cls.Dispose();
            sty.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendStyleDeclAttr(Dictionary<string, object> attrs, string fullDecl)
    {
        if (string.IsNullOrWhiteSpace(fullDecl))
            return;

        attrs.TryGetValue("style", out var styleObj);

        if (styleObj is string existing && existing.Length != 0)
        {
            using var b = new PooledStringBuilder(existing.Length + 2 + fullDecl.Length);

            b.Append(existing);

            if (existing[^1] != ';')
                b.Append(';');

            b.Append(' ');
            b.Append(fullDecl);
            attrs["style"] = b.ToString();
            return;
        }

        attrs["style"] = fullDecl;
    }

    protected virtual void ApplyBorderColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<BorderColorBuilder>? value)
    {
        AddCss(ref sty, ref cls, value);
    }

    protected virtual void ApplyTextColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<TextColorBuilder>? value)
    {
        AddCss(ref sty, ref cls, value);
    }

    protected virtual void ApplyBackgroundColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<BackgroundColorBuilder>? value)
    {
        AddCss(ref sty, ref cls, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendStyleDecl(ref PooledStringBuilder b, ReadOnlySpan<char> name, string value)
    {
        if (value.Length == 0)
            return;

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
