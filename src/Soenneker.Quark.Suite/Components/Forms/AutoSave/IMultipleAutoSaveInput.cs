using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>Autosave callbacks for controls supporting single and multiple selections.</summary>
public interface IMultipleAutoSaveInput<TValue, TValues> : IAutoSaveInput<TValue>
{
    /// <summary>Persists a snapshot of the selected values in multiple mode.</summary>
    Func<TValues, CancellationToken, ValueTask>? OnAutoSaveMultiple { get; set; }
}
