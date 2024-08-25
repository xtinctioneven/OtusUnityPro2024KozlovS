using System.Collections;
using System.Collections.Generic;
using GameEngine;
using UnityEngine;
using Zenject;

public class SceneSetup : MonoBehaviour
{
    [Inject]
    public void Construct(IEnumerable<Unit> units, 
        UnitManager unitManager,
        IEnumerable<Resource> resources,
        ResourceService resourceService
        )
    {
        unitManager.SetupUnits(units);
        resourceService.SetResources(resources);
    }
}
