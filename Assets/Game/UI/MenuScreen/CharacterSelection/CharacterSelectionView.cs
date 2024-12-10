using System;
using Game.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionView : MonoBehaviour
{
    public event Action<CharacterConfig, CharacterSelectionView> OnCharacterSelected;
    public event Action<CharacterConfig> OnCharacterDeselected;
    public event Action<CharacterConfig, Vector2> OnCharacterPositionChanged;
    public bool IsSelected => _highlightToggle.isOn;
    [SerializeField] private Toggle _highlightToggle;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private Image _characterImage;
    [SerializeField] private TMP_Dropdown _positionDropdown;
    [SerializeField] private GameObject _lockView;
    private CharacterConfig _characterConfig;

    private void Awake()
    {
        _lockView.SetActive(true);
        _positionDropdown.gameObject.SetActive(false);
        _highlightToggle.interactable = false;
    }

    public void Setup(CharacterConfig characterConfig)
    {
        _characterConfig = characterConfig;
        _characterImage.sprite = _characterConfig.CharacterIcon;
        _lockView.SetActive(false);
        _highlightToggle.interactable = true;
        _highlightToggle.onValueChanged.AddListener(OnToggle);
    }

    public void OnToggle(bool isOn)
    {
        _highlight.SetActive(isOn);
        _positionDropdown.gameObject.SetActive(isOn);
        if (isOn)
        {
            _positionDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
            OnCharacterSelected?.Invoke(_characterConfig, this);
        }
        else
        {
            _positionDropdown.onValueChanged.RemoveAllListeners();
            OnCharacterDeselected?.Invoke(_characterConfig);
        }
    }

    public void Toggle(bool isOn)
    {
        _highlightToggle.isOn = isOn;
    }

    private void OnDropdownValueChanged(int value)
    {
        int x = value % 3;
        int y = value / 3;
        Vector2 position = new Vector2(x, y);
        OnCharacterPositionChanged?.Invoke(_characterConfig, position);
    }
}