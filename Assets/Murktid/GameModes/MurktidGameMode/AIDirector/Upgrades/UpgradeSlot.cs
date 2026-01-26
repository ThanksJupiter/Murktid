using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Murktid {

    public class UpgradeSlot : MonoBehaviour {

        public Action<UpgradeSlot> onSelected;
        public TMP_Text name;
        public TMP_Text description;

        public AbstractStatusEffect statusEffect;
        public Button button;

        public void SetStatusEffect(AbstractStatusEffect statusEffect) {
            this.statusEffect = statusEffect;
            name.text = statusEffect.name;
            description.text = statusEffect.description;
        }

        public void SelectUpgrade() {
            onSelected?.Invoke(this);
        }
    }
}
