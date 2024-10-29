using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    private const string CHEST_READY_TO_OPEN_TEXT = "Ready to open!";
    private const string CHEST_NOT_READY_TO_OPEN_TEXT = "Opening...";
    public event Action<ChestModel> OpenChestAction;
    [SerializeField] private Image _openChestImage; 
    [SerializeField] private Image _closedChestImage;
    [SerializeField] private TextMeshProUGUI _countdownText;
    [SerializeField] private Button _openChestButton;
    private ChestPresenter _chestPresenter;

    public void Show(ChestPresenter chestPresenter)
    {
        _chestPresenter = chestPresenter;
        _openChestImage.sprite = _chestPresenter.OpenChestImage;
        _closedChestImage.sprite = _chestPresenter.ClosedChestImage;
        SetCountdownText(_chestPresenter.TimeToOpenChest);
        
        _openChestImage.gameObject.SetActive(false);
        _closedChestImage.gameObject.SetActive(true);
        _openChestButton.onClick.RemoveAllListeners();
        _openChestButton.gameObject.SetActive(false);
        _chestPresenter.OnChestReadyToOpen += UnlockChest;
        _chestPresenter.OnTimeToOpenChanged += ChestPresenterOnTimeToOpenChanged;
        _chestPresenter.OnChestRestart += LockChest;
    }

    private void ChestPresenterOnTimeToOpenChanged(string newText)
    {
        SetCountdownText(newText);
    }

    private void SetCountdownText(string text)
    {
        _countdownText.text = text;
    }

    private void UnlockChest(bool isSafeToOpen)
    {
        _chestPresenter.OnTimeToOpenChanged -= ChestPresenterOnTimeToOpenChanged;
        if (isSafeToOpen)
        {
            SetCountdownText(CHEST_READY_TO_OPEN_TEXT);
            _openChestImage.gameObject.SetActive(true);
            _closedChestImage.gameObject.SetActive(false);
            _openChestButton.gameObject.SetActive(true);
            _openChestButton.onClick.AddListener(OpenChest);
        }
        else
        {
            SetCountdownText(CHEST_NOT_READY_TO_OPEN_TEXT);
            _openChestImage.gameObject.SetActive(false);
            _closedChestImage.gameObject.SetActive(true);
            _openChestButton.gameObject.SetActive(false);
            _openChestButton.onClick.RemoveAllListeners();
        }
    }

    private void OpenChest()
    {
        OpenChestAction?.Invoke(_chestPresenter.ChestModel);
    }

    private void LockChest()
    {
        _openChestImage.gameObject.SetActive(false);
        _closedChestImage.gameObject.SetActive(true);
        _openChestButton.gameObject.SetActive(false);
        _openChestButton.onClick.RemoveAllListeners();
        _chestPresenter.OnTimeToOpenChanged += ChestPresenterOnTimeToOpenChanged;
        
    }
}