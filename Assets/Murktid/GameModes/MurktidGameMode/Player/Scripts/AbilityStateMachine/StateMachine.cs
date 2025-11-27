using System;
using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    public class StateMachine {
        public readonly List<State> stateStack = new();
        public State TopmostState => stateStack.Count > 0 ? stateStack[^1] : null;
        private Dictionary<Type, State> stateCache = new();

        public AbilityComponent AbilityComponent { get; private set; }

        public StateMachine(AbilityComponent playerAbilityComponent) {
            AbilityComponent = playerAbilityComponent;
        }

        public void PushState<T>() where T : State, new() {
            PushState_Internal<T>();
        }

        private void PushState_Internal<T>() where T : State, new() {
            State newState = GetOrCreateState<T>();
            TopmostState?.OnExit_Internal();
            stateStack.Add(newState);
            newState.OnEnter_Internal(this);
        }

        public void PopState() {
            PopState_Internal();
        }

        private void PopState_Internal() {
            TopmostState?.OnExit_Internal();
            if(stateStack.Count > 0) {
                stateStack.RemoveAt(stateStack.Count - 1);
            }

            TopmostState?.OnEnter_Internal(this);
        }

        private State GetOrCreateState<T>() where T : State, new() {
            Type classType = typeof(T);
            if(!stateCache.TryGetValue(classType, out State cachedState)) {
                T newState = new T {
                    StateMachine = this
                };

                newState.Internal_Setup();
                stateCache.Add(classType, newState);
                return newState;
            }

            return cachedState;
        }

        public void Tick(float deltaTime) {
            if(TopmostState == null) {
                return;
            }

            TopmostState.Tick_Internal(deltaTime);

            if(TopmostState.WantsToPop) {
                PopState_Internal();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            TopmostState?.OnDrawGizmos_Internal();
        }

        private void OnDrawGizmosSelected() {
            TopmostState?.OnDrawGizmosSelected_Internal();
        }
#endif
    }
}
