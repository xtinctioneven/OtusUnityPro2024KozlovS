using System.Collections.Generic;
using System.Linq;
using UI;
using Zenject;

public class HeroTrackerService: IInitializable
{
    private EventBus _eventBus;
    private readonly UIService _uiService;
    private List<HeroEntity> _redTeamHeroes = new();
    private List<HeroEntity> _blueTeamHeroes = new();
    private HeroEntity _activeHero;
    
    public HeroTrackerService(
        UIService uiService,
        EventBus eventBus
    )
    {
        _eventBus = eventBus;
        _uiService = uiService;
    }

    public void Initialize()
    {
        var redHeroListView = _uiService.GetRedPlayer().GetViews();
        for (int i = 0; i < redHeroListView.Count(); i++)
        {
            AddHeroToTeam(redHeroListView[i].GetComponent<HeroEntity>(), Team.RedTeam);
        }
        var blueHeroListView = _uiService.GetBluePlayer().GetViews();
        for (int i = 0; i < blueHeroListView.Count(); i++)
        {
            AddHeroToTeam(blueHeroListView[i].GetComponent<HeroEntity>(), Team.BlueTeam);
        }
    }

    public void Clear()
    {
        _redTeamHeroes = null;
        _blueTeamHeroes = null;
        _activeHero = null;
    }

    public HeroEntity GetActiveHero()
    {
        return _activeHero;
    }

    public void AddHeroToTeam(HeroEntity hero, Team team)
    {
        if (Team.RedTeam == team)
        {
            _redTeamHeroes.Add(hero);
        }
        else
        {
            _blueTeamHeroes.Add(hero);
        }
    }

    public void RemoveHero(HeroEntity hero)
    {
        if (_redTeamHeroes.Contains(hero))
        {
            _redTeamHeroes.Remove(hero);
        }
        else
        {
            _blueTeamHeroes.Remove(hero);
        }
    }

    public IReadOnlyList<HeroEntity> GetRedTeam()
    {
        return _redTeamHeroes;
    }

    public IReadOnlyList<HeroEntity> GetBlueTeam()
    {
        return _blueTeamHeroes;
    }

    public IReadOnlyList<HeroEntity> GetHeroesFromTeam(Team team)
    {
        if (team == Team.RedTeam)
        {
            return GetRedTeam();
        }
        else
        {
            return GetBlueTeam();
        }
    }
}