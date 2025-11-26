using UnityEngine;

namespace Murktid {

    [CreateAssetMenu(fileName = "MurktidGameData", menuName = "Murktid/GameMode/Murktid/GameModeData")]
    public class MurktidGameData : ScriptableObject {
        public PlayerData playerData;
    }
}
