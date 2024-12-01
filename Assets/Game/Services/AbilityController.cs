using System.Collections.Generic;
using Game.Gameplay;
using Zenject;

public class AbilityController
{
    private readonly DiContainer _diContainer;
    private Dictionary<AbilityType, List<IEffect>> _abilitiesByType = new();

    public void Initialize(List<IEntity> entities)
    {
        //     foreach (var entity in entities)
        //     {
        //         var abilities = entity.GetEntityComponent<AbilityComponent>().GetAbilities();
        //         foreach (var ability in abilities)
        //         {
        //             switch (ability.AbilityType)
        //             {
        //                 case (AbilityType.Standard):
        //                 {
        //                     break;
        //                 }
        //                 case (AbilityType.Passive)
        //
        //                 default:
        //                 {
        //                     break;
        //                 }
        //             }
        //         }
        //     }
    }
    
}