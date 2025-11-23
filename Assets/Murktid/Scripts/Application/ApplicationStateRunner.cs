using System;
using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    public class ApplicationStateRunner : MonoBehaviour {
        private IApplicationState currentState;
        private ApplicationState currentStateType;
        private Dictionary<ApplicationState, IApplicationState> applicationStates;
        private ApplicationData applicationData;
        private bool isRunnerUpdated;
        private bool isUnloading;
        private IPlatform platform;

        public void Initialize(Dictionary<ApplicationState, IApplicationState> stateLookUp, ApplicationData applicationData, IPlatform selectedPlatform) {
            platform = selectedPlatform;
            applicationStates = stateLookUp;
            this.applicationData = applicationData;
            ChangeApplicationState(applicationData.ActiveApplicationState);
        }

        private void Update() {
            platform.Tick();

            if(!isRunnerUpdated && currentState.IsApplicationStateInitialized) {
                isRunnerUpdated = true;
            }

            ApplicationState nextState = currentStateType;
            if(isRunnerUpdated || isUnloading) {
                nextState = currentState.Tick();
                if(applicationData.ActiveApplicationState != currentStateType) {
                    nextState = applicationData.ActiveApplicationState;
                }
            }

            if(nextState != currentStateType) {
                ChangeApplicationState(nextState);
            }
        }

        private void FixedUpdate() {
            if(!currentState.IsApplicationStateInitialized) {
                return;
            }
        }

        private void LateUpdate() {
            if(!currentState.IsApplicationStateInitialized) {
                return;
            }

            currentState.LateTick();
        }

        private void ChangeApplicationState(ApplicationState newApplicationState) {
            if(currentState != null) {
                currentState.ExitApplicationState();
            }

            currentState = applicationStates[newApplicationState];
            currentState.EnterApplicationState();
            currentStateType = newApplicationState;
            applicationData.ChangeApplicationState(newApplicationState);
            isRunnerUpdated = false;
            Debug.Log($"Entered { newApplicationState } ApplicationState");
        }
    }
}
