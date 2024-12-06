namespace Game.Gameplay
{
    public interface IStatusEffect : IEvent
    {
        public IEntity AfflictedEntity { get; set; }
        public IStatusEffect Clone();
        public EntityInteractionData InteractionData { get; set; }
    }
}