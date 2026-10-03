using System;
using System.Globalization;

namespace Soenneker.Quark;

public partial class NodeEditor
{
    private struct HandleStyleCache
    {
        private long _xBits;
        private long _yBits;
        private string? _style;

        internal string Get(NodeEditorAddHandleModel handle)
        {
            var xBits = BitConverter.DoubleToInt64Bits(handle.X);
            var yBits = BitConverter.DoubleToInt64Bits(handle.Y);
            if (_style is null || _xBits != xBits || _yBits != yBits)
            {
                _style = string.Create(CultureInfo.InvariantCulture, $"transform: translate3d({handle.X:0.###}px, {handle.Y:0.###}px, 0) translate(-50%, -50%)");
                _xBits = xBits;
                _yBits = yBits;
            }
            return _style;
        }
    }
}
