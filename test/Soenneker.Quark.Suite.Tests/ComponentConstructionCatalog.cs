using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Tests;

internal static class ComponentConstructionCatalog
{
    public static IEnumerable<Type> Types => new[] { typeof(Div).Assembly, typeof(BradixSeparator).Assembly }
        .SelectMany(a => a.GetTypes()).Where(t => !t.IsAbstract && typeof(Microsoft.AspNetCore.Components.IComponent).IsAssignableFrom(t)).OrderBy(t => t.FullName);

    public static Type Close(Type type)
    {
        if (!type.ContainsGenericParameters) return type;
        foreach (Type candidate in new[] { typeof(string), typeof(int), typeof(object), typeof(DayOfWeek) })
        {
            try { return type.MakeGenericType(Enumerable.Repeat(candidate, type.GetGenericArguments().Length).ToArray()); }
            catch (ArgumentException) { }
        }
        throw new InvalidOperationException($"Add a representative generic argument for {type.FullName}.");
    }

    public static long Measure(Type type)
    {
        for (var i = 0; i < 100; i++) GC.KeepAlive(Activator.CreateInstance(type, nonPublic: true));
        long before = GC.GetAllocatedBytesForCurrentThread();
        for (var i = 0; i < 100; i++) GC.KeepAlive(Activator.CreateInstance(type, nonPublic: true));
        return (GC.GetAllocatedBytesForCurrentThread() - before) / 100;
    }
}
