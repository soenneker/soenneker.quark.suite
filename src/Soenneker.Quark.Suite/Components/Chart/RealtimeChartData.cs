using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Soenneker.Quark;

/// <summary>A bounded, timestamp-ordered buffer for realtime line charts with one or more synchronized series.</summary>
/// <remarks>
/// Bind <see cref="Series"/>, <see cref="Labels"/>, <see cref="XValues"/>, and <see cref="Version"/> to the matching
/// chart parameters (Version binds to Chart.DataVersion). Append on the Blazor dispatcher and request a render after a sample or batch.
/// This buffer is not thread-safe. Disable ChartOptions.Animate for frequently updated charts.
/// </remarks>
public sealed class RealtimeChartData
{
    private readonly Ring<string> _labels;
    private readonly Ring<double> _xValues;
    private readonly Ring<double?>[] _values;

    /// <summary>Creates a rolling buffer retaining at most <paramref name="capacity"/> samples per named series.</summary>
    public RealtimeChartData(int capacity, params string[] seriesNames)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 2);
        ArgumentNullException.ThrowIfNull(seriesNames);
        if (seriesNames.Length == 0)
            throw new ArgumentException("At least one series is required.", nameof(seriesNames));

        Capacity = capacity;
        _labels = new Ring<string>(capacity);
        _xValues = new Ring<double>(capacity);
        _values = new Ring<double?>[seriesNames.Length];
        var series = new ChartSeries[seriesNames.Length];
        for (var index = 0; index < seriesNames.Length; index++)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(seriesNames[index]);
            _values[index] = new Ring<double?>(capacity);
            series[index] = new ChartSeries(seriesNames[index], _values[index]);
        }
        Series = Array.AsReadOnly(series);
    }

    /// <summary>Gets the maximum number of retained samples per series.</summary>
    public int Capacity { get; }
    /// <summary>Gets the current sample count.</summary>
    public int Count => _xValues.Count;
    /// <summary>Gets stable series collections in constructor order.</summary>
    public IReadOnlyList<ChartSeries> Series { get; }
    /// <summary>Gets the display labels for retained samples.</summary>
    public IReadOnlyList<string> Labels => _labels;
    /// <summary>Gets retained timestamps as Unix milliseconds for proportional spacing.</summary>
    public IReadOnlyList<double> XValues => _xValues;
    /// <summary>Gets the data revision to bind to Chart.DataVersion.</summary>
    public long Version { get; private set; }

    /// <summary>Appends a sample, evicting the oldest sample when full. Null values create gaps.</summary>
    /// <remarks>Timestamps must increase by at least one millisecond. Supply one finite or null value per series.</remarks>
    public void Append(DateTimeOffset timestamp, params double?[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        if (values.Length != _values.Length)
            throw new ArgumentException("Supply one value per series.", nameof(values));
        var x = timestamp.ToUnixTimeMilliseconds();
        if (Count > 0 && x <= _xValues[Count - 1])
            throw new ArgumentException("Timestamps must be strictly increasing in milliseconds.", nameof(timestamp));
        foreach (var value in values)
        {
            if (value.HasValue && !double.IsFinite(value.Value))
                throw new ArgumentException("Values must be finite or null.", nameof(values));
        }

        _labels.Add(timestamp.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture));
        _xValues.Add(x);
        for (var index = 0; index < values.Length; index++)
            _values[index].Add(values[index]);
        Version++;
    }

    /// <summary>Removes all samples while preserving series identity and legend state.</summary>
    public void Clear()
    {
        _labels.Clear();
        _xValues.Clear();
        foreach (var values in _values)
            values.Clear();
        Version++;
    }

    private sealed class Ring<T>(int capacity) : IReadOnlyList<T>
    {
        private readonly T[] _items = new T[capacity];
        private int _start;
        public int Count { get; private set; }
        public T this[int index] => index >= 0 && index < Count ? _items[(_start + index) % capacity] : throw new ArgumentOutOfRangeException(nameof(index));

        public void Add(T value)
        {
            _items[(_start + Count) % capacity] = value;
            if (Count == capacity)
                _start = (_start + 1) % capacity;
            else
                Count++;
        }

        public void Clear()
        {
            Array.Clear(_items);
            _start = 0;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
                yield return this[index];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
