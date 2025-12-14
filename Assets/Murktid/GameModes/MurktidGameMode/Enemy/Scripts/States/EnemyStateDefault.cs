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

            abilityComponent.AddDefaultAbility<EnemyAbilityApproachPlayer>();
            abilityComponent.AddDefaultAbility<EnemyAbilityAttackPlayer>();
            abilityComponent.AddDefaultAbility<EnemyAbilityEngagePlayer>();
            abilityComponent.AddDefaultAbility<EnemyAbilityLookForPlayer>();
            abilityComponent.AddDefaultAbility<EnemyAbilityRequestAttackSlot>();
            abilityComponent.AddDefaultAbility<EnemyAbilityRequestEngageSlot>();
        }

        protected override void Tick(float deltaTime) {
            abilityComponent.Tick(deltaTime);
        }

        protected override void OnDestroy() {
            abilityComponent.StopAllAbilities();
        }
    }
}
