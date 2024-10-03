using UI;
using UnityEngine;

public struct DeactivateHeroEvent: IEvent
{
    public readonly HeroEntity HeroEntity;
    public DeactivateHeroEvent(HeroEntity activeHeroEntity)
    {
        HeroEntity = activeHeroEntity;
    }
}