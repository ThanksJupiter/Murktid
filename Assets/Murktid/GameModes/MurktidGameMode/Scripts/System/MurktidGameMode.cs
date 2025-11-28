using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Murktid {

    public class MurktidGameMode : IGameMode {
        private readonly ApplicationData applicationData;

        private IMurktidPlayer player;
        private EnemySystem enemySystem;

        public MurktidGameMode(ApplicationData applicationData) {
            this.applicationData = applicationData;
        }
        public bool IsGameModeInitialized { get; private set; }
        public void EnterGameMode() {
            MurktidGameReference gameModeReference = Object.FindFirstObjectByType<MurktidGameReference>();
            switch(gameModeReference.gameData.playerType) {

                case PlayerType.DefaultStateMachine:
                    InitializeDefaultStateMachine(gameModeReference);
                    break;
                case PlayerType.AbilityStateMachine:
                    InitializeAbilityStateMachine(gameModeReference);
                    break;
                default:
                    break;
            }

            EnemySystemReference enemySystemReference = Object.FindFirstObjectByType<EnemySystemReference>();
            enemySystem = new(enemySystemReference);

            applicationData.cursorHandler.PushState(CursorHandler.CursorState.Locked, this);
            IsGameModeInitialized = true;
        }

        private void InitializeDefaultStateMachine(MurktidGameReference gameModeReference) {

            Vector3 spawnPosition = gameModeReference.GetSpawnPosition();
            Quaternion spawnRotation = gameModeReference.GetSpawnRotation();

            PlayerReference playerReference = Object.Instantiate(gameModeReference.gameData.playerData.playerReferencePrefab, spawnPosition, spawnRotation);
            PlayerCameraReference playerCameraReference = Object.Instantiate(gameModeReference.gameData.playerData.playerCameraReferencePrefab, spawnPosition, spawnRotation);

            PlayerMachine playerMachine = new(playerReference.context) {
                Context = {
                    input = applicationData.Input,
                    cameraReference = playerCameraReference
                }
            };

            player = playerMachine;
            player.Initialize();
        }

        private void InitializeAbilityStateMachine(MurktidGameReference gameModeReference) {

            Vector3 spawnPosition = gameModeReference.GetSpawnPosition();
            Quaternion spawnRotation = gameModeReference.GetSpawnRotation();

            PlayerReference playerReference = Object.Instantiate(gameModeReference.gameData.playerData.playerReferencePrefab, spawnPosition, spawnRotation);
            PlayerCameraReference playerCameraReference = Object.Instantiate(gameModeReference.gameData.playerData.playerCameraReferencePrefab, spawnPosition, spawnRotation);

            PlayerController playerController = new(playerReference.context) {
                Context = {
                    input = applicationData.Input,
                    cameraReference = playerCameraReference
                }
            };

            player = playerController;
            player.Initialize();
        }

        public void Tick() {
            player?.SetInput();
            player?.Tick(Time.deltaTime);
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
