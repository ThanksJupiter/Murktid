using UnityEngine;

namespace Murktid {

    [CreateAssetMenu(fileName = "SlotSystemSettings", menuName = "Murktid/Settings/SlotSystemSettings")]
    public class SlotSystemSettings : ScriptableObject {
        public int maxAttackers = 3;
    }
}
