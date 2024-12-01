using System;

namespace Game.Gameplay
{
    [Serializable]
    public enum LinkStatusType
    {
        None = 0,
        LowFloat = 10,
        Repulsed = 20,
        Knockdown = 30
    }
}