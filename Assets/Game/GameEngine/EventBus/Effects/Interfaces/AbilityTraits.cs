using System;

namespace Game.Gameplay
{
    [Flags]
    public enum AbilityTraits
    {
        None = 0,
        Melee = 1,
        Ranged = 2,
        Heal = 4,
        Physical = 8,
    }
}