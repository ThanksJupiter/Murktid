using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    public class CursorHandler {
        public enum CursorState {
            Free,
            Locked,
        }

        public CursorState CurrentState =>
            stateStack.Count > 0 ? stateStack[^1].state : CursorState.Free;

        private List<(object instigator, CursorState state)> stateStack = new();

        public Vector3 CursorPos {
            get {
                switch(CurrentState) {
                    case CursorState.Free:
                        return Input.mousePosition;
                    case CursorState.Locked:
                        return new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);
                    default:
                        return Input.mousePosition;
                }
            }
        }

        public void PushState(CursorState state, object instigator) {
            stateStack.Add((instigator, state));
            ApplyState();
        }

        public void PopState(CursorState state, object instigator) {
            if(stateStack.Count <= 0) {
                return;
            }

            for(int i = 0; i < stateStack.Count; i++) {
                if(stateStack[i].state != state)
                    continue;

                if(stateStack[i].instigator != instigator)
                    continue;

                stateStack.RemoveAt(i);
                ApplyState();
            }
        }

        public void ClearInstigator(object instigator) {
            bool removedAnyEntry = false;

            if(stateStack.Count < 0)
                return;

            for(int i = stateStack.Count - 1; i >= 0; i--) {
                if(stateStack[i].instigator != instigator)
                    continue;

                stateStack.RemoveAt(i);
                removedAnyEntry = true;
            }

            if(removedAnyEntry) {
                ApplyState();
            }
        }

        private void ApplyState() {
            switch(CurrentState) {
                case CursorState.Free:
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
                case CursorState.Locked:
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;
                default:
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
            }
        }

        public void Dispose() {
            ClearInstigator(this);
        }
    }
}
