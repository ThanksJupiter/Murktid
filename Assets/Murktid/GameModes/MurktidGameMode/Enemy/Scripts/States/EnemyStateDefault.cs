using UnityEngine;

namespace Murktid {

    public class EnemyStateDefault : State {

        private EnemyAbilityComponent abilityComponent;

        protected override void Setup() {
            abilityComponent = StateMachine.AbilityComponent as EnemyAbilityComponent;
            if(abilityComponent == null) {
                Debug.LogError($"{nameof(EnemyAbilityComponent)} is not an EnemyAbilityComponent");
                return;
            }

            abilityComponent.AddDefaultAbility<EnemyAbilityLookForPlayer>();
            abilityComponent.AddDefaultAbility<EnemyAbilityChasePlayer>();
            abilityComponent.AddDefaultAbility<EnemyAbilityAttackPlayer>();
        }

        protected override void Tick(float deltaTime) {
            abilityComponent.Tick(deltaTime);
        }

        protected override void OnDestroy() {
            abilityComponent.StopAllAbilities();
        }
    }
}
