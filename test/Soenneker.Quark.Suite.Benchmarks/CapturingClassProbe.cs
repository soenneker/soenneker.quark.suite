using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark.Suite.Benchmarks;

public sealed class CapturingClassProbe : RenderComponent
{
    private readonly Dictionary<string, object> _attributes = new(1);
    public string StateClass { get; set; } = "selected";

    public object WithDelegate()
    {
        _attributes.Clear();
        BuildClassAttribute(_attributes, (ref PooledStringBuilder cls) =>
        {
            AppendClass(ref cls, "flex items-center");
            AppendClass(ref cls, StateClass);
        });
        return _attributes;
    }

    public object WithDirectBuilder()
    {
        _attributes.Clear();
        var cls = new PooledStringBuilder(64);
        try
        {
            AppendClass(ref cls, "flex items-center");
            AppendClass(ref cls, StateClass);
            BuildClassAttribute(_attributes, ref cls);
        }
        finally
        {
            cls.Dispose();
        }
        return _attributes;
    }

    public object WithCombinedDelegate()
    {
        _attributes.Clear();
        BuildClassAndStyleAttributes(_attributes, (ref PooledStringBuilder cls, ref PooledStringBuilder sty) =>
        {
            AppendClass(ref cls, StateClass);
            AppendStyleDecl(ref sty, "opacity: 1");
        });
        return _attributes;
    }

    public object WithStackBuilder()
    {
        _attributes.Clear();
        var cls = new PooledStringBuilder(stackalloc char[64]);
        try
        {
            AppendClass(ref cls, "flex items-center");
            AppendClass(ref cls, StateClass);
            BuildClassAttribute(_attributes, ref cls);
        }
        finally
        {
            cls.Dispose();
        }
        return _attributes;
    }

    public object WithCombinedDirectBuilder()
    {
        _attributes.Clear();
        var cls = new PooledStringBuilder(64);
        var sty = new PooledStringBuilder(64);
        try
        {
            AppendClass(ref cls, StateClass);
            AppendStyleDecl(ref sty, "opacity: 1");
            BuildClassAndStyleAttributes(_attributes, ref cls, ref sty);
        }
        finally
        {
            cls.Dispose();
            sty.Dispose();
        }
        return _attributes;
    }
}
