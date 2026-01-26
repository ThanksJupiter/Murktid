using UnityEngine;
using Object = UnityEngine.Object;

namespace Murktid {

    public class MurktidGameMode : IGameMode {
        private readonly ApplicationData applicationData;

        private IMurktidPlayer player;
        private PlayerController playerController;
        private EnemySystem enemySystem;
        private BulletSystem bulletSystem = new();

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

            enemySystem = new EnemySystem(applicationData.cursorHandler);
            enemySystem.Initialize();

            if(enemySystem != null) {
                enemySystem.slotSystem = playerController.attackerSlotSystem;
            }

            bulletSystem.Initialize(gameModeReference);

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
            player.Initialize(playerReference);
        }

        private void InitializeAbilityStateMachine(MurktidGameReference gameModeReference) {

            Vector3 spawnPosition = gameModeReference.GetSpawnPosition();
            Quaternion spawnRotation = gameModeReference.GetSpawnRotation();

            PlayerReference playerReference = Object.Instantiate(gameModeReference.gameData.playerData.playerReferencePrefab, spawnPosition, spawnRotation);
            PlayerCameraReference playerCameraReference = Object.Instantiate(gameModeReference.gameData.playerData.playerCameraReferencePrefab, spawnPosition, spawnRotation);

            playerController = new(playerReference) {
                Context = {
                    input = applicationData.Input,
                    cameraReference = playerCameraReference,
                },

                bulletSystem = bulletSystem
            };

            player = playerController;
            player.Initialize(playerReference);
        }

        public void Tick() {
            player.SetInput();

            float deltaTime = Time.deltaTime;
            player.Tick(deltaTime);
            bulletSystem.Tick(deltaTime);
            enemySystem.Tick(deltaTime);
        }
        public void LateTick() {

        }
        public void ExitGameMode() {
            applicationData.cursorHandler.ClearInstigator(this);
        }
        public void Dispose() {
            player?.Dispose();
        }
    }
}
