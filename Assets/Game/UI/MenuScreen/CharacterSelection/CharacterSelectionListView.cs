using System;
using System.Collections.Generic;
using Game.Gameplay;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CharacterSelectionListView : MonoBehaviour
{
    [SerializeField] private CharacterSelectionView _characterSelectionViewPrefab;
    private TeamConfig _teamConfig;
    private CharacterSelectionView[] _characterSelectionViews = new CharacterSelectionView[5];
    private int _selectedCharactersCount = 0;
    private CharacterSelectionService _characterSelectionService; 

    [Inject]
    public void Construct(
        [Inject(Id = "AvailableHeroes")] TeamConfig teamConfig,
        CharacterSelectionService characterSelectionService
    )
    {
        _teamConfig = teamConfig;
        _characterSelectionService = characterSelectionService;
    }
    
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            CharacterSelectionView characterSelectionView = Instantiate(_characterSelectionViewPrefab, transform);
            characterSelectionView.name = "CharacterSelectionView " + i;
            if (i < _teamConfig.characters.Count)
            {
                characterSelectionView.Setup(_teamConfig.characters[i]);
                characterSelectionView.OnCharacterSelected += OnCharacterSelected;
                characterSelectionView.OnCharacterPositionChanged += OnCharacterPositionChanged;
                characterSelectionView.OnCharacterDeselected += OnCharacterDeselected;
                characterSelectionView.name += " " + _teamConfig.characters[i].name;
            }
            
            _characterSelectionViews[i] = characterSelectionView;
        }
    }

    private void OnCharacterDeselected(CharacterConfig deselectedCharacter)
    {
        _selectedCharactersCount--;
        _characterSelectionService.DeselectCharacter(deselectedCharacter);
    }

    private void OnCharacterPositionChanged(CharacterConfig characterConfig, Vector2 newPostion)
    {
        _characterSelectionService.ChangePosition(characterConfig, newPostion);
    }

    private void OnCharacterSelected(CharacterConfig selectedCharacter, CharacterSelectionView characterSelectionView)
    {
        _selectedCharactersCount++;
        _characterSelectionService.SelectCharacter(selectedCharacter);
        if (_selectedCharactersCount <= 3)
        {
            return;
        }
        characterSelectionView.Toggle(false);
    }
}