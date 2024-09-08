using TMPro;
using UnityEngine;
using Zenject;

public class PlayerStatsView: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hitPointsText;
    [SerializeField] private TextMeshProUGUI _bulletsText;
    [SerializeField] private TextMeshProUGUI _killsText;
    private PlayerStatsPresenter _playerStatsPresenter;

    [Inject]
    private void Construct(PlayerStatsPresenter playerStatsPresenter)
    {
        _playerStatsPresenter = playerStatsPresenter;
        _playerStatsPresenter.OnHitPointsChanged += UpdateHitPointsText;
        _playerStatsPresenter.OnBulletsChanged += UpdateBulletsText;
        _playerStatsPresenter.OnKillsChanged += UpdateKillsText;
    }

    private void UpdateHitPointsText(string newText)
    {
        _hitPointsText.text = newText;
    }

    private void UpdateBulletsText(string newText)
    {
        _bulletsText.text = newText;
    }

    private void UpdateKillsText(string newText)
    {
        _killsText.text = newText;
    }
}