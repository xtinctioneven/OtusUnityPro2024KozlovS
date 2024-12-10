using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class CharacterFactory
    {
        public CharacterEntity CreateCharacter(CharacterConfig config)
        {
            EntityView entityView = GameObject.Instantiate(config.EntityViewPrefab);
            entityView.name = config.CharacterName;
            entityView.Setup(config.CharacterVisualPrefab);
            CharacterEntity characterEntity = new(config, entityView);
            return characterEntity;
        }
    }
}