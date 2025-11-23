using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Murktid {

    [CreateAssetMenu(fileName = "BootstrapSettings", menuName = "Murktid/Settings/General/BootstrapSettings")]
    public class BootstrapSettings : ScriptableObject {
        public AssetReference menuScene;
        public GameModeSettings gameModeSettings;
        public AssetReferenceT<MenuPrefabsContainer> menuPrefabsContainer;
    }
}
