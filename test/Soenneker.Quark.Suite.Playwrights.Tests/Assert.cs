using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

internal static class Assert
{
    public static void True(bool condition, string? message = null)
    {
        if (!condition)
            throw new Exception(message ?? "Expected condition to be true.");
    }

    public static void False(bool condition, string? message = null)
    {
        if (condition)
            throw new Exception(message ?? "Expected condition to be false.");
    }

    public static T NotNull<T>(T? value, string? message = null)
    {
        if (value is null)
            throw new Exception(message ?? "Expected value to be non-null.");

        return value;
    }

    public static void Null(object? value, string? message = null)
    {
        if (value is not null)
            throw new Exception(message ?? "Expected value to be null.");
    }

    public static void Equal<T>(T expected, T actual, string? message = null)
    {
        if (expected is IEnumerable expectedEnumerable && actual is IEnumerable actualEnumerable && expected is not string && actual is not string)
        {
            var expectedItems = expectedEnumerable.Cast<object?>().ToArray();
            var actualItems = actualEnumerable.Cast<object?>().ToArray();

            if (expectedItems.SequenceEqual(actualItems))
                return;
        }
        else if (EqualityComparer<T>.Default.Equals(expected, actual))
        {
            return;
        }

        throw new Exception(message ?? $"Expected '{expected}' but found '{actual}'.");
    }

    public static void NotEqual<T>(T notExpected, T actual, string? message = null)
    {
        if (EqualityComparer<T>.Default.Equals(notExpected, actual))
            throw new Exception(message ?? $"Did not expect '{actual}'.");
    }

    public static void InRange<T>(T actual, T low, T high, string? message = null) where T : IComparable<T>
    {
        if (actual.CompareTo(low) < 0 || actual.CompareTo(high) > 0)
            throw new Exception(message ?? $"Expected '{actual}' to be between '{low}' and '{high}'.");
    }

    public static void DoesNotContain(string expectedSubstring, string actualString, StringComparison comparison, string? message = null)
    {
        if (actualString.Contains(expectedSubstring, comparison))
            throw new Exception(message ?? $"Did not expect '{expectedSubstring}' in '{actualString}'.");
    }
}
