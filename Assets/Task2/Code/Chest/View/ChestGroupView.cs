using System.Collections.Generic;
using UnityEngine;

public class ChestGroupView: MonoBehaviour
{
    [SerializeField] private ChestView _chestViewPrefab;
    [SerializeField] private Transform _chestViewsContainer;
    private List<ChestView> _chestViews = new();

    public void Show(List<ChestPresenter> chestPresenters)
    {
        ClearViews();
        _chestViews = new List<ChestView>();
        foreach (var chestPresenter in chestPresenters)
        {
            ChestView chestView = Instantiate(_chestViewPrefab, _chestViewsContainer);
            chestView.name = _chestViewPrefab.name + " " +(_chestViews.Count + 1);
            chestView.Show(chestPresenter);
            _chestViews.Add(chestView);
        }
    }

    public IEnumerable<ChestView> GetViews()
    {
        return _chestViews;
    }

    private void ClearViews()
    {
        foreach (var chestView in _chestViews)
        {
            Destroy(chestView.gameObject);
        }
        _chestViews.Clear();
    }
}