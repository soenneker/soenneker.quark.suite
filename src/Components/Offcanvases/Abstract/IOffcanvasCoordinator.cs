using System;

namespace Soenneker.Quark;

public interface IOffcanvasCoordinator
{
    int ActiveCount { get; }
    event Action? StateChanged;

    void Register(string id);
    void Unregister(string id);
    void Enter(string id);
    void Exit(string id);
}


