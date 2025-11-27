using System;

namespace Murktid {

    public readonly struct AbilityTag : IEquatable<AbilityTag> {
        public string Name { get; }
        private readonly int hash;

        public AbilityTag(string name) {
            Name = name;
            hash = GenerateHashCode(name);
        }

        public static bool operator ==(AbilityTag a, AbilityTag b) {
            return a.hash == b.hash;
        }

        public static bool operator !=(AbilityTag a, AbilityTag b) {
            return a.hash != b.hash;
        }

        public bool Equals(AbilityTag other) {
            return hash == other.hash;
        }

        public override bool Equals(object obj) {
            return obj is AbilityTag abilityTag && Equals(abilityTag);
        }

        public override int GetHashCode() {
            return hash;
        }

        public override string ToString() {
            return Name;
        }

        private static int GenerateHashCode(string name) {
            int hash = 17;
            unchecked {
                foreach(char c in name) {
                    hash = hash * 23 + c;
                }
            }

            return hash;
        }
    }
}
