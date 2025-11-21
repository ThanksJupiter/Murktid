using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Murktid {

    public class GameModeApplicationState : IApplicationState {
        private readonly ApplicationData applicationData;
        private readonly GameModeSettings gameModeSettings;
        private IGameMode gameMode;

        public bool IsApplicationStateInitialized { get; private set; }

        public GameModeApplicationState(ApplicationData applicationData, GameModeSettings gameModeSettings) {
            this.applicationData = applicationData;
            this.gameModeSettings = gameModeSettings;
        }

        public void EnterApplicationState() {
            if(applicationData.ActiveGameMode == GameMode.Invalid) {
                SceneReference sceneReference = Object.FindObjectOfType<SceneReference>();
                if(gameModeSettings.gameModeData.Any(g => g.gameMode == sceneReference.gameMode)) {
                    applicationData.ChangeGameModeState(sceneReference.gameMode);
                }
                InitializeGameMode();
                return;
            }
            GameModeData gameModeData = gameModeSettings.gameModeData.FirstOrDefault(g => g.gameMode == applicationData.ActiveGameMode);
            if(gameModeData != null) {
                Addressables.LoadSceneAsync(gameModeData.scene, LoadSceneMode.Single).Completed += handle => {
                    InitializeGameMode();
                };
            }
        }
        private void InitializeGameMode() {
            switch(applicationData.ActiveGameMode) {
                case GameMode.Murktid:
                    gameMode = new MurktidGameMode(applicationData);
                    break;
            }
            if(gameMode != null) {
                gameMode.EnterGameMode();
            }
            IsApplicationStateInitialized = true;
        }

        public ApplicationState Tick() {
            if(gameMode.IsGameModeInitialized) {
                gameMode.Tick();
            }
            return ApplicationState.GameMode;
        }

        public void LateTick() {
            if(gameMode.IsGameModeInitialized) {
                gameMode.LateTick();
            }
        }

        public void Dispose() { }

        public void ExitApplicationState() {
            gameMode.ExitGameMode();
            gameMode.Dispose();
            applicationData.ChangeGameModeState(GameMode.Invalid);
        }
    }
}
