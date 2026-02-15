using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap column auto utilities.
/// </summary>
public class ColumnAutoBuilder : ICssBuilder
{
    private ColumnAuto? _columnAuto;

    /// <summary>
    /// Gets a value indicating whether the builder is empty (no column auto set).
    /// </summary>
    public bool IsEmpty => !_columnAuto.HasValue;
    /// <summary>
    /// Gets a value indicating whether this builder generates CSS classes (always true).
    /// </summary>
    public bool IsCssClass => true;
    /// <summary>
    /// Gets a value indicating whether this builder generates inline styles (always false).
    /// </summary>
    public bool IsCssStyle => false;

    /// <summary>
    /// Sets the column auto value.
    /// </summary>
    /// <param name="columnAuto">The column auto value to set.</param>
    /// <returns>The builder instance for method chaining.</returns>
    public ColumnAutoBuilder Set(ColumnAuto columnAuto)
    {
        _columnAuto = columnAuto;
        return this;
    }

    /// <summary>
    /// Sets the column auto to auto (no breakpoint).
    /// </summary>
    /// <returns>The builder instance for method chaining.</returns>
    public ColumnAutoBuilder Auto()
    {
        _columnAuto = ColumnAuto.Auto;
        return this;
    }

    /// <summary>
    /// Sets the column auto to auto with small breakpoint.
    /// </summary>
    /// <returns>The builder instance for method chaining.</returns>
    public ColumnAutoBuilder AutoSmall()
    {
        _columnAuto = ColumnAuto.AutoSmall;
        return this;
    }

    /// <summary>
    /// Sets the column auto to auto with medium breakpoint.
    /// </summary>
    /// <returns>The builder instance for method chaining.</returns>
    public ColumnAutoBuilder AutoMedium()
    {
        _columnAuto = ColumnAuto.AutoMedium;
        return this;
    }

    /// <summary>
    /// Sets the column auto to auto with large breakpoint.
    /// </summary>
    /// <returns>The builder instance for method chaining.</returns>
    public ColumnAutoBuilder AutoLarge()
    {
        _columnAuto = ColumnAuto.AutoLarge;
        return this;
    }

    /// <summary>
    /// Sets the column auto to auto with extra large breakpoint.
    /// </summary>
    /// <returns>The builder instance for method chaining.</returns>
    public ColumnAutoBuilder AutoExtraLarge()
    {
        _columnAuto = ColumnAuto.AutoExtraLarge;
        return this;
    }

    /// <summary>
    /// Sets the column auto to auto with extra extra large breakpoint.
    /// </summary>
    /// <returns>The builder instance for method chaining.</returns>
    public ColumnAutoBuilder AutoExtraExtraLarge()
    {
        _columnAuto = ColumnAuto.AutoExtraExtraLarge;
        return this;
    }

    /// <summary>
    /// Returns the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string, or an empty string if no column auto is set.</returns>
    public override string ToString()
    {
        if (!_columnAuto.HasValue) return string.Empty;

        return GetColumnAutoClass(_columnAuto.Value);
    }

    private static string GetColumnAutoClass(ColumnAuto columnAuto)
    {
        const string colAuto = "col-auto";
        if (columnAuto.Type.Value != ColumnAutoType.AutoValue)
            return colAuto;
        string? bp = columnAuto.Breakpoint.Value;
        if (string.IsNullOrEmpty(bp))
            return colAuto;
        // Tailwind responsive: sm:col-auto, md:col-auto, etc. (2xl not xxl)
        string tailwindBp = bp == "xxl" ? "2xl" : bp;
        return $"{tailwindBp}:{colAuto}";
    }

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string, or an empty string if no column auto is set.</returns>
    public string ToClass() => ToString();
    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// Column autos are typically handled via Bootstrap classes, not inline styles.
    /// </summary>
    /// <returns>An empty string, as column autos are handled via classes.</returns>
    public string ToStyle() => string.Empty;
}
