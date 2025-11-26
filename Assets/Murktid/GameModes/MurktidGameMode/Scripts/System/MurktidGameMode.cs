using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Murktid {
    public class MurktidGameMode : IGameMode {
        private readonly ApplicationData applicationData;
        private PlayerMachine playerMachine;

        public MurktidGameMode(ApplicationData applicationData) {
            this.applicationData = applicationData;
        }
        public bool IsGameModeInitialized { get; private set; }
        public void EnterGameMode() {
            MurktidGameReference gameModeReference = Object.FindFirstObjectByType<MurktidGameReference>();
            PlayerReference playerReference = Object.Instantiate(gameModeReference.gameData.playerData.playerReferencePrefab);
            PlayerCamera playerCamera = Object.Instantiate(gameModeReference.gameData.playerData.playerCameraPrefab);
            playerMachine = new PlayerMachine(playerReference, applicationData);
            playerMachine.context.input = applicationData.Input;
            playerMachine.context.camera = playerCamera;
            playerMachine.Initialize();

            applicationData.cursorHandler.PushState(CursorHandler.CursorState.Locked, this);
            IsGameModeInitialized = true;
        }
        public void Tick() {
            playerMachine.SetInput();
            playerMachine.Tick();
        }
        public void LateTick() {

        }
        public void ExitGameMode() {
            applicationData.cursorHandler.ClearInstigator(this);
        }
        public void Dispose() {

        }
    }
}
