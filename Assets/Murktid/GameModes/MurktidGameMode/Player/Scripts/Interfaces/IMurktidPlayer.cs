using Murktid;
using UnityEngine;

public interface IMurktidPlayer
{
    public PlayerContext Context { get; }

    void Initialize(PlayerReference playerReference);
    void SetInput();
    void Tick(float deltaTime);
    void Dispose();
}
