using System;

namespace Game.Gameplay
{
    [Serializable]
    public class LinkStatusEffect : IStatusEffect
    {
        public LinkStatusType LinkStatus;
        public IEntity AfflictedEntity { get; set; }
        public EntityInteractionData InteractionData { get; set; }

        public IStatusEffect Clone()
        {
            LinkStatusEffect clone = new LinkStatusEffect();
            clone.AfflictedEntity = this.AfflictedEntity;
            clone.LinkStatus = this.LinkStatus;
            return clone;
        }
    }
}