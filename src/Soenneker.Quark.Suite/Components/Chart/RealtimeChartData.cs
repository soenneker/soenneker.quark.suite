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
    private readonly Ring<string?> _labels;
    private readonly Ring<DateTimeOffset> _timestamps;
    private readonly LabelList _labelView;
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
        _labels = new Ring<string?>(capacity);
        _timestamps = new Ring<DateTimeOffset>(capacity);
        _labelView = new LabelList(this);
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
    public IReadOnlyList<string> Labels => _labelView;
    /// <summary>Gets retained timestamps as Unix milliseconds for proportional spacing.</summary>
    public IReadOnlyList<double> XValues => _xValues;
    /// <summary>Gets the data revision to bind to Chart.DataVersion.</summary>
    public long Version { get; private set; }

    /// <summary>Appends a sample, evicting the oldest sample when full. Null values create gaps.</summary>
    /// <remarks>Timestamps must increase by at least one millisecond. Supply one finite or null value per series.</remarks>
    public void Append(DateTimeOffset timestamp, params double?[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        Append(timestamp, values.AsSpan());
    }

    /// <summary>Appends a sample without allocating an argument array. Null values create gaps.</summary>
    /// <param name="timestamp">A timestamp later than the last retained sample by at least one millisecond.</param>
    /// <param name="values">One finite or null value per series.</param>
    public void Append(DateTimeOffset timestamp, params ReadOnlySpan<double?> values)
    {
        if (values.Length != _values.Length)
            throw new ArgumentException("Supply one value per series.", nameof(values));
        ValidateTimestamp(timestamp, Count > 0 ? _xValues[Count - 1] : double.NegativeInfinity);
        ValidateValues(values);
        AppendCore(timestamp, values);
        Version++;
    }

    /// <summary>Appends a batch and advances the data version once. Validation completes before any samples are changed.</summary>
    /// <param name="timestamps">Strictly increasing timestamps, later than the last retained sample.</param>
    /// <param name="values">Sample-major values: all series for the first timestamp, then all series for the next.</param>
    /// <remarks>Request one chart render after the batch. Labels are formatted lazily when read.</remarks>
    public void AppendBatch(ReadOnlySpan<DateTimeOffset> timestamps, ReadOnlySpan<double?> values)
    {
        if (values.Length % _values.Length != 0 || values.Length / _values.Length != timestamps.Length)
            throw new ArgumentException("Supply one value per series for every timestamp.", nameof(values));
        if (timestamps.IsEmpty)
            return;

        double previous = Count > 0 ? _xValues[Count - 1] : double.NegativeInfinity;
        foreach (var timestamp in timestamps)
        {
            ValidateTimestamp(timestamp, previous);
            previous = timestamp.ToUnixTimeMilliseconds();
        }
        ValidateValues(values);
        for (var index = 0; index < timestamps.Length; index++)
            AppendCore(timestamps[index], values.Slice(index * _values.Length, _values.Length));
        Version++;
    }

    private static void ValidateTimestamp(DateTimeOffset timestamp, double previous)
    {
        if (timestamp.ToUnixTimeMilliseconds() <= previous)
            throw new ArgumentException("Timestamps must be strictly increasing in milliseconds.", nameof(timestamp));
    }

    private static void ValidateValues(ReadOnlySpan<double?> values)
    {
        foreach (var value in values)
        {
            if (value.HasValue && !double.IsFinite(value.Value))
                throw new ArgumentException("Values must be finite or null.", nameof(values));
        }
    }

    private void AppendCore(DateTimeOffset timestamp, ReadOnlySpan<double?> values)
    {
        _labels.Add(null);
        _timestamps.Add(timestamp);
        _xValues.Add(timestamp.ToUnixTimeMilliseconds());
        for (var index = 0; index < values.Length; index++)
            _values[index].Add(values[index]);
    }

    /// <summary>Removes all samples while preserving series identity and legend state.</summary>
    public void Clear()
    {
        _labels.Clear();
        _timestamps.Clear();
        _xValues.Clear();
        foreach (var values in _values)
            values.Clear();
        Version++;
    }

    private sealed class LabelList(RealtimeChartData owner) : IReadOnlyList<string>
    {
        public int Count => owner.Count;
        public string this[int index] => owner._labels[index] ??=
            owner._timestamps[index].ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);
        public IEnumerator<string> GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
                yield return this[index];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private sealed class Ring<T>(int capacity) : IReadOnlyList<T>
    {
        private readonly T[] _items = new T[capacity];
        private int _start;
        public int Count { get; private set; }
        public T this[int index]
        {
            get => _items[PhysicalIndex(index)];
            set => _items[PhysicalIndex(index)] = value;
        }

        private int PhysicalIndex(int index) => index >= 0 && index < Count
            ? (_start + index) % capacity : throw new ArgumentOutOfRangeException(nameof(index));

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
