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

                case WeaponType.Primary:
                    Context.playerEquipmentData.currentWeaponType = WeaponType.Secondary;
                    hasSwitchedWeapon = true;
                    break;
                case WeaponType.Secondary:
                    Context.playerEquipmentData.currentWeaponType = WeaponType.Primary;
                    hasSwitchedWeapon = true;
                    break;
                default:
                    break;
            }
        }
    }
}
