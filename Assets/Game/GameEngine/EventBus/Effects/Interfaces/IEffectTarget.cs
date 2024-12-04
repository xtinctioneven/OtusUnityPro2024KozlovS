namespace Game.Gameplay
{
    public interface IEffectTarget: IEffect
    {
        TargetType TargetType { get; }
        TargetPriorityType TargetPriority { get; }
        int TargetsCount { get; }
    }
}