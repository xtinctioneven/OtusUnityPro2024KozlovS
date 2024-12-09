using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class VfxComponent
{
    public Dictionary<Type, ParticleSystem> AbilityVfxDictionary;

    public VfxComponent(KeyValuePair<Type, ParticleSystem>[] abilityVfxPairs = null)
    {
        AbilityVfxDictionary = new();
        if (abilityVfxPairs != null)
        {
            foreach (var pair in abilityVfxPairs)
            {
                AbilityVfxDictionary.Add(pair.Key, pair.Value);
            }
        }
    }

    // public void Install(IEnumerable<IEffectOld> abilities, Transform parent)
    // {
    //     foreach (var ability in abilities)
    //     {
    //         if (ability.VfxAbilityData.IsAttachedToComponent)
    //         {
    //             ParticleSystem vfx = Object.Instantiate(ability.VfxAbilityData.Vfx, parent, true);
    //             vfx.transform.localPosition = Vector3.zero;
    //             vfx.Play();
    //             TryAddVfxByAbility(ability, vfx);
    //         }
    //     }
    // }

    // public bool TryAddVfxByAbility(IEffectOld ability, ParticleSystem vfx)
    // {
    //     Type abilityType = ability.GetType();
    //     return AbilityVfxDictionary.TryAdd(abilityType, vfx);
    // }

    // public bool TryRemoveVfxByAbility(IEffectOld ability, out ParticleSystem removedVfx)
    // {
    //     Type abilityType = ability.GetType();
    //     removedVfx = null;
    //     if (AbilityVfxDictionary.ContainsKey(abilityType))
    //     {
    //         removedVfx = AbilityVfxDictionary[abilityType];
    //         AbilityVfxDictionary.Remove(abilityType);
    //         return true;
    //     }
    //     return false;
    // }
}