namespace Soenneker.Quark.Suite.Benchmarks;

public sealed class ClassListProbe : RenderComponent
{
    private readonly Dictionary<string, object> _attributes = new(1);

    public object Build()
    {
        _attributes["class"] = "consumer-class";
        AppendClassAttribute(_attributes, "flex", "items-center", "gap-2");
        return _attributes;
    }
}
