using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
namespace Soenneker.Quark;
/// <summary>Evaluates filter state locally without reflection. Server applications can translate the same DTOs to their query language.</summary>
public static class FilterEvaluator
{
    public static IReadOnlyList<FilterOperator> Operators(FilterFieldType type) => type switch
    {
        FilterFieldType.Text => [FilterOperator.Equals, FilterOperator.NotEquals, FilterOperator.Contains, FilterOperator.StartsWith, FilterOperator.IsEmpty, FilterOperator.IsNotEmpty],
        FilterFieldType.Number or FilterFieldType.Date => [FilterOperator.Equals, FilterOperator.NotEquals, FilterOperator.GreaterThan, FilterOperator.LessThan, FilterOperator.IsEmpty, FilterOperator.IsNotEmpty],
        _ => [FilterOperator.Equals, FilterOperator.NotEquals, FilterOperator.IsEmpty, FilterOperator.IsNotEmpty]
    };
    public static bool IsValid(FilterRule rule, FilterField field)
    {
        if (!Operators(field.Type).Contains(rule.Operator)) return false;
        if (rule.Operator is FilterOperator.IsEmpty or FilterOperator.IsNotEmpty) return true;
        return field.Type switch
        {
            FilterFieldType.Number => decimal.TryParse(rule.Value, NumberStyles.Number, CultureInfo.InvariantCulture, out _),
            FilterFieldType.Date => DateOnly.TryParseExact(rule.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _),
            FilterFieldType.Boolean => bool.TryParse(rule.Value, out _),
            FilterFieldType.Choice => field.Choices?.Any(x => x.Value == rule.Value && !x.Disabled) == true,
            _ => rule.Value is not null
        };
    }
    public static bool Matches(FilterGroup group, IReadOnlyList<FilterField> fields, Func<string, object?> getValue)
    {
        var results = group.Rules.Select(rule => fields.FirstOrDefault(f => f.Key == rule.Field) is { } field && Match(rule, field, getValue(rule.Field)))
            .Concat(group.Groups.Select(child => Matches(child, fields, getValue))).ToArray();
        return results.Length == 0 || (group.Junction == FilterJunction.All ? results.All(x => x) : results.Any(x => x));
    }
    private static bool Match(FilterRule rule, FilterField field, object? raw)
    {
        if (!IsValid(rule, field)) return false;
        var value = Convert.ToString(raw, CultureInfo.InvariantCulture);
        if (rule.Operator == FilterOperator.IsEmpty) return string.IsNullOrEmpty(value);
        if (rule.Operator == FilterOperator.IsNotEmpty) return !string.IsNullOrEmpty(value);
        if (value is null) return false;
        int comparison;
        if (field.Type == FilterFieldType.Number)
        {
            if (!decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var number)) return false;
            comparison = number.CompareTo(decimal.Parse(rule.Value!, CultureInfo.InvariantCulture));
        }
        else if (field.Type == FilterFieldType.Date)
        {
            DateOnly date;
            if (raw is DateOnly dateOnly) date = dateOnly;
            else if (raw is DateTime dateTime) date = DateOnly.FromDateTime(dateTime);
            else if (raw is DateTimeOffset offset) date = DateOnly.FromDateTime(offset.DateTime);
            else if (!DateOnly.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) return false;
            comparison = date.CompareTo(DateOnly.ParseExact(rule.Value!, "yyyy-MM-dd", CultureInfo.InvariantCulture));
        }
        else comparison = string.Compare(value, rule.Value, StringComparison.OrdinalIgnoreCase);
        return rule.Operator switch
        {
            FilterOperator.Equals => comparison == 0, FilterOperator.NotEquals => comparison != 0,
            FilterOperator.GreaterThan => comparison > 0, FilterOperator.LessThan => comparison < 0,
            FilterOperator.Contains => value.Contains(rule.Value!, StringComparison.OrdinalIgnoreCase),
            FilterOperator.StartsWith => value.StartsWith(rule.Value!, StringComparison.OrdinalIgnoreCase), _ => false
        };
    }
}
