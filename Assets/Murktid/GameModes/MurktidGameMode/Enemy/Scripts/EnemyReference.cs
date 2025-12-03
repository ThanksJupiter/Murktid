using UnityEngine;

namespace Murktid {

    public class EnemyReference : MonoBehaviour, ITarget {
        public EnemyContext context;
        public Animator animator;

        // prototype hit function
        public void Hit(float damage) {
            context.isDead = true;
        }
    }
}
