using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Murktid {
    public class MenuApplicationStateData {
        public Action<GameMode> startGameRequests;
    }

    public class MainMenuApplicationState : IApplicationState {
        private readonly ApplicationData applicationData;
        private readonly MenuApplicationStateData menuApplicationStateData;
        private readonly BootstrapSettings bootstrapSettings;
        private MainMenuView mainMenuView;
        private AsyncOperationHandle<SceneInstance> loadSceneAsync;

        public bool IsApplicationStateInitialized { get; set; } = true;

        public MainMenuApplicationState(ApplicationData applicationData,
            MenuApplicationStateData menuApplicationStateData,
            BootstrapSettings bootstrapSettings) {
            this.applicationData = applicationData;
            this.menuApplicationStateData = menuApplicationStateData;
            this.bootstrapSettings = bootstrapSettings;
        }

        public void EnterApplicationState() {
            Addressables.LoadSceneAsync(bootstrapSettings.menuScene, LoadSceneMode.Single).Completed += handle => {
                OnSceneLoaded();
            };
            menuApplicationStateData.startGameRequests += mode => {
                applicationData.ChangeApplicationState(ApplicationState.GameMode);
                applicationData.ChangeGameModeState(mode);
            };

            // create / start character customization functionality
            // save customized character somehow and take it into consideration when spawning character when starting game
            // get access to camera & menu ui
            // create separate customization UI and leave main menu prefab thing alone?
            // but it might be part of same window so perhaps combine? or no because might want to customize elsewhere later
        }
        private void OnSceneLoaded() {
            Addressables.LoadAssetAsync<MenuPrefabsContainer>(bootstrapSettings.menuPrefabsContainer).Completed += OnContainerLoaded;
        }
        private void OnContainerLoaded(AsyncOperationHandle<MenuPrefabsContainer> handle) {
            mainMenuView = new MainMenuView(handle.Result, menuApplicationStateData);
            mainMenuView.Initialize();
        }

        public ApplicationState Tick() {
            mainMenuView?.Tick();
            return applicationData.ActiveApplicationState;
        }
        public void LateTick() { }
        public void Dispose() { }
        public void ExitApplicationState() { }
    }
}
