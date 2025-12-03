using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Murktid {

    [System.Serializable]
    public class EnemyContext {
        [Get] public NavMeshAgent agent;
        [Get] public Rigidbody rigidbody;
        [Get] public Transform transform;
        public GameObject gameObject;
        public EnemySettings settings;
        public LayerMask playerMask;
        public EnemyAnimatorBridge animatorBridge;

        // Target
        public bool HasTarget => targetPlayer != null;
        public PlayerReference targetPlayer = null;

        // prototype behaviour
        public EnemyPrototypeBehaviour enemyPrototypeBehaviour;
        public bool isDead = false;

        public bool IsTargetWithinAttackRange {
            get {
                if(!HasTarget) {
                    return false;
                }

                return Vector3.Distance(transform.position, targetPlayer.transform.position) < settings.attackRange;
            }
        }
    }
}
