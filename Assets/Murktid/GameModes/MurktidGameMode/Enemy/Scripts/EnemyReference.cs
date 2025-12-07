using UnityEngine;

namespace Murktid {

    public class EnemyReference : MonoBehaviour, ITarget {
        public EnemyContext context;
        public Animator animator;

        // prototype hit function
        public void Hit(float damage) {

            context.health.TakeDamage(damage);

            Debug.Log($"Remaining health: {context.health.currentHealth.Value}");
            if(context.health.currentHealth.Value <= 0f) {
                context.isDead = true;
            }
        }
    }
}
