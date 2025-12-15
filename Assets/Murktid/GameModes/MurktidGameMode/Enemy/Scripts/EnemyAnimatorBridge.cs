using UnityEngine;

namespace Murktid {

    public class EnemyAnimatorBridge {
        private Animator animator;

        public EnemyAnimatorBridge(Animator animator) {
            this.animator = animator;
        }

        public bool IsIdle {
            get => animator.GetBool(Param.isIdle);
            set => animator.SetBool(Param.isIdle, value);
        }

        public bool IsChasing {
            get => animator.GetBool(Param.isChasing);
            set => animator.SetBool(Param.isChasing, value);
        }

        public bool IsWalking {
            get => animator.GetBool(Param.isWalking);
            set => animator.SetBool(Param.isWalking, value);
        }

        public bool IsAttacking {
            get => animator.GetBool(Param.isAttacking);
            set => animator.SetBool(Param.isAttacking, value);
        }

        public bool IsHitboxActive => animator.GetFloat(Param.hitboxActive) > .1f;

        public float Speed {
            get => animator.GetFloat(Param.speed);
            set => animator.SetFloat(Param.speed, value);
        }

        private static class Param {
            public static readonly int empty = Animator.StringToHash("Empty");
            public static readonly int isIdle = Animator.StringToHash("isIdle");
            public static readonly int isChasing = Animator.StringToHash("isChasing");
            public static readonly int isWalking = Animator.StringToHash("isWalking");
            public static readonly int isAttacking = Animator.StringToHash("isAttacking");
            public static readonly int hitboxActive = Animator.StringToHash("HitboxActive");
            public static readonly int speed = Animator.StringToHash("Speed");
        }
    }
}
