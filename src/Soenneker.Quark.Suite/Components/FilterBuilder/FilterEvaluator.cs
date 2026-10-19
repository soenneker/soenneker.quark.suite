using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
namespace Soenneker.Quark;
/// <summary>Evaluates filter state locally without reflection. Server applications can translate the same DTOs to their query language.</summary>
public static class FilterEvaluator
{
    private static readonly IReadOnlyList<FilterOperator> TextOperators = Array.AsReadOnly(new[] { FilterOperator.Equals, FilterOperator.NotEquals, FilterOperator.Contains, FilterOperator.StartsWith, FilterOperator.IsEmpty, FilterOperator.IsNotEmpty });
    private static readonly IReadOnlyList<FilterOperator> OrderedOperators = Array.AsReadOnly(new[] { FilterOperator.Equals, FilterOperator.NotEquals, FilterOperator.GreaterThan, FilterOperator.LessThan, FilterOperator.IsEmpty, FilterOperator.IsNotEmpty });
    private static readonly IReadOnlyList<FilterOperator> ValueOperators = Array.AsReadOnly(new[] { FilterOperator.Equals, FilterOperator.NotEquals, FilterOperator.IsEmpty, FilterOperator.IsNotEmpty });
    public static IReadOnlyList<FilterOperator> Operators(FilterFieldType type) => type switch
    {
        FilterFieldType.Text => TextOperators,
        FilterFieldType.Number or FilterFieldType.Date => OrderedOperators,
        _ => ValueOperators
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
        bool all = group.Junction == FilterJunction.All;
        foreach (var rule in group.Rules)
        {
            FilterField? field = null;
            foreach (var candidate in fields)
                if (candidate.Key == rule.Field) { field = candidate; break; }
            bool result = field is not null && Match(rule, field, getValue(rule.Field));
            if (result != all) return result;
        }
        foreach (var child in group.Groups)
        {
            bool result = Matches(child, fields, getValue);
            if (result != all) return result;
        }
        return all || (group.Rules.Count == 0 && group.Groups.Count == 0);
    }

    /// <summary>Snapshots a filter into a reusable, short-circuiting predicate. Recompile when filter rules or field definitions change.</summary>
    public static Func<T, bool> Compile<T>(FilterGroup group, IReadOnlyList<FilterField> fields, Func<T, string, object?> getValue)
    {
        if (group.Rules.Count == 0 && group.Groups.Count == 0) return static _ => true;
        var predicates = new Func<T, bool>[group.Rules.Count + group.Groups.Count];
        int index = 0;
        foreach (var rule in group.Rules)
        {
            var field = fields.FirstOrDefault(f => f.Key == rule.Field);
            if (field is null || !IsValid(rule, field)) { predicates[index++] = static _ => false; continue; }
            decimal number = field.Type == FilterFieldType.Number && rule.Operator is not (FilterOperator.IsEmpty or FilterOperator.IsNotEmpty)
                ? decimal.Parse(rule.Value!, CultureInfo.InvariantCulture) : default;
            DateOnly date = field.Type == FilterFieldType.Date && rule.Operator is not (FilterOperator.IsEmpty or FilterOperator.IsNotEmpty)
                ? DateOnly.ParseExact(rule.Value!, "yyyy-MM-dd", CultureInfo.InvariantCulture) : default;
            predicates[index++] = item => MatchValidated(rule, field, getValue(item, rule.Field), number, date);
        }
        foreach (var child in group.Groups) predicates[index++] = Compile(child, fields, getValue);
        bool all = group.Junction == FilterJunction.All;
        return item =>
        {
            foreach (var predicate in predicates)
            {
                bool result = predicate(item);
                if (result != all) return result;
            }
            return all;
        };
    }
    private static bool Match(FilterRule rule, FilterField field, object? raw)
    {
        if (!IsValid(rule, field)) return false;
        decimal number = field.Type == FilterFieldType.Number && rule.Operator is not (FilterOperator.IsEmpty or FilterOperator.IsNotEmpty)
            ? decimal.Parse(rule.Value!, CultureInfo.InvariantCulture) : default;
        DateOnly date = field.Type == FilterFieldType.Date && rule.Operator is not (FilterOperator.IsEmpty or FilterOperator.IsNotEmpty)
            ? DateOnly.ParseExact(rule.Value!, "yyyy-MM-dd", CultureInfo.InvariantCulture) : default;
        return MatchValidated(rule, field, raw, number, date);
    }
    private static bool MatchValidated(FilterRule rule, FilterField field, object? raw, decimal expectedNumber, DateOnly expectedDate)
    {
        var value = Convert.ToString(raw, CultureInfo.InvariantCulture);
        if (rule.Operator == FilterOperator.IsEmpty) return string.IsNullOrEmpty(value);
        if (rule.Operator == FilterOperator.IsNotEmpty) return !string.IsNullOrEmpty(value);
        if (value is null) return false;
        int comparison;
        if (field.Type == FilterFieldType.Number)
        {
            if (!decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var number)) return false;
            comparison = number.CompareTo(expectedNumber);
        }
        else if (field.Type == FilterFieldType.Date)
        {
            DateOnly date;
            if (raw is DateOnly dateOnly) date = dateOnly;
            else if (raw is DateTime dateTime) date = DateOnly.FromDateTime(dateTime);
            else if (raw is DateTimeOffset offset) date = DateOnly.FromDateTime(offset.DateTime);
            else if (!DateOnly.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) return false;
            comparison = date.CompareTo(expectedDate);
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
