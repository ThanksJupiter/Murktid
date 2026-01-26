using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

namespace Murktid {

    public class UpgradeWindow : MonoBehaviour {

        public GameObject upgradeWindow;
        public List<AbstractStatusEffect> availableStatusEffects = new List<AbstractStatusEffect>();
        public Transform upgradesHolder;

        private int availablePoints = 0;
        public TMP_Text availablePointsText;

        public UpgradeSlot upgradeSlotPrefab;

        public Action<int> onCompleted;

        private List<UpgradeSlot> selectableUpgrades = new List<UpgradeSlot>();

        private StatusEffectSystem playerStatusEffectSystem;

        public void Initialize() {
            PlayerReference playerReference = GameObject.FindFirstObjectByType<PlayerReference>();
            playerStatusEffectSystem = playerReference.Context.statusEffectSystem;
            Hide();
        }

        public void Display(int points) {

            availablePoints = points;
            selectableUpgrades.Clear();
            UpdateAvailablePoints(availablePoints);

            for(int i = 0; i < 2; i++) {
                UpgradeSlot upgradeSlot = Instantiate(upgradeSlotPrefab, upgradesHolder);
                upgradeSlot.SetStatusEffect(availableStatusEffects[Random.Range(0, availableStatusEffects.Count)]);
                upgradeSlot.button.onClick.AddListener(upgradeSlot.SelectUpgrade);
                upgradeSlot.onSelected += SelectUpgrade;

                selectableUpgrades.Add(upgradeSlot);
            }

            upgradeWindow.SetActive(true);
        }

        public void Hide() {
            foreach (UpgradeSlot upgradeSlot in selectableUpgrades)
            {
                upgradeSlot.button.onClick.RemoveAllListeners();
                upgradeSlot.onSelected -= SelectUpgrade;
            }

            UpgradeSlot[] slots = upgradesHolder.GetComponentsInChildren<UpgradeSlot>();
            for(int i = slots.Length - 1; i >= 0; i--) {
                Destroy(slots[i].gameObject);
            }

            selectableUpgrades.Clear();
            upgradeWindow.SetActive(false);
        }

        public void UpdateAvailablePoints(int points) {
            availablePointsText.text = points.ToString();
        }

        private void SelectUpgrade(UpgradeSlot upgradeSlot) {

            upgradeSlot.onSelected -= SelectUpgrade;
            playerStatusEffectSystem.AddEffect(upgradeSlot.statusEffect);
            selectableUpgrades.Remove(upgradeSlot);

            availablePoints--;
            UpdateAvailablePoints(availablePoints);
            Destroy(upgradeSlot.gameObject);

            if(selectableUpgrades.Count == 0) {
                onCompleted?.Invoke(availablePoints);
                return;
            }

            if(availablePoints <= 0) {
                onCompleted?.Invoke(0);
                return;
            }
        }
    }
}
