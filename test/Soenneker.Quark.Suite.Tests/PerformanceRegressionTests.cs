using System;
using System.Linq;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class PerformanceRegressionTests
{
    [Test]
    public void Compiled_filters_short_circuit_and_preserve_empty_group_semantics()
    {
        FilterField[] fields = [new("number", "Number", FilterFieldType.Number)];
        var group = new FilterGroup { Rules = Enumerable.Range(0, 10).Select(_ => new FilterRule { Field = "number", Operator = FilterOperator.GreaterThan, Value = "10" }).ToArray() };
        int calls = 0;
        var predicate = FilterEvaluator.Compile<int>(group, fields, (item, _) => { calls++; return item; });
        predicate(0).Should().BeFalse(); calls.Should().Be(1);
        predicate(11).Should().BeTrue(); calls.Should().Be(11);
        FilterEvaluator.Compile<int>(new FilterGroup { Junction = FilterJunction.Any }, fields, (_, _) => throw new Exception())(0).Should().BeTrue();
        FilterEvaluator.Compile<int>(new FilterGroup { Junction = FilterJunction.Any, Rules = group.Rules }, fields, (_, _) => 11)(0).Should().BeTrue();
    }

    [Test]
    public void Builder_cache_invalidates_on_new_values_but_not_pending_modifiers()
    {
        var builder = Width.Is1.OnMd.Is2;
        string first = builder.ToClass();
        ReferenceEquals(first, builder.ToClass()).Should().BeTrue();
        var pending = builder.OnLg;
        pending.ToClass().Should().Be(first);
        var changed = pending.Is3;
        changed.ToClass().Should().Be("w-1 md:w-2 lg:w-3");
        ReferenceEquals(first, changed.ToClass()).Should().BeFalse();
    }

    [Test]
    public void Scheduler_reuses_lowest_available_column_and_sizes_entire_overlap_group()
    {
        var day = new DateOnly(2026, 10, 18);
        var start = new DateTimeOffset(2026, 10, 18, 9, 0, 0, TimeSpan.Zero);
        var events = new[] {
            new SchedulerEvent("a", "A", start, start.AddMinutes(60)),
            new SchedulerEvent("b", "B", start, start.AddMinutes(120)),
            new SchedulerEvent("c", "C", start.AddMinutes(60), start.AddMinutes(90))
        };
        var result = SchedulerLayout.Arrange(events, day, TimeZoneInfo.Utc);
        result.Single(p => p.Event.Id == "a").Column.Should().Be(1);
        result.Single(p => p.Event.Id == "c").Column.Should().Be(1);
        result.Should().OnlyContain(p => p.ColumnCount == 2);
        var dense = SchedulerLayout.Arrange(Enumerable.Range(0, 10000).Select(i => new SchedulerEvent(i.ToString(), "", start, start.AddHours(1))), day, TimeZoneInfo.Utc);
        dense.Select(p => p.Column).Distinct().Count().Should().Be(10000);
        dense.Should().OnlyContain(p => p.ColumnCount == 10000);
    }
}
