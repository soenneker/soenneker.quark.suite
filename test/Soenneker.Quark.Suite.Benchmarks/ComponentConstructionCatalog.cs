using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Benchmarks;

internal static class ComponentConstructionCatalog
{
    public static void Measure(string output)
    {
        var rows = new List<object>();
        foreach (var assembly in new[] { typeof(Soenneker.Quark.Div).Assembly, typeof(Soenneker.Bradix.BradixSeparator).Assembly })
        foreach (var definition in assembly.GetTypes().Where(t => !t.IsAbstract && typeof(Microsoft.AspNetCore.Components.IComponent).IsAssignableFrom(t)).OrderBy(t => t.FullName))
        {
            Type type = definition;
            string? error = null;
            try
            {
                if (type.ContainsGenericParameters)
                {
                    Type? closed = null;
                    foreach (var candidate in new[] { typeof(string), typeof(int), typeof(object), typeof(DayOfWeek) })
                    {
                        try { closed = type.MakeGenericType(Enumerable.Repeat(candidate, type.GetGenericArguments().Length).ToArray()); break; }
                        catch (ArgumentException) { }
                    }
                    type = closed ?? throw new InvalidOperationException("Generic constraints need a custom fixture");
                }
                for (var i = 0; i < 100; i++) GC.KeepAlive(Activator.CreateInstance(type, nonPublic: true));
                long before = GC.GetAllocatedBytesForCurrentThread();
                for (var i = 0; i < 200; i++) GC.KeepAlive(Activator.CreateInstance(type, nonPublic: true));
                long bytes = (GC.GetAllocatedBytesForCurrentThread() - before) / 200;
                var instance = Activator.CreateInstance(type, nonPublic: true);
                var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly);
                rows.Add(new { component = definition.FullName, closedType = type.FullName, bytes, ownFields = fields.Length,
                    initializedReferences = fields.Where(f => !f.FieldType.IsValueType && f.GetValue(instance) is not null).Select(f => new { name = f.Name, type = f.FieldType.FullName }).ToArray(), error });
            }
            catch (Exception ex) { rows.Add(new { component = definition.FullName, error = ex.GetBaseException().Message }); }
        }
        File.WriteAllText(output, JsonSerializer.Serialize(rows, new JsonSerializerOptions { WriteIndented = true }));
        Console.WriteLine($"Inventoried {rows.Count} concrete components: {output}");
    }
}
