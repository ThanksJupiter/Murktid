using UnityEngine;
using UnityEngine.InputSystem;

namespace Murktid {

    public class SplashApplicationState : IApplicationState {
        private readonly BootstrapSettings bootstrapSettings;
        private readonly ApplicationData applicationData;
        public void Dispose() {
            throw new System.NotImplementedException();
        }
        public bool IsApplicationStateInitialized { get; set; } = true;

        public SplashApplicationState(BootstrapSettings bootstrapSettings, ApplicationData applicationData) {
            this.bootstrapSettings = bootstrapSettings;
            this.applicationData = applicationData;
        }

        public void EnterApplicationState() {
            // load loud woman intro gif
        }
        public ApplicationState Tick() {

            applicationData.ChangeApplicationState(ApplicationState.MainMenu);
            return ApplicationState.Splash;

            Keyboard keyboard = Keyboard.current;
            if(keyboard.anyKey.wasPressedThisFrame) {
                Debug.Log($"any key pressed");
                applicationData.ChangeApplicationState(ApplicationState.MainMenu);
            }

            return ApplicationState.Splash;
        }
        public void LateTick() {

        }
        public void ExitApplicationState() {

        }
    }
}
