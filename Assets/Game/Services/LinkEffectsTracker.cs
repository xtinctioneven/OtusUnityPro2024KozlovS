using System.Collections.Generic;
using Game.Gameplay;
using Zenject;

public class LinkEffectsTracker
{
    private DiContainer _diContainer;
    private EntityTrackerService _entityTrackerService;
    private Dictionary<LinkStatusType, List<IEffectLink>> _linkAbilitiesCollectionLeft;
    private Dictionary<LinkStatusType, List<IEffectLink>> _linkAbilitiesCollectionRight;
    
    public LinkEffectsTracker(
        DiContainer diContainer
    )
    {
        _diContainer = diContainer;
    }

    public void Initialize()
    {
        _linkAbilitiesCollectionLeft = new ();
        _linkAbilitiesCollectionRight = new ();
        _entityTrackerService = _diContainer.Resolve<EntityTrackerService>();
        _entityTrackerService.OnEntityTracked += TrackEntity;
        _entityTrackerService.OnEntityUntracked += UntrackEntity;
    }

    public bool TryGetActiveLink(LinkStatusType linkStatusType, Team team, out IEffectLink effectLink)
    {
        effectLink = null;
        if (team == Team.Left)
        {
            if (TryGetLinkFromCollection(linkStatusType, _linkAbilitiesCollectionLeft, out effectLink))
            {
                return true;
            }
        }
        else
        {
            if (TryGetLinkFromCollection(linkStatusType, _linkAbilitiesCollectionRight, out effectLink))
            {
                return true;
            }
        }
        return false;
    }

    private bool TryGetLinkFromCollection(LinkStatusType linkStatusType,
        Dictionary<LinkStatusType, List<IEffectLink>> linkAbilitiesCollection, out IEffectLink effectLink)
    {
        effectLink = null;
        if (!linkAbilitiesCollection.ContainsKey(linkStatusType))
        {
            return false;
        }
        var possibleLinks = linkAbilitiesCollection[linkStatusType];
        for (int i = 0; i < possibleLinks.Count; i++)
        {
            if (!possibleLinks[i].CanBeUsed)
            {
                continue;
            }
            effectLink = possibleLinks[i];
            return true;
        }
        return false;
    }
    
    private void TrackEntity(IEntity entity)
    {
        var abilityComponent = entity.GetEntityComponent<AbilityComponent>();
        if (entity.GetEntityComponent<Game.Gameplay.TeamComponent>().Value == Team.Left)
        {
            AddAbilitiesToCollection(abilityComponent.GetAbilitiesByType<IEffectLink>(), _linkAbilitiesCollectionLeft);
        }
        else
        {
            AddAbilitiesToCollection(abilityComponent.GetAbilitiesByType<IEffectLink>(), _linkAbilitiesCollectionRight);
        }
    }

    private void AddAbilitiesToCollection(List<IEffectLink> abilities,
        Dictionary<LinkStatusType, List<IEffectLink>> linkAbilitiesCollection)
    {
        foreach (var linkAbility in abilities)
        {
            if (!linkAbilitiesCollection.ContainsKey(linkAbility.SeekLinkStatus))
            {
                linkAbilitiesCollection.Add(linkAbility.SeekLinkStatus, new List<IEffectLink>());
            }
            linkAbilitiesCollection[linkAbility.SeekLinkStatus].Add(linkAbility);
        } 
    }
    
    private void UntrackEntity(IEntity entity)
    {
        if (entity.GetEntityComponent<Game.Gameplay.TeamComponent>().Value == Team.Left)
        {
            RemoveAbilitiesFromCollection(entity, _linkAbilitiesCollectionLeft);
        }
        else
        {
            RemoveAbilitiesFromCollection(entity, _linkAbilitiesCollectionRight);
        }
    }

    private void RemoveAbilitiesFromCollection(IEntity entity,
        Dictionary<LinkStatusType, List<IEffectLink>> linkAbilitiesCollection)
    {
        List<LinkStatusType> seekedLinks = new();
        var abilityComponent = entity.GetEntityComponent<AbilityComponent>();
        foreach (var linkAbility in abilityComponent.GetAbilitiesByType<IEffectLink>())
        {
            if (!linkAbilitiesCollection.ContainsKey(linkAbility.SeekLinkStatus))
            {
                continue;
            }
            seekedLinks.Add(linkAbility.SeekLinkStatus);
        }
        for (int i = 0; i < seekedLinks.Count; i++)
        {
            foreach (var link in linkAbilitiesCollection[seekedLinks[i]])
            {
                if (link.SourceEntity == entity)
                {
                    linkAbilitiesCollection[seekedLinks[i]].Remove(link);
                }
            }
        }
    }
        
}