using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Murktid {

    [System.Serializable]
    public class EnemyContext {
        public EnemyController controller = null;
        [Get] public NavMeshAgent agent;
        [Get] public Rigidbody rigidbody;
        [Get] public Transform transform;
        public GameObject gameObject;
        public EnemySettings settings;
        public LayerMask playerMask;
        public LayerMask enemyMask;
        public EnemyAnimatorBridge animatorBridge;
        public EnemyHealth health;

        // Target
        public bool HasTarget => targetPlayer != null;
        public PlayerReference targetPlayer = null;
        public PlayerAttackerSlotSystem playerSlotSystem = null;
        public bool hasEngagementSlot = false;
        public int engagementSlotIndex = -1;
        public float engagementSlotRequestCooldown = 1f;
        [HideInInspector]
        public float engagementSlotRequestTimestamp = float.MinValue;

        public bool hasAttackSlot = false;
        public int attackSlotIndex = -1;
        public float attackSlotRequestCooldown = 1f;
        [HideInInspector]
        public float attackSlotRequestTimestamp = float.MinValue;

        // prototype behaviour
        public bool isDead = false;
        public HitboxReference hitbox;

        public bool IsTargetWithinThreatRange {
            get {
                if(!HasTarget) {
                    return false;
                }

                return Vector3.Distance(transform.position, targetPlayer.transform.position) < settings.threatRange;
            }
        }

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
