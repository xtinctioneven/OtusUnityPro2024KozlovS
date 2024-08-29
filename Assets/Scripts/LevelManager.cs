using System;
using System.Collections;
using System.Collections.Generic;
using GameEngine;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Unit = GameEngine.Unit;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform unitsContainer;
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private ResourceService resourceService;
    private IEnumerable<Unit> units;
    private IEnumerable<Resource> resources;
    
    [Inject]
    public void Construct(IEnumerable<Unit> units, 
        UnitManager unitManager,
        IEnumerable<Resource> resources,
        ResourceService resourceService
        )
    {
        this.unitManager = unitManager;
        this.resourceService = resourceService;
        this.units = units;
        this.resources = resources;
    }

    private void Start()
    {
        this.unitManager.SetContainer(unitsContainer);
        this.unitManager.SetupUnits(units);
        this.resourceService.SetResources(resources);
    }
}
