using System;
using UnityEngine;

namespace Murktid {
    public class PlayerAnimatorBridge {
        private Animator animator;

        public PlayerAnimatorBridge(/*Animator animator*/) {
            /*this.animator = animator;*/
        }

        public void SetAnimator(Animator animator) {
            this.animator = animator;
        }

        public bool IsADS
        {
            get => animator.GetBool(Param.isAds);
            set => animator.SetBool(Param.isAds, value);
        }

        public bool Shoot
        {
            get => animator.GetBool(Param.shoot);
            set => animator.SetBool(Param.shoot, value);
        }

        public bool Reload
        {
            get => animator.GetBool(Param.reload);
            set => animator.SetBool(Param.reload, value);
        }

        public bool IsSprinting
        {
            get => animator.GetBool(Param.isSprinting);
            set => animator.SetBool(Param.isSprinting, value);
        }

        public bool Sheathe
        {
            get => animator.GetBool(Param.sheathe);
            set => animator.SetBool(Param.sheathe, value);
        }

        public bool IsWalking
        {
            get => animator.GetBool(Param.isWalking);
            set => animator.SetBool(Param.isWalking, value);
        }

        public bool IsInRangedLayer => animator.GetCurrentAnimatorStateInfo(Layer.ranged).shortNameHash != Param.empty;
        public bool IsInRangedFireLayer => animator.GetCurrentAnimatorStateInfo(Layer.rangedFire).shortNameHash != Param.empty;
        public bool IsInMeleeLayer => animator.GetCurrentAnimatorStateInfo(Layer.melee).shortNameHash != Param.empty;
        public bool IsInSheatheLayer => animator.GetCurrentAnimatorStateInfo(Layer.sheathe).shortNameHash != Param.empty;

        public bool IsHitboxActive => animator.GetFloat("HitboxActive") > .1f;

        public static class Layer
        {
            public static readonly int unarmed = 0;
            public static readonly int ranged = 1;
            public static readonly int rangedFire = 2;
            public static readonly int melee = 3;
            public static readonly int sheathe = 4;
        }

        private static class Param {
            public static readonly int empty = Animator.StringToHash("Empty");
            public static readonly int isAds = Animator.StringToHash("IsADS");
            public static readonly int shoot = Animator.StringToHash("Shoot");
            public static readonly int reload = Animator.StringToHash("Reload");
            public static readonly int sheathe = Animator.StringToHash("Sheathe");
            public static readonly int isSprinting = Animator.StringToHash("IsSprinting");
            public static readonly int isWalking = Animator.StringToHash("IsWalking");
        }
    }
}
