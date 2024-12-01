using UI;
using UnityEngine;

public class ActivateHeroVisualHandler: BaseHandler<ActivateHeroEvent>
{
    private readonly UIService _uiService;
    
    public ActivateHeroVisualHandler(EventBus eventBus, UIService uiService) : base(eventBus)
    {
        _uiService = uiService;
    }

    protected override void OnHandleEvent(ActivateHeroEvent evt)
    {
        HeroEntity heroEntity = evt.HeroEntity;
        // Team activeHeroTeam = heroEntity.GetHeroComponent<TeamComponent>().Value;
        // if (activeHeroTeam == Team.RedTeam)
        // {
        //     _uiService.GetRedPlayer().SetActive(true);
        //     _uiService.GetBluePlayer().SetActive(false);
        // }
        // else
        // {
        //     _uiService.GetBluePlayer().SetActive(true);
        //     _uiService.GetRedPlayer().SetActive(false);
        // }
        // heroEntity.GetHeroComponent<ViewComponentOld>().Value.SetActive(true);
        // AudioClip audioClip = heroEntity.GetHeroComponent<AudioComponent>().GetStartTurnSound(); 
        // AudioPlayer.Instance.PlaySound(audioClip);
    }
}