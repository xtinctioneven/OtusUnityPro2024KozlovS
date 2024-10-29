using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ChestManager : MonoBehaviour
{
    private List<ChestModel> _chests;
    private ChestGroupView _chestGroupView;
    private ChestConfigCollection _chestConfigCollection;
    
    private ChestModelFactory _chestFactory;
    private ChestPresenterFactory _chestPresenterFactory;
    private ChestTimerService _chestTimerService;
    
    [Inject]
    public void Construct(ChestModelFactory chestFactory,
        ChestConfigCollection chestsConfigCollection,
        ChestGroupView chestGroupView,
        ChestPresenterFactory chestPresenterFactory,
        ChestTimerService chestTimerService)
    {
        _chestConfigCollection = chestsConfigCollection;
        _chestFactory = chestFactory;
        _chestGroupView = chestGroupView;
        _chestPresenterFactory = chestPresenterFactory;
        _chestTimerService = chestTimerService;
    }

    private void Start()
    {
        _chests = new List<ChestModel>();
        List<ChestPresenter> chestPresenters = new List<ChestPresenter>();
        foreach (var chestConfig in _chestConfigCollection.Configs)
        {
            var chest = _chestFactory.Create(chestConfig);
            _chests.Add(chest);
            var chestPresenter = _chestPresenterFactory.Create(chest);
            chestPresenters.Add(chestPresenter);
        }
        _chestTimerService.Initialize(_chests);
        _chestGroupView.Show(chestPresenters);
        foreach (var chestView in _chestGroupView.GetViews())
        {
            chestView.OpenChestAction += OpenChest;
        }
    }

    private void OpenChest(ChestModel chest)
    {
        Debug.Log("Open chest " + chest.Id);
        foreach (var reward in chest.Rewards)
        {
            reward.GetReward();
        }
        chest.RestartCountdown();
    }
}