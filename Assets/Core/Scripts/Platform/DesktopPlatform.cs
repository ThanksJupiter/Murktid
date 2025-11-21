using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Murktid {
    public class DesktopPlatform : IPlatform {
        private readonly IPlatform platform;
        private readonly AssetReference assetReference;

        private DesktopPlatformSettings desktopPlatformSettings;

        public DesktopPlatform(AssetReference assetReference) {
            this.assetReference = assetReference;
        }

        public IEnumerator Initialize(object applicationData) {
            if(platform != null) {
                yield return platform.Initialize(applicationData);
            }

            AsyncOperationHandle<DesktopPlatformSettings> handle = Addressables.LoadAssetAsync<DesktopPlatformSettings>(assetReference);
            yield return new WaitUntil(() => handle.IsDone);
            desktopPlatformSettings = handle.Result;
            Debug.Log($"Device Platform {desktopPlatformSettings.devicePlatform} initialized");
        }

        public IApplicationLifecycle InputHandler() {
            DesktopInput inputHandler = new DesktopInput((DesktopInputSettings)desktopPlatformSettings.inputSettings);
            inputHandler.Initialize();
            return inputHandler;
        }

        public void Tick() { }
        public void Dispose() { }
        public void OnApplicationQuit() { }
    }
}
