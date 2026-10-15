using System;
using System.Threading.Tasks;

namespace Soenneker.Quark;

internal sealed class ComparisonChartRowRenderState
{
    private readonly ComparisonChartRow _row;
    private readonly Func<ComparisonChartRow, Task> _select;
    private ComparisonChartCellStyleCache[] _cells = [];

    internal Func<Task> Click { get; }

    internal ComparisonChartCellStyleCache[] Cells => _cells;

    internal ComparisonChartRowRenderState(ComparisonChartRow row, Func<ComparisonChartRow, Task> select)
    {
        _row = row;
        _select = select;
        Click = Select;
    }

    internal void Resize(int count)
    {
        if (_cells.Length != count)
            Array.Resize(ref _cells, count);
    }

    private Task Select() => _select(_row);
}
