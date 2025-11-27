using UnityEngine;

namespace Murktid {

    public enum PlayerType {
        DefaultStateMachine,
        AbilityStateMachine
    }

    [CreateAssetMenu(fileName = "MurktidGameData", menuName = "Murktid/GameMode/Murktid/GameModeData")]
    public class MurktidGameData : ScriptableObject {
        public PlayerData playerData;
        public PlayerType playerType = PlayerType.DefaultStateMachine;
    }
}
