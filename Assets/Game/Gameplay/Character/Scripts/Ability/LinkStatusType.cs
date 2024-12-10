using System;

namespace Game.Gameplay
{
    [Serializable]
    public enum LinkStatusType
    {
        None = 0,
        LowFloat = 10,
        HighFloat = 15,
        Repulse = 20,
        Knockdown = 30,
        OffGuard = 40
    }
}