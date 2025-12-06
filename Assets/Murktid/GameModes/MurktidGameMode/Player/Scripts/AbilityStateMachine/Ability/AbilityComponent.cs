using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    public class AbilityComponent {
        protected readonly HashSet<Ability> currentAbilities = new();
        private readonly Dictionary<object, HashSet<AbilityTag>> blockers = new();
        private readonly HashSet<AbilityTag> blockedTags = new();

        public void AddDefaultAbility<T>() where T : Ability, new() {
            T ability = new T();
            currentAbilities.Add(ability);
            ability.Setup_Internal(this);
        }

        public void BlockAbility(AbilityTag blockingTag, object instigator) {
            if(!blockers.TryGetValue(instigator, out HashSet<AbilityTag> abilityTags)) {
                abilityTags = new HashSet<AbilityTag>();
                blockers.Add(instigator, abilityTags);
            }

            abilityTags.Add(blockingTag);
            blockedTags.Add(blockingTag);

            foreach(Ability ability in currentAbilities) {
                if(ability.IsActive && ability.Tags.Contains(blockingTag)) {
                    ability.OnDeactivate_Internal();
                }
            }
        }

        public void UnblockAbility(AbilityTag blockingTag, object instigator) {
            if(blockers.TryGetValue(instigator, out HashSet<AbilityTag> abilityTags)) {
                if(abilityTags.Remove(blockingTag)) {
                    RecalculateBlockedTags();
                }
            }
        }

        public void UnblockAbilitiesByInstigator(object instigator) {
            if(blockers.Remove(instigator)) {
                RecalculateBlockedTags();
            }
        }

        public void StopAllAbilities() {
            foreach(Ability ability in currentAbilities) {
                ability.OnDeactivate_Internal();
            }

            currentAbilities.Clear();
        }

        private void RecalculateBlockedTags() {
            blockedTags.Clear();
            foreach((_, HashSet<AbilityTag> blockedTags) in blockers) {
                foreach(AbilityTag blockedTag in blockedTags) {
                    this.blockedTags.Add(blockedTag);
                }
            }
        }

        public void Tick(float deltaTime) {
            foreach(Ability ability in currentAbilities) {
                if(!ability.IsActive && !ability.IsBlocked(blockedTags) && ability.ShouldActivate()) {
                    ability.OnActivate_Internal();
                }
                else if(ability.IsActive && (ability.IsBlocked(blockedTags) || ability.ShouldDeactivate())) {
                    ability.OnDeactivate_Internal();
                }

                if(ability.IsActive) {
                    ability.Tick_Internal(deltaTime);
                }
            }
        }
    }
}
