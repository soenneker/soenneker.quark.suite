using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap column auto utilities.
/// </summary>
public class ColumnAutoBuilder : ICssBuilder
{
    private ColumnAuto? _columnAuto;

    public bool IsEmpty => !_columnAuto.HasValue;
    public bool IsCssClass => true;
    public bool IsCssStyle => false;

    public ColumnAutoBuilder Set(ColumnAuto columnAuto)
    {
        _columnAuto = columnAuto;
        return this;
    }

    public ColumnAutoBuilder Auto()
    {
        _columnAuto = ColumnAuto.Auto;
        return this;
    }

    public ColumnAutoBuilder AutoSmall()
    {
        _columnAuto = ColumnAuto.AutoSmall;
        return this;
    }

    public ColumnAutoBuilder AutoMedium()
    {
        _columnAuto = ColumnAuto.AutoMedium;
        return this;
    }

    public ColumnAutoBuilder AutoLarge()
    {
        _columnAuto = ColumnAuto.AutoLarge;
        return this;
    }

    public ColumnAutoBuilder AutoExtraLarge()
    {
        _columnAuto = ColumnAuto.AutoExtraLarge;
        return this;
    }

    public ColumnAutoBuilder AutoExtraExtraLarge()
    {
        _columnAuto = ColumnAuto.AutoExtraExtraLarge;
        return this;
    }

    public override string ToString()
    {
        if (!_columnAuto.HasValue) return string.Empty;

        return GetColumnAutoClass(_columnAuto.Value);
    }

    private static string GetColumnAutoClass(ColumnAuto columnAuto)
    {
        return columnAuto.Type.Value switch
        {
            ColumnAutoType.AutoValue => columnAuto.Breakpoint.Value switch
            {
                ColumnAutoBreakpoint.SmValue => "col-sm-auto",
                ColumnAutoBreakpoint.MdValue => "col-md-auto",
                ColumnAutoBreakpoint.LgValue => "col-lg-auto",
                ColumnAutoBreakpoint.XlValue => "col-xl-auto",
                ColumnAutoBreakpoint.XxlValue => "col-xxl-auto",
                _ => "col-auto"
            },
            _ => "col-auto"
        };
    }

    public string ToClass() => ToString();
    public string ToStyle() => string.Empty;
}
