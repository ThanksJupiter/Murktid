/*
using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    public class Ability {
        public HashSet<AbilityTag> Tags { get; } = new();
        public bool IsActive { get; private set; }
        public float TimeActivated { get; private set; }
        public float TimeSinceActivated => IsActive ? Time.time - TimeActivated : 0f;
        public Transform Owner { get; private set; }
        public AbilityComponent OwningAbilityComponent { get; private set; }

        protected void AddTag(AbilityTag tag) {
            Tags.Add(tag);
        }

        protected void BlockAbility(AbilityTag tag, object instigator) {
            OwningAbilityComponent.BlockAbility(tag, instigator);
        }

        protected void UnblockAbility(AbilityTag tag, object instigator) {
            OwningAbilityComponent.UnblockAbility(tag, instigator);
        }

        protected void UnblockAbilitiesByInstigator(object instigator) {
            OwningAbilityComponent.UnblockAbilitiesByInstigator(instigator);
        }

        public virtual void Setup_Internal(AbilityComponent owningAbilityComponent) {
            Owner = owningAbilityComponent.transform;
            OwningAbilityComponent = owningAbilityComponent;
            Setup();
        }

        public void OnActivate_Internal() {
            IsActive = true;
            TimeActivated = Time.time;
            OnActivate();
        }

        public void OnDeactivate_Internal() {
            IsActive = false;
            OnDeactivate();
        }

        public virtual void Tick_Internal(float deltaTime) {
            Tick(deltaTime);
        }

        public bool IsBlocked(HashSet<AbilityTag> blockedTags) {
            foreach(AbilityTag tag in blockedTags) {
                if(Tags.Contains(tag)) {
                    return true;
                }
            }

            return false;
        }

        public virtual bool ShouldActivate() {
            return false;
        }

        public virtual bool ShouldDeactivate() {
            return true;
        }

        protected virtual void Setup() {}
        protected virtual void OnActivate() {}
        protected virtual void OnDeactivate() {}
        protected virtual void Tick(float deltaTime) {}

        protected virtual void OnDrawGizmos() {}
        protected virtual void OnDrawGizmosSelected() {}
    }
}
*/
