using Game.Gameplay;

public class EntityDeathVisualTask : EventTask
{
    private readonly IEntity _entity;

    public EntityDeathVisualTask(IEntity entity)
    {
        _entity = entity;
    }

    protected override void OnRun()
    {
        _entity.GetEntityComponent<AnimatorComponent>().Animator.Play("Death");
        _entity.GetEntityComponent<HealthViewComponent>().DisableView();
        Finish();
    }
}