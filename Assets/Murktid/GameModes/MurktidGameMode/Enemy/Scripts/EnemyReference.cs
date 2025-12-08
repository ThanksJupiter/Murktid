using UnityEngine;

namespace Murktid {

    public class EnemyReference : MonoBehaviour, ITarget {
        public EnemyContext context;
        public Animator animator;
        public EnemyController controller;

        // prototype hit function
        public void Hit(float damage) {

            PlayerReference playerReference = FindFirstObjectByType<PlayerReference>();
            context.targetPlayer = playerReference;

            EnemyController[] nearbyEnemies = GameplayStatics.GetEnemiesAtPositionWithinRadius(transform.position, context.settings.aggroRange, context.enemyMask);
            foreach(EnemyController enemy in nearbyEnemies) {
                enemy.Context.targetPlayer = playerReference;
            }

            context.health.TakeDamage(damage);

            if(context.health.currentHealth.Value <= 0f) {
                context.isDead = true;
            }
        }
    }
}
