using System;

namespace Game.Gameplay
{
    public interface IEntity
    {
        public string Name { get; }
        public TType GetEntityComponent<TType>();
    }
}