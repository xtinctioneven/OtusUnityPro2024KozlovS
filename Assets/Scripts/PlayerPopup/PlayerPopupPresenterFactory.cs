namespace  Lessons.Architecture.PM
{
    public class PlayerPopupPresenterFactory
    {
        private readonly CharacterStatPresenterFactory _characterStatPresenterFactory;

        PlayerPopupPresenterFactory(CharacterStatPresenterFactory characterStatPresenterFactory)
        {
            _characterStatPresenterFactory = characterStatPresenterFactory;
        }
        
        public IPlayerPopupPresenter Create(UserInfo userInfo, PlayerLevel playerLevel, CharacterInfo characterInfo)
        {
            return new PlayerPopupPresenter(userInfo, playerLevel, characterInfo, _characterStatPresenterFactory);
        }
    }
}
