using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Murktid {

    [CreateAssetMenu(menuName = "Murktid/Settings/General/InitializerSettingsFile", fileName = "InitializerSettingsFile")]
    public class InitializerSettingsFile : ScriptableObject {
        public AssetReferenceT<DesktopPlatformSettings> desktopPlatformSettings;
        //public AssetReferenceT<AndroidPlatformSettings> androidPlatformSettings;
        //public AssetReferenceT<>
        public AssetReferenceT<BootstrapSettings> bootstrapAssetReference;
    }
}
