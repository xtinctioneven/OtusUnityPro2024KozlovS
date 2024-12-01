using System;
using Game.Gameplay;
using UnityEngine;

public class StatsResolveHandler: BaseHandler<StatsResolveEvent>
{
    
    public StatsResolveHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(StatsResolveEvent evt)
    {
       
    }
}