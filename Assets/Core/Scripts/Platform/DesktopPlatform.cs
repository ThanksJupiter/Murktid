using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Murktid {
    public class DesktopPlatform : IPlatform {
        private readonly IPlatform platform;
        private readonly AssetReference assetReference;

        private DesktopPlatformSettings desktopPlatformSettings;
        private IApplicationLifecycle input;

        public DesktopPlatform(AssetReference assetReference) {
            this.assetReference = assetReference;
        }

        public IEnumerator Initialize(ApplicationData applicationData) {
            if(platform != null) {
                yield return platform.Initialize(applicationData);
            }

            AsyncOperationHandle<DesktopPlatformSettings> handle = Addressables.LoadAssetAsync<DesktopPlatformSettings>(assetReference);
            yield return new WaitUntil(() => handle.IsDone);
            desktopPlatformSettings = handle.Result;
            applicationData.ChangeInput((IInput)InputHandler());
            Debug.Log($"Device Platform {desktopPlatformSettings.devicePlatform} initialized");
        }

        public IApplicationLifecycle InputHandler() {
            DesktopInput inputHandler = new DesktopInput((DesktopInputSettings)desktopPlatformSettings.inputSettings);
            inputHandler.Initialize();
            input = inputHandler;
            return inputHandler;
        }

        public void Tick() {
            input?.Tick();
        }
        public void LateTick() {
            input?.LateTick();
        }
        public void Dispose() {
            input?.Dispose();
        }
        public void OnApplicationQuit() { }
    }
}
