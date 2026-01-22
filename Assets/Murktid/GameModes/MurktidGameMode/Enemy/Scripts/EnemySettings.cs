using UnityEngine;

namespace Murktid {

    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Murktid/GameMode/Murktid/EnemySettings")]
    public class EnemySettings : ScriptableObject {

        [Header("Move Speed")]
        public float defaultChaseSpeed = 5f;
        public float minDefaultWalkSpeed = .5f;
        public float maxDefaultWalkSpeed = 1.5f;
        public float minChaseLerpSpeed = .1f;
        public float maxChaseLerpSpeed = .75f;

        [Header("Health")]
        public float maxHealth = 50f;

        [Header("Combat")]
        public float aggroRange = 15f;
        public float attackRange = 2.1f;
        public float threatRange = 5f;
        public float minThreatRange = 10f;
        public float maxThreatRange = 20f;
        public float attackRate = 3f;
        public float rotateToTargetRate = 3f;

        public float checkEngagementStatusInterval = .5f;
    }
}
