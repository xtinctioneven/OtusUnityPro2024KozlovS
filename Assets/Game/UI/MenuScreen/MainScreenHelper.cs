using System.Collections;
using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine;

public class MainScreenHelper : MonoBehaviour
{
    public static MainScreenHelper Instance;
    public List<CharacterConfig> SelectedCharacters = new List<CharacterConfig>();
    public List<Vector2> SelectedCharacterPositions = new List<Vector2>();
    
    void Awake()
    {
        Instance = this;
    }

    public void SelectCharacter(CharacterConfig newCharacter)
    {
        SelectedCharacters.Add(newCharacter);
        SelectedCharacterPositions.Add(Vector2.zero);
    }

    public void DeselectCharacter(CharacterConfig character)
    {
        int index = SelectedCharacters.IndexOf(character);
        SelectedCharacters.Remove(character);
        SelectedCharacterPositions.RemoveAt(index);
    }

    public void ChangePosition(CharacterConfig character, Vector2 newPosition)
    {
        int index = SelectedCharacters.IndexOf(character);
        SelectedCharacterPositions[index] = newPosition;
    }
}
