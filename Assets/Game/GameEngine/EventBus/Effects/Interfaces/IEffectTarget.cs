namespace Game.Gameplay
{
    public interface IEffectTarget
    {
        TargetType TargetType {get; set;}
        TargetPriorityType Priority {get; set;}
        //IEntity[] TargetEntities { get; set; }
    }
}