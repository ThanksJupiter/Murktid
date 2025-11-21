using UnityEngine;

namespace Murktid {

    public class ApplicationData {
        public PlatformSelector platformSelector;
        public ApplicationState ActiveApplicationState { get; private set; }
        public GameMode ActiveGameMode { get; private set; }

        public void ChangeApplicationState(ApplicationState applicationState) {
            ActiveApplicationState = applicationState;
        }

        public void ChangeGameModeState(GameMode gameMode) {
            if(ActiveApplicationState != ApplicationState.GameMode && gameMode != GameMode.Invalid) {
                Debug.LogError("Cannot change Game Mode state in any other Application State than ApplicationState.GameMode");
            }

            ActiveGameMode = gameMode;
        }
    }
}
