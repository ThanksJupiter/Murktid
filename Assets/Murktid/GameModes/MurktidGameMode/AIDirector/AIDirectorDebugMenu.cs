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

        public void UpdateCurrentRoundText(int roundNo) {
            reference.currentRound.text = roundNo.ToString();
        }

        public void UpdateCurrentPlayerLevel(int level) {
            reference.currentPlayerLevel.text = level.ToString();
        }

        public void UpdateExperience(float current, float required) {
            reference.currentExperienceText.text = current.ToString("0");
            reference.requiredExperienceText.text = required.ToString("0");
            reference.experienceSlider.value = current / required;
        }
    }
}
