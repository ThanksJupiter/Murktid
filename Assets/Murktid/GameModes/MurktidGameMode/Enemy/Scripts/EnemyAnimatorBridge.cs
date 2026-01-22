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

        public bool AttackReady {
            get => animator.GetBool(Param.attackReady);
            set => animator.SetBool(Param.attackReady, value);
        }

        public bool TakeDamage {
            get => animator.GetBool(Param.takeDamage);
            set => animator.SetBool(Param.takeDamage, value);
        }

        public bool IsKnockback {
            get => animator.GetBool(Param.isKnockback);
            set => animator.SetBool(Param.isKnockback, value);
        }

        public bool IsDead {
            get => animator.GetBool(Param.isDead);
            set => animator.SetBool(Param.isDead, value);
        }

        public bool IsHitboxActive => animator.GetFloat(Param.hitboxActive) > .1f;

        public bool IsMovementLocked => animator.GetFloat(Param.movementLock) > .1f;

        public bool IsInBaseLayer => animator.GetCurrentAnimatorStateInfo(Layer.baseLayer).shortNameHash != Param.empty;
        public bool IsInDamageLayer => animator.GetCurrentAnimatorStateInfo(Layer.damage).shortNameHash != Param.empty;
        public bool IsInDeadLayer => animator.GetCurrentAnimatorStateInfo(Layer.dead).shortNameHash != Param.empty;

        public float Speed {
            get => animator.GetFloat(Param.speed);
            set => animator.SetFloat(Param.speed, value, .25f, Time.deltaTime);
        }

        public static class Layer
        {
            public static readonly int baseLayer = 0;
            public static readonly int attack = 1;
            public static readonly int damage = 2;
            public static readonly int dead = 3;
        }

        private static class Param {
            public static readonly int empty = Animator.StringToHash("Empty");
            public static readonly int isIdle = Animator.StringToHash("isIdle");
            public static readonly int isChasing = Animator.StringToHash("isChasing");
            public static readonly int isWalking = Animator.StringToHash("isWalking");
            public static readonly int isAttacking = Animator.StringToHash("isAttacking");
            public static readonly int attackReady = Animator.StringToHash("AttackReady");
            public static readonly int hitboxActive = Animator.StringToHash("HitboxActive");
            public static readonly int speed = Animator.StringToHash("Speed");
            public static readonly int movementLock = Animator.StringToHash("MovementLock");
            public static readonly int takeDamage = Animator.StringToHash("TakeDamage");
            public static readonly int isKnockback = Animator.StringToHash("isKnockback");
            public static readonly int isDead = Animator.StringToHash("IsDead");
        }
    }
}
