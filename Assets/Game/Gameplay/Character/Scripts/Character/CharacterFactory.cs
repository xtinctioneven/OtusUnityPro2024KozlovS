using UnityEngine;

namespace Game.Gameplay
{
    public class CharacterFactory
    {
        public CharacterEntity CreateCharacter(CharacterConfig config)
        {
            EntityView entityView = GameObject.Instantiate(config.EntityViewPrefab);
            CharacterEntity characterEntity = new(config, entityView);
            return characterEntity;
        }
    }
}