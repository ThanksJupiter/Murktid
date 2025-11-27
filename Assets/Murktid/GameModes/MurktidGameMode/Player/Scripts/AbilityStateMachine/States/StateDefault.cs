using UnityEngine;

namespace Murktid {

    public class StateDefault : State {
        private PlayerAbilityComponent abilityComponent;

        protected override void Setup() {
            abilityComponent = StateMachine.AbilityComponent as PlayerAbilityComponent;
            if(abilityComponent == null) {
                Debug.LogError($"{nameof(PlayerAbilityComponent)} is not a PlayerAbilityComponent");
                return;
            }

            abilityComponent.AddDefaultAbility<AbilityGroundMovement>();
            abilityComponent.AddDefaultAbility<AbilityJump>();
            abilityComponent.AddDefaultAbility<AbilitySprint>();
            abilityComponent.AddDefaultAbility<AbilityLook>();
            abilityComponent.AddDefaultAbility<AbilityAirMovement>();
            abilityComponent.AddDefaultAbility<AbilityFalling>();
            abilityComponent.AddDefaultAbility<AbilityMoveCamera>();
            abilityComponent.AddDefaultAbility<AbilityPrimaryWeaponEquipped>();
            abilityComponent.AddDefaultAbility<AbilitySecondaryWeaponEquipped>();
            abilityComponent.AddDefaultAbility<AbilitySwitchWeapon>();
        }

        protected override void Tick(float deltaTime) {
            abilityComponent.Tick(deltaTime);
        }

        protected override void OnDestroy() {
            abilityComponent.StopAllAbilities();
        }
    }
}
