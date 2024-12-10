using System;
using Game.Gameplay;
using UnityEngine;

[Serializable]
public class AnimatorComponent
{
    public event Action OnAnimationEnd;
    public event Action OnStrike;
    public event Action OnCast;
    public Animator Animator { get; }
    public AnimationDispatcher AnimationDispatcher { get; }

    public AnimatorComponent(EntityView entityView)
    {
        this.Animator = entityView.Animator;
        this.AnimationDispatcher = entityView.AnimationDispatcher;
        this.AnimationDispatcher.SubscribeOnEvent("Strike", OnStrikeDispatched);
        this.AnimationDispatcher.SubscribeOnEvent("Cast", OnCastDispatched);
        this.AnimationDispatcher.SubscribeOnEvent("AnimationEnd", OnAnimationEndDispatched);
    }

    public void PlayAnimation(string animationName)
    {
        this.Animator.SetTrigger(animationName);
    }

    private void OnStrikeDispatched()
    {
        OnStrike?.Invoke();
    }

    private void OnCastDispatched()
    {
        OnCast?.Invoke();
    }

    private void OnAnimationEndDispatched()
    {
        OnAnimationEnd?.Invoke();
    }
}