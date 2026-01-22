using UnityEngine;

namespace Murktid {

    public class AIDirectorDebugMenu {
        public AIDirectorDebugMenuReference reference;

        public AIDirectorDebugMenu(AIDirectorDebugMenuReference reference) {
            this.reference = reference;
        }

        public void UpdateCurrentStateText(EAIDirectorState state) {
            reference.currentAIDirectorState.text = state.ToString();
        }

        public void UpdateEnemyCountText(int amount) {
            reference.currentEnemyCount.text = amount.ToString();
        }

        public void UpdateStressLevelText(float value) {
            reference.stressLevel.text = value.ToString("0.0");
        }
    }
}
