using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Blazor.Rrweb.Replay.Abstract;
using Soenneker.Blazor.Rrweb.Replay.Configuration;

namespace Soenneker.Quark.Suite.Tests;

internal sealed class SessionReplayTestInterop : IRrwebReplayInterop
{
    public List<string> Calls { get; } = [];
    public double Position { get; set; }
    public double Speed { get; private set; } = 1;
    public bool FailCreate { get; set; }
    public TaskCompletionSource? PendingCreate { get; set; }
    public ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public async ValueTask Create(string id, ElementReference element, JsonElement[] events, RrwebReplayOptions? options = null,
        bool useCdn = true, CancellationToken cancellationToken = default)
    {
        Calls.Add("create");
        if (PendingCreate is not null) await PendingCreate.Task;
        if (FailCreate) throw new InvalidOperationException("Invalid recording");
        Position = 0;
    }
    public ValueTask Play(string id, double? timeOffset = null, CancellationToken cancellationToken = default)
    {
        Calls.Add("play");
        if (timeOffset.HasValue) Position = timeOffset.Value;
        return ValueTask.CompletedTask;
    }
    public ValueTask Pause(string id, double? timeOffset = null, CancellationToken cancellationToken = default)
    {
        Calls.Add("pause");
        if (timeOffset.HasValue) Position = timeOffset.Value;
        return ValueTask.CompletedTask;
    }
    public ValueTask SetSpeed(string id, double speed, CancellationToken cancellationToken = default)
    {
        Speed = speed;
        return ValueTask.CompletedTask;
    }
    public ValueTask<double> GetCurrentTime(string id, CancellationToken cancellationToken = default) => ValueTask.FromResult(Position);
    public ValueTask SetFitToContainer(string id, bool enabled, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask<double> GetDuration(string id, CancellationToken cancellationToken = default) => ValueTask.FromResult(10000d);
    public ValueTask AddEvent(string id, JsonElement recordedEvent, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask StartLive(string id, double? baselineTime = null, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask Destroy(string id, CancellationToken cancellationToken = default)
    {
        Calls.Add("destroy");
        return ValueTask.CompletedTask;
    }
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
