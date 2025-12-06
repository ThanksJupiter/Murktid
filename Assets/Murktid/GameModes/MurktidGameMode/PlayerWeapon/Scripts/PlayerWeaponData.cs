using R3;
using UnityEngine;

namespace Murktid {

    public class PlayerWeaponData {
        public int LoadedBullets { get => loadedBullets.Value; set => loadedBullets.Value = value; }
        public int MaxLoadedBullets { get => maxLoadedBullets.Value; set => maxLoadedBullets.Value = value; }

        public ReactiveProperty<int> loadedBullets;
        public ReactiveProperty<int> maxLoadedBullets;
        public ReactiveProperty<int> bulletsInReserve;
        public ReactiveProperty<int> maxBulletsInReserve;

        public PlayerWeaponConfig config;
        public PlayerWeaponReference reference;

        public PlayerWeaponData(PlayerWeaponReference weaponReference) {
            reference = weaponReference;
            config = weaponReference.config;

            loadedBullets = new ReactiveProperty<int>(weaponReference.config.maxLoadedBullets);
            maxLoadedBullets = new ReactiveProperty<int>(weaponReference.config.maxLoadedBullets);
            bulletsInReserve = new ReactiveProperty<int>(weaponReference.config.maxBulletsInReserve);
            maxBulletsInReserve = new ReactiveProperty<int>(weaponReference.config.maxBulletsInReserve);
        }

        public void ConsumeAmmo(int amount) {
            loadedBullets.Value -= amount;
        }

        public bool HasLoadedAmmo(int checkThreshold = 0) {

            if(checkThreshold == 0) {
                return loadedBullets.Value > 0;
            }

            return loadedBullets.Value >= checkThreshold;
        }

        public bool HasAmmoInReserve(int checkThreshold = 0) {
            if(checkThreshold == 0) {
                return bulletsInReserve.Value > 0;
            }

            return bulletsInReserve.Value >= checkThreshold;
        }

        public void Reload() {
            int bulletsToLoad = maxLoadedBullets.Value - loadedBullets.Value;

            if(bulletsInReserve.Value >= bulletsToLoad) {
                loadedBullets.Value += bulletsToLoad;
                bulletsInReserve.Value -= bulletsToLoad;
                return;
            }

            loadedBullets.Value += bulletsInReserve.Value;
            bulletsInReserve.Value = 0;
        }
    }
}
