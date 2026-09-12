using System;
using System.Collections.Generic;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class ResultAllocationTests
{
    [Test]
    public void Chart_series_snapshots_non_nullable_values_and_retains_nullable_views()
    {
        var input = new List<double> { 2, 4, 6 };
        var series = new ChartSeries("Values", input);
        input[0] = 99;
        series.Values.Should().Equal(new double?[] { 2, 4, 6 });
        new ChartSeries("Empty", Array.Empty<double>()).Values.Should().BeEmpty();

        var nullable = new List<double?> { 1, null };
        var view = new ChartSeries("View", nullable);
        nullable[0] = 7;
        view.Values.Should().BeSameAs(nullable);
        view.Values[0].Should().Be(7);
    }

    [Test]
    public void Validation_results_preserve_error_details_across_reused_successes()
    {
        ValidationResult.Success().Should().BeSameAs(ValidationResult.Success());
        ValidationResult.None().Should().BeSameAs(ValidationResult.None());
        var combined = ValidationResult.Combine(ValidationResult.Success(),
            ValidationResult.Error("First", new[] { "Name", "Shared" }),
            ValidationResult.Error("Second", new[] { "Shared", "Email" }));
        combined.Status.Should().Be(ValidationStatus.Error);
        combined.ErrorText.Should().Be("First Second");
        combined.MemberNames.Should().Equal("Name", "Shared", "Email");
        ValidationResult.Success().ErrorText.Should().BeNull();
        ValidationResult.Success().MemberNames.Should().BeNull();
        ValidationResult.Combine(ValidationResult.None(), ValidationResult.Success()).Status.Should().Be(ValidationStatus.Success);
        ValidationResult.Combine().Status.Should().Be(ValidationStatus.None);
    }
}
