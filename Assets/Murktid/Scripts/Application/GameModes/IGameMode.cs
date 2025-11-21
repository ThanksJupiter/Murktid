using UnityEngine;

namespace Murktid {

    public enum GameMode {
        Invalid,
        AutoZombie
    }

    public interface IGameMode {
        bool IsGameModeInitialized { get; }

        public void EnterGameMode();
        public void Tick();
        public void LateTick();
        void ExitGameMode();
        void Dispose();
    }
}
