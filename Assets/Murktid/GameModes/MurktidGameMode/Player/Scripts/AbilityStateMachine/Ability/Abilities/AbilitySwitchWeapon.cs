using System;
using UnityEngine;

namespace Murktid {

    public class AbilitySwitchWeapon : PlayerAbility {

        private bool hasSwitchedWeapon = false;

        public override bool ShouldActivate() {
            if(!Context.input.SwitchWeapon.wasPressedThisFrame) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(!hasSwitchedWeapon) {
                return false;
            }

            return true;
        }

        protected override void Tick(float deltaTime) {
            switch(Context.playerEquipmentData.currentWeaponType) {
                case WeaponType.Melee:
                    Context.playerEquipmentData.currentWeaponType = WeaponType.Ranged;
                    Context.controller.WeaponSystem.InstantiateWeapon(Context.defaultRangedWeaponReferencePrefab);
                    hasSwitchedWeapon = true;
                    break;
                case WeaponType.Ranged:
                    Context.playerEquipmentData.currentWeaponType = WeaponType.Melee;
                    Context.controller.WeaponSystem.InstantiateWeapon(Context.defaultMeleeWeaponReferencePrefab);
                    hasSwitchedWeapon = true;
                    break;
                default:
                    break;
            }
        }
    }
}
