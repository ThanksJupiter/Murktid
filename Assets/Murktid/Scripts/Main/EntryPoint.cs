using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Murktid {

    public class EntryPoint : MonoBehaviour {

        public AssetReferenceT<InitializerSettingsFile> initializerSettingsFile;

        // Globals
        private IPlatform platform;
        private InputSettings input;
        private ApplicationData applicationData;
        private Dictionary<ApplicationState, IApplicationState> applicationStates;

        // Settings
        private InitializerSettingsFile initializerSettings;
        private BootstrapSettings bootstrapSettings;
        private MenuApplicationStateData menuApplicationStateData;

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize() {
#if UNITY_EDITOR
            if(BootMode.BootType == BootType.UnityDefault) {
                return;
            }
#endif
            EntryPoint entryPoint = FindFirstObjectByType<EntryPoint>(FindObjectsInactive.Include);
            if(entryPoint == null) {
                AsyncOperationHandle<GameObject> handler = Addressables.InstantiateAsync("Initializer");
            }
        }

        public IEnumerator Start() {
            DontDestroyOnLoad(this);

            SceneReference activeSceneReference = FindFirstObjectByType<SceneReference>();
            if(activeSceneReference == null) {
                Debug.LogWarning("The scene has no SceneReference script. Will start by default in Splash Application State");
                GameObject sceneRef = new GameObject() {
                    name = "Scene Reference"
                };
                activeSceneReference = sceneRef.AddComponent<SceneReference>();
                activeSceneReference.applicationState = ApplicationState.Splash;
                yield return null;
            }

            applicationData = new ApplicationData {
                platformSelector = new PlatformSelector(PlatformSelector.GetDevicePlatform(), PlatformSelector.GetPlatformDefaultInputMode()),
            };
            applicationData.ChangeApplicationState(activeSceneReference.applicationState);

            AsyncOperationHandle<InitializerSettingsFile> initSettingsHandle = Addressables.LoadAssetAsync<InitializerSettingsFile>(initializerSettingsFile);
            yield return new WaitUntil(() => initSettingsHandle.IsDone);
            initializerSettings = initSettingsHandle.Result;

            yield return CreatePlatformFactory();

            AsyncOperationHandle<BootstrapSettings> bootstrapSettingsHandle = Addressables.LoadAssetAsync<BootstrapSettings>(initializerSettings.bootstrapAssetReference);
            yield return new WaitUntil(() => bootstrapSettingsHandle.IsDone);
            bootstrapSettings = bootstrapSettingsHandle.Result;

            menuApplicationStateData = new MenuApplicationStateData();
            CreateApplicationStates();
            ApplicationStateRunner applicationStateRunner = gameObject.AddComponent<ApplicationStateRunner>();
            applicationStateRunner.Initialize(applicationStates, applicationData, platform);
        }

        private IEnumerator CreatePlatformFactory() {
            platform = null;
            DevicePlatform devicePlatform = applicationData.platformSelector.devicePlatform;
            platform = devicePlatform switch {
                DevicePlatform.Desktop => new DesktopPlatform(initializerSettings.desktopPlatformSettings),
                //DevicePlatform.Android => new AndroidPlatform(initializerSettings.androidPlatformSettings),
                _ => platform
            };

            if(!Equals(applicationData.platformSelector.devicePlatform, DevicePlatform.Desktop) &&
                PlatformSelector.GetPlatformDefaultInputMode() == InputMode.Desktop) {
                platform = new DesktopPlatform(initializerSettings.desktopPlatformSettings);
            }
            yield return platform?.Initialize(applicationData);
        }

        private void CreateApplicationStates() {
            applicationStates = new Dictionary<ApplicationState, IApplicationState> {
                [ApplicationState.Splash] = new SplashApplicationState(bootstrapSettings, applicationData),
                [ApplicationState.MainMenu] = new MainMenuApplicationState(applicationData, menuApplicationStateData, bootstrapSettings),
                [ApplicationState.GameMode] = new GameModeApplicationState(applicationData, bootstrapSettings.gameModeSettings),
            };
        }
    }
}
