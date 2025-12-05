using UnityEngine;

namespace Murktid {

    public class AbilityReload : PlayerAbility {

        private bool hasReloaded = false;
        private float reloadTimestamp = float.MinValue;

        public override bool ShouldActivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Secondary) {
                return false;
            }

            if(Context.playerEquipmentData.currentWeapon.weaponData.loadedBullets >= Context.playerEquipmentData.currentWeapon.weaponData.maxLoadedBullets) {
                return false;
            }

            // if try to shoot with 0 bullets
            bool emptyReload = Context.playerEquipmentData.currentWeapon.weaponData.loadedBullets <= 0 &&
                Context.recoilOffset <= 1f;

            bool reloadInput = Context.input.Reload.wasPressedThisFrame || emptyReload;
            return reloadInput;
        }

        public override bool ShouldDeactivate() {
            return hasReloaded;
        }

        protected override void OnActivate() {
            Debug.Log($"Activating Ability Reload");
            hasReloaded = false;
            Context.animatorBridge.Reload = true;
            reloadTimestamp = Time.time + 1.75f;
        }

        protected override void Tick(float deltaTime) {
            if(Time.time >= reloadTimestamp) {
                Context.playerEquipmentData.currentWeapon.weaponData.loadedBullets = Context.playerEquipmentData.currentWeapon.weaponData.maxLoadedBullets;
                hasReloaded = true;
            }

            if(/*Context.animatorBridge.IsInRangedFireLayer && */hasReloaded) {
                Context.animatorBridge.Reload = false;
            }
        }
    }
}
