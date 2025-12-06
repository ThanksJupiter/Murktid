using R3;
using UnityEngine;

namespace Murktid {

    public class PlayerWeaponSystem : IPlayerSystem {

        private PlayerContext context;
        private AmmoDisplay ammoDisplay;

        public void Initialize(PlayerReference playerReference) {
            context = playerReference.context;
            ammoDisplay = new(playerReference);

            context.playerEquipmentData.currentWeapon.Subscribe(OnEquipNewWeapon);
        }

        private void OnEquipNewWeapon(PlayerWeaponData weaponData) {
            ammoDisplay.EquipNewWeapon(weaponData);
        }
    }
}
