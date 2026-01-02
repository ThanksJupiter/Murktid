using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Murktid {

    public class MurktidGameMode : IGameMode {
        private readonly ApplicationData applicationData;

        private IMurktidPlayer player;
        private EnemySystem enemySystem = new();
        private BulletSystem bulletSystem = new();

        private NetcodeTestManager netcodeTestManager;

        public MurktidGameMode(ApplicationData applicationData) {
            this.applicationData = applicationData;
        }
        public bool IsGameModeInitialized { get; private set; }
        public void EnterGameMode() {

            netcodeTestManager = Object.FindFirstObjectByType<NetcodeTestManager>();
            netcodeTestManager.hostButton.clicked += StartHost;
            netcodeTestManager.clientButton.clicked += StartClient;
        }

        private void StartHost() {
            Debug.Log($"start host :)");

            NetworkManager.Singleton.StartHost();
            Object.Destroy(Object.FindFirstObjectByType<Camera>().gameObject);
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

            enemySystem?.Initialize();
            bulletSystem.Initialize(gameModeReference);

            applicationData.cursorHandler.PushState(CursorHandler.CursorState.Locked, this);
            IsGameModeInitialized = true;
        }

        private void StartClient() {
            Debug.Log($"start cliente :o");
            NetworkManager.Singleton.StartClient();
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

            NetworkObject playerNetworkObject = NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(gameModeReference.gameData.playerData.playerNetworkObjectPrefab, isPlayerObject: true, position: spawnPosition, rotation: spawnRotation);

            PlayerReference playerReference = playerNetworkObject.GetComponent<PlayerReference>();
            //PlayerReference playerReference = Object.Instantiate(gameModeReference.gameData.playerData.playerReferencePrefab, spawnPosition, spawnRotation);
            PlayerCameraReference playerCameraReference = Object.Instantiate(gameModeReference.gameData.playerData.playerCameraReferencePrefab, spawnPosition, spawnRotation);

            PlayerController playerController = new(playerReference) {
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

            // TODO not this
            if(!NetworkManager.Singleton) {
                return;
            }

            if(!NetworkManager.Singleton.IsHost) {
                return;
            }

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

            netcodeTestManager.hostButton.clicked -= StartHost;
            netcodeTestManager.clientButton.clicked -= StartClient;
        }
    }
}
