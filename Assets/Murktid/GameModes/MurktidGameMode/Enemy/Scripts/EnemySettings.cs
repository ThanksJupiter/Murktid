using UnityEngine;

namespace Murktid {

    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Murktid/GameMode/Murktid/EnemySettings")]
    public class EnemySettings : ScriptableObject {
        public float defaultChaseSpeed = 5f;
        public float defaultWalkSpeed = 2f;

        [Header("Health")]
        public float maxHealth = 50f;

        [Header("Combat")]
        public float aggroRange = 15f;
        public float attackRange = 2.1f;
        public float threatRange = 5f;
        public float attackRate = 3f;
        public float rotateToTargetRate = 3f;
    }
}
