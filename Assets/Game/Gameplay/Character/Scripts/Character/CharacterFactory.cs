namespace Game.Gameplay
{
    public class CharacterFactory
    {
        public CharacterEntity CreateCharacter(CharacterConfig config)
        {
            CharacterEntity characterEntity = new(config);
            return characterEntity;
        }
    }
}