namespace  Lessons.Architecture.PM
{
    public class CharacterStatPresenterFactory 
    {
        private readonly PlayerLevel _playerLevel;
        
        public ICharacterStatPresenter Create(CharacterStat characterStat, IPlayerLevelPresenter playerLevelPresenter)
        {
            return new CharacterStatPresenter(characterStat, playerLevelPresenter);
        }
    }
}