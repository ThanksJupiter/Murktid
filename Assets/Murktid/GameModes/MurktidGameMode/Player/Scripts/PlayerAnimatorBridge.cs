using UnityEngine;

namespace Murktid {

    public class PlayerAnimatorBridge {
        private readonly Animator animator;

        public PlayerAnimatorBridge(Animator animator) {
            this.animator = animator;
        }

        public bool IsADS
        {
            get => animator.GetBool(Param.isAds);
            set => animator.SetBool(Param.isAds, value);
        }

        private static class Param {
            public static readonly int empty = Animator.StringToHash("Empty");
            public static readonly int isAds = Animator.StringToHash("IsADS");
        }
    }
}
