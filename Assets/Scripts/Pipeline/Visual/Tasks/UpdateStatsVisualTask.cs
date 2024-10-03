using UI;
using UnityEngine;

public class UpdateStatsVisualTask : EventTask
{
    private readonly HeroView _heroView;
    private readonly string _statsValue;
    private readonly AudioClip _audioClip;

    public UpdateStatsVisualTask(HeroView heroView, string stats, AudioClip audioClip = null)
    {
        _heroView = heroView;
        _statsValue = stats;
        _audioClip = audioClip;
    }

    protected override void OnRun()
    {
        if (_audioClip != null)
        {
            AudioPlayer.Instance.PlaySound(_audioClip);
        }
        _heroView.SetStats(_statsValue);
        Finish();
    }
}