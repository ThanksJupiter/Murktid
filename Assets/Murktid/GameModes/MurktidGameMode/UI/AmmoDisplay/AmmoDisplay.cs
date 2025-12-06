using R3;
using UnityEngine;

namespace Murktid {

    public class AmmoDisplay {

        private AmmoDisplayReference ammoDisplayReference;
        private PlayerWeaponData currentWeaponData = null;

        public AmmoDisplay(PlayerReference playerReference) {
            ammoDisplayReference = playerReference.ammoDisplayReference;
        }

        public void EquipNewWeapon(PlayerWeaponData newWeaponData) {
            if(currentWeaponData != null) {
                currentWeaponData.loadedBullets.OnCompleted();
                currentWeaponData.maxLoadedBullets.OnCompleted();
            }

            currentWeaponData = newWeaponData;
            currentWeaponData.loadedBullets.Subscribe(UpdateAmmoCount);
            currentWeaponData.bulletsInReserve.Subscribe(UpdateMaxReserveCount);
        }

        private void UpdateAmmoCount(int value) {
            ammoDisplayReference.currentLoaded.text = value.ToString();
        }

        private void UpdateMaxReserveCount(int value) {
            ammoDisplayReference.currentReserve.text = value.ToString();
        }
    }
}
