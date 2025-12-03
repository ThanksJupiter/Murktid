using UnityEngine;

namespace Murktid {

    public class EnemyAnimatorBridge {
        private Animator animator;

        public EnemyAnimatorBridge(Animator animator) {
            this.animator = animator;
        }

        public bool IsIdle
        {
            get => animator.GetBool(Param.isIdle);
            set => animator.SetBool(Param.isIdle, value);
        }

        public bool IsChasing
        {
            get => animator.GetBool(Param.isChasing);
            set => animator.SetBool(Param.isChasing, value);
        }

        public bool IsAttacking
        {
            get => animator.GetBool(Param.isAttacking);
            set => animator.SetBool(Param.isAttacking, value);
        }

        private static class Param {
            public static readonly int empty = Animator.StringToHash("Empty");
            public static readonly int isIdle = Animator.StringToHash("isIdle");
            public static readonly int isChasing = Animator.StringToHash("isChasing");
            public static readonly int isAttacking = Animator.StringToHash("isAttacking");
        }
    }
}
