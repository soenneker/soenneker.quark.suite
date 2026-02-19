using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance text alignment builder with fluent API for chaining text alignment rules.
/// </summary>
public sealed class TextAlignmentBuilder : ICssBuilder
{
    private readonly List<TextAlignmentRule> _rules = new(4);

    // ----- Class name constants (compile-time) -----
    private const string _classStart = "text-start";
    private const string _classCenter = "text-center";
    private const string _classEnd = "text-end";

    // ----- CSS prefix (compile-time) -----
    private const string _textAlignPrefix = "text-align: ";

    // ----- Style constants (compile-time, Intellenum *Value are const) -----
    private const string _styleStart = $"{_textAlignPrefix}{TextAlignKeyword.StartValue}";
    private const string _styleCenter = $"{_textAlignPrefix}{TextAlignKeyword.CenterValue}";
    private const string _styleEnd = $"{_textAlignPrefix}{TextAlignKeyword.EndValue}";
    private const string _styleInherit = $"{_textAlignPrefix}{GlobalKeyword.InheritValue}";
    private const string _styleInitial = $"{_textAlignPrefix}{GlobalKeyword.InitialValue}";
    private const string _styleUnset = $"{_textAlignPrefix}{GlobalKeyword.UnsetValue}";
    private const string _styleRevert = $"{_textAlignPrefix}{GlobalKeyword.RevertValue}";
    private const string _styleRevertLayer = $"{_textAlignPrefix}{GlobalKeyword.RevertLayerValue}";

    internal TextAlignmentBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextAlignmentRule(value, breakpoint));
    }

    internal TextAlignmentBuilder(List<TextAlignmentRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

	/// <summary>
	/// Sets the text alignment to start.
	/// </summary>
    public TextAlignmentBuilder Start => Chain(TextAlignKeyword.StartValue);
	/// <summary>
	/// Sets the text alignment to center.
	/// </summary>
    public TextAlignmentBuilder Center => Chain(TextAlignKeyword.CenterValue);
	/// <summary>
	/// Sets the text alignment to end.
	/// </summary>
    public TextAlignmentBuilder End => Chain(TextAlignKeyword.EndValue);

	/// <summary>
	/// Sets the text alignment to inherit.
	/// </summary>
    public TextAlignmentBuilder Inherit => Chain(GlobalKeyword.InheritValue);
	/// <summary>
	/// Sets the text alignment to initial.
	/// </summary>
    public TextAlignmentBuilder Initial => Chain(GlobalKeyword.InitialValue);
	/// <summary>
	/// Sets the text alignment to revert.
	/// </summary>
    public TextAlignmentBuilder Revert => Chain(GlobalKeyword.RevertValue);
	/// <summary>
	/// Sets the text alignment to revert-layer.
	/// </summary>
    public TextAlignmentBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
	/// <summary>
	/// Sets the text alignment to unset.
	/// </summary>
    public TextAlignmentBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextAlignmentBuilder Chain(string value)
    {
        _rules.Add(new TextAlignmentRule(value, null));
        return this;
    }

	/// <summary>
	/// Gets the CSS class string for the current configuration.
	/// </summary>
	/// <returns>The CSS class string.</returns>
    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            TextAlignmentRule rule = _rules[i];

            string baseClass = rule.Value switch
            {
                TextAlignKeyword.StartValue => _classStart,
                TextAlignKeyword.CenterValue => _classCenter,
                TextAlignKeyword.EndValue => _classEnd,
                _ => string.Empty
            };

            if (baseClass.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                baseClass = BreakpointUtil.ApplyTailwindBreakpoint(baseClass, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(baseClass);
        }

        return sb.ToString();
    }

	/// <summary>
	/// Gets the CSS style string for the current configuration.
	/// </summary>
	/// <returns>The CSS style string.</returns>
    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            TextAlignmentRule rule = _rules[i];

            string css = rule.Value switch
            {
                TextAlignKeyword.StartValue => _styleStart,
                TextAlignKeyword.CenterValue => _styleCenter,
                TextAlignKeyword.EndValue => _styleEnd,

                GlobalKeyword.InheritValue => _styleInherit,
                GlobalKeyword.InitialValue => _styleInitial,
                GlobalKeyword.UnsetValue => _styleUnset,
                GlobalKeyword.RevertValue => _styleRevert,
                GlobalKeyword.RevertLayerValue => _styleRevertLayer,

                _ => string.Empty
            };

            if (css.Length == 0)
                continue;

            if (!first)
                sb.Append("; ");
            else
                first = false;

            sb.Append(css);
        }

        return sb.ToString();
    }
}
