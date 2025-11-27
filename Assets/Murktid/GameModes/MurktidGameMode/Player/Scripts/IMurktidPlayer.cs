using Murktid;
using UnityEngine;

public interface IMurktidPlayer
{
    public PlayerContext Context { get; }

    void Initialize();
    void SetInput();
    void Tick(float deltaTime);
}
