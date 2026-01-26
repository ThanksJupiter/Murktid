using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Murktid {

    public interface IStatusEffectContext {
        public GameObject MachineGameObject { get; }
        public Transform Transform => MachineGameObject.transform;
        public StatusEffectSystem System { get; }
        public PlayerContext Context { get; }
    }

    public class StatusEffectSystem {

        public IStatusEffectContext statusEffectContext;

        [SerializeField] private List<AbstractStatusEffect> effects = new();
        public IEnumerable<AbstractStatusEffect> Effects => effects;

        public void Tick(float deltaTime) {

            for(int i = effects.Count - 1; i >= 0; i--) {
                effects[i].OnTick(deltaTime);
            }

            for(int i = effects.Count - 1; i >= 0; i--) {
                if(effects[i].remainingTime > 0.0f) {
                    continue;
                }

                if(effects[i].isInfinite) {
                    continue;
                }

                RemoveEffect(i);
            }
        }

        public void AddEffects(IEnumerable<AbstractStatusEffect> enumerable) {
            foreach(AbstractStatusEffect effect in enumerable) {

            }
        }

        public AbstractStatusEffect AddEffect(AbstractStatusEffect effect) {
            if(effect == null) {
                Debug.Log($"You tried to add an effect that is null");
                return null;
            }

            if(TryGetEffectOfType(effect.GetType(), out AbstractStatusEffect activeEffect)) {
                if(activeEffect.OnEffectRenewed(effect)) {
                    return activeEffect;
                }
            }

            if(statusEffectContext != null) {
                effects.Add(Object.Instantiate(effect));
                AbstractStatusEffect clone = effects[^1];
                clone.ActivateEffect(statusEffectContext);
                clone.OnAdded();
                return clone;
            }

            return null;
        }

        public void RemoveAllEffects() {
            for(int i = effects.Count - 1; i >= 0; i--) {
                RemoveEffect(i);
            }
        }

        public void RemoveEffect(AbstractStatusEffect effect) {
            if(effect.isInfinite) {
                if(TryGetEffectOfType(effect.GetType(), out AbstractStatusEffect outEffect)) {
                    RemoveEffect(effects.IndexOf(outEffect));
                    return;
                }
            }

            RemoveEffect(effects.IndexOf(effect));
        }

        public void RemoveEffects(List<AbstractStatusEffect> effectsToRemove) {
            foreach(AbstractStatusEffect effect in effectsToRemove) {
                RemoveEffect(effect);
            }
        }

        public void RemoveEffect(int index) {
            if(index == -1) {
                return;
            }

            AbstractStatusEffect effect = effects[index];
            effects.RemoveAt(index);
            effect.OnRemoved();

            if(Application.isPlaying) {
                Object.Destroy(effect);
            }
            else {
                Object.DestroyImmediate(effect);
            }
        }

        public bool TryGetEffectOfType(Type effectType, out AbstractStatusEffect outEffect) {
            for(int i = 0; i < effects.Count; i++) {
                if(effects[i].GetType() == effectType) {
                    outEffect = effects[i];
                    return true;
                }
            }

            outEffect = null;
            return false;
        }

        public bool HasEffect<TEffect>() where TEffect : AbstractStatusEffect {
            return effects.Exists((e) => e.GetType() == typeof(TEffect));
        }

        public float GetStatusEffectedStaminaRegeneration(float originalStaminaRegeneration) {
            float workingStaminaRegeneration = originalStaminaRegeneration;

            for(int i = effects.Count - 1; i >= 0; i--) {
                workingStaminaRegeneration = effects[i].GetStatusEffectedStaminaRegeneration(workingStaminaRegeneration);
            }

            return workingStaminaRegeneration;
        }

        public float GetStatusEffectedRangedDamage(float originalRangedDamage) {
            float workingRangedDamage = originalRangedDamage;

            for(int i = effects.Count - 1; i >= 0; i--) {
                workingRangedDamage = effects[i].GetStatusEffectedRangedDamage(workingRangedDamage);
            }

            return workingRangedDamage;
        }
    }
}
