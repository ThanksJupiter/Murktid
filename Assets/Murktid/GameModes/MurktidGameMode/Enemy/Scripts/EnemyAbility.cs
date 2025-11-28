using UnityEngine;

namespace Murktid {

    public class EnemyAbility : Ability {
        protected EnemyContext Context { get; private set; }

        public override void Setup_Internal(AbilityComponent owningAbilityComponent) {
            base.Setup_Internal(owningAbilityComponent);

            if(owningAbilityComponent is EnemyAbilityComponent enemyAbilityComponent) {
                Context = enemyAbilityComponent.Context;
            }
        }
    }
}
