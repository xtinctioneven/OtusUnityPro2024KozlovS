using System;

namespace Client
{
    [Serializable]
    public struct TeamId
    {
        public Team Value;
    }

    public enum Team
    {
        Blue = 1,
        Red = 2
    }
}