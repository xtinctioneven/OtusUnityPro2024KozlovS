using Zenject;

namespace SaveSystem
{
    public interface ISaveLoader
    {
        void SaveGame(DiContainer diContainer, IGameRepository gameRepository);
        void LoadGame(DiContainer diContainer, IGameRepository gameRepository);
    }
}