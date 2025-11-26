using UnityEngine;

namespace Murktid {

    public abstract class State {
        public StateMachine StateMachine { get; internal set; }

        public float TimeEntered { get; private set; }
        public float TimeSinceEntered => Time.time - TimeEntered;
        public float TimeExited { get; private set; }
        public float TimeSinceExited => Time.time - TimeExited;
        public bool WantsToPop { get; private set; }

        public void Internal_Setup() {
            Setup();
        }

        public void OnEnter_Internal(StateMachine stateMachine) {
            StateMachine = stateMachine;

            TimeEntered = Time.time;
            WantsToPop = false;
            OnEnter();
        }

        public void OnExit_Internal() {
            TimeExited = Time.time;
            OnExit();
        }
        public void Tick_Internal(float deltaTime) {
            Tick(deltaTime);
        }
        public void OnDestroy_Internal() {
            OnDestroy();
        }
        public void OnDrawGizmos_Internal() {
            OnDrawGizmos();
        }

        public void OnDrawGizmosSelected_Internal() {
            OnDrawGizmosSelected();
        }
        protected void PopState() {
            WantsToPop = true;
        }

        protected virtual void Setup() { }
        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
        protected virtual void OnDestroy() { }
        protected virtual void Tick(float deltaTime) { }

        protected virtual void OnDrawGizmos() { }
        protected virtual void OnDrawGizmosSelected() { }
    }
}
