using UnityEngine;

namespace Murktid {

    [System.Serializable]
    public class Input<T> {
        public T value;
        public bool wasPressedThisFrame;
        public bool wasReleasedThisFrame;
        public double pressedTimestamp;

        public bool IsPressed =>
            value switch {
                float floatValue => floatValue > 0,
                Vector2 vector2Value => vector2Value.sqrMagnitude > 0,
                bool boolValue => boolValue,
                _ => false
            };
    }

    public interface IInput {
        public Input<Vector2> Move { get; set; }
        public Input<Vector2> Look { get; set; }
        public Input<bool> PrimaryAction { get; set; }
        public Input<bool> SecondaryAction { get; set; }
    }
}
