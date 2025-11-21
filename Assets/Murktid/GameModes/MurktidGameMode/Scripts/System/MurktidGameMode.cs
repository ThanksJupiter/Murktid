using UnityEngine;

namespace Murktid {
    public class MurktidGameMode : IGameMode {
        private readonly ApplicationData applicationData;

        public MurktidGameMode(ApplicationData applicationData) {
            this.applicationData = applicationData;
        }
        public bool IsGameModeInitialized { get; private set; }
        public void EnterGameMode() {

        }
        public void Tick() {

        }
        public void LateTick() {

        }
        public void ExitGameMode() {

        }
        public void Dispose() {

        }
    }
}
