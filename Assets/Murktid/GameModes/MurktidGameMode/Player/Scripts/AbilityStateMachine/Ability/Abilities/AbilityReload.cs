using UnityEngine;

namespace Murktid {

    public class AbilityReload : PlayerAbility {

        private bool hasReloaded = false;
        private float reloadTimestamp = float.MinValue;

        public override bool ShouldActivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Secondary) {
                return false;
            }

            if(Context.playerEquipmentData.CurrentWeapon.LoadedBullets >= Context.playerEquipmentData.CurrentWeapon.MaxLoadedBullets) {
                return false;
            }

            // if try to shoot with 0 bullets
            bool emptyReload = Context.playerEquipmentData.CurrentWeapon.LoadedBullets <= 0 &&
                Context.recoilOffset <= 1f;

            bool reloadInput = Context.input.Reload.wasPressedThisFrame || emptyReload;
            return reloadInput;
        }

        public override bool ShouldDeactivate() {
            return hasReloaded;
        }

        protected override void OnActivate() {
            hasReloaded = false;
            Context.IsReloading = true;
            Context.animatorBridge.Reload = true;
            reloadTimestamp = Time.time + 1.9f;
        }

        protected override void OnDeactivate() {
            Context.IsReloading = false;
        }

        protected override void Tick(float deltaTime) {
            if(Time.time >= reloadTimestamp) {
                Context.playerEquipmentData.CurrentWeapon.Reload();
                //Context.playerEquipmentData.CurrentWeapon.LoadedBullets = Context.playerEquipmentData.CurrentWeapon.MaxLoadedBullets;
                hasReloaded = true;
            }

            if(/*Context.animatorBridge.IsInRangedFireLayer && */hasReloaded) {
                Context.animatorBridge.Reload = false;
            }
        }
    }
}
