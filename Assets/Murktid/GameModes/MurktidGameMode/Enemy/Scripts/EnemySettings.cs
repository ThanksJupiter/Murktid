using UnityEngine;

namespace Murktid {

    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Murktid/GameMode/Murktid/EnemySettings")]
    public class EnemySettings : ScriptableObject {
        public float defaultMoveSpeed = 10f;
        public float aggroRange = 15f;
    }
}
