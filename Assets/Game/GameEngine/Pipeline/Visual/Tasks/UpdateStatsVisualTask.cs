using UI;
using UnityEngine;

public class UpdateStatsVisualTask : EventTask
{
    private HealthViewComponent _healthView;
    private int _newValue;
    
    public UpdateStatsVisualTask(HealthViewComponent healthView, int newValue)
    {
        _healthView = healthView;
        _newValue = newValue;
    }

    protected override void OnRun()
    {
        if (_healthView != null)
        {
            _healthView.UpdateCurrentHealth(_newValue);
        }
        Finish();
    }
}