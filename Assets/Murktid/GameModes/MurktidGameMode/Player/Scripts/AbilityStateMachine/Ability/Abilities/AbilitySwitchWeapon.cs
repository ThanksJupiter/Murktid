using System;
using UnityEngine;

namespace Murktid {

    public class AbilitySwitchWeapon : PlayerAbility {

        private bool hasSwitchedWeapon = false;
        private bool leaveState = false;

        public override bool ShouldActivate() {
            if(!Context.input.SwitchWeapon.wasPressedThisFrame) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(!leaveState) {
                return false;
            }

            return true;
        }

        protected override void OnActivate() {
            Context.animatorBridge.Sheathe = true;
            hasSwitchedWeapon = false;
            leaveState = false;
        }

        protected override void Tick(float deltaTime) {
            if(Context.animatorBridge.IsInSheatheLayer) {
                Context.animatorBridge.Sheathe = false;
            }

            if(hasSwitchedWeapon && !Context.animatorBridge.IsHitboxActive) {
                leaveState = true;
            }

            if(Context.animatorBridge.IsHitboxActive && !hasSwitchedWeapon) {
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
}
