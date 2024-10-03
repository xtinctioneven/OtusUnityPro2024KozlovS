using System.Collections.Generic;
using UI;
using UnityEngine;

public class HeroesClashTask : EventTask
{
    private readonly HeroInteractionService _heroInteractionService;
    private readonly EventBus _eventBus;

    public HeroesClashTask(
        HeroInteractionService heroInteractionService,
        EventBus eventBus)
    {
        _heroInteractionService = heroInteractionService;
        _eventBus = eventBus;
    }

    protected override void OnRun()
    {
        Debug.Log("HeroesInteractionTask.OnRun");
        HeroInteractionData
            heroClashData = _heroInteractionService.CreateCurrentClashData();
        //TargetHero не кэшируется, т.к. может поменяться в рамках активации способностей
        HeroEntity activeHero = heroClashData.SourceHero;
        _eventBus.RaiseEvent(new ActivateAbilitiesEvent(TurnPhase.HeroActionStart, activeHero, heroClashData));
        _eventBus.RaiseEvent(new ActivateAbilitiesEvent(TurnPhase.HeroActionStart, heroClashData.TargetHero, heroClashData));
        //Activate source hero abilities of HeroesClash phase II
        heroClashData.TargetHeroDamageReceived = activeHero.GetHeroComponent<AttackComponent>().Value;
        heroClashData.SourceHeroDamageReceived = heroClashData.TargetHero.GetHeroComponent<AttackComponent>().Value;
        //Activate source hero abilities of HeroesClash phase III
        _eventBus.RaiseEvent(new ActivateAbilitiesEvent(TurnPhase.HeroesClash, activeHero, heroClashData));
        _eventBus.RaiseEvent(new ActivateAbilitiesEvent(TurnPhase.HeroesClash, heroClashData.TargetHero, heroClashData));
        _eventBus.RaiseEvent(new AttackEvent(activeHero, heroClashData.TargetHero));
        _eventBus.RaiseEvent(new HeroInteractionEvent(heroClashData));
        //Activate abilities of HeroActionEnd phase IV
        _eventBus.RaiseEvent(new ActivateAbilitiesEvent(TurnPhase.HeroActionEnd, activeHero, heroClashData));
        _eventBus.RaiseEvent(new ActivateAbilitiesEvent(TurnPhase.HeroActionEnd, heroClashData.TargetHero, heroClashData));
        
        Finish();
    }
}