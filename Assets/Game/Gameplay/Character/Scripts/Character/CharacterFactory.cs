using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class CharacterFactory
    {
        public CharacterEntity CreateCharacter(CharacterConfig config)
        {
            CharacterEntity characterEntity = new(config);
            EntityView entityView = GameObject.Instantiate(config.EntityViewPrefab);
            entityView.name = config.CharacterName;
            entityView.Setup(config.CharacterVisualPrefab);
            characterEntity.AttachView(entityView);
            return characterEntity;
        }
    }
}