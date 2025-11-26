using System;
using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    public class StateMachine {
        public readonly List<State> stateStack = new();
        public State topmostState => stateStack.Count > 0 ? stateStack[^1] : null;
        private Dictionary<Type, State> stateCache = new();

        public void PushState<T>() where T : State, new() {
            State newState = GetOrCreateState<T>();
        }

        private void PushState_Internal<T>() where T : State, new() {
            State newState = GetOrCreateState<T>();
            topmostState?.OnExit_Internal();
            stateStack.Add(newState);
            newState.OnEnter_Internal(this);
        }

        public void PopState() {
            PopState_Internal();
        }

        private void PopState_Internal() {
            topmostState?.OnExit_Internal();
            if(stateStack.Count > 0) {
                stateStack.RemoveAt(stateStack.Count - 1);
            }

            topmostState?.OnEnter_Internal(this);
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

        public void Tick() {
            if(topmostState == null) {
                return;
            }

            topmostState.Tick_Internal(Time.deltaTime);

            if(topmostState.WantsToPop) {
                PopState_Internal();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            topmostState?.OnDrawGizmos_Internal();
        }

        private void OnDrawGizmosSelected() {
            topmostState?.OnDrawGizmosSelected_Internal();
        }
#endif
    }
}
