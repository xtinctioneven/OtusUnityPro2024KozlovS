using Zenject;

namespace SaveSystem
{
    public interface ISaveLoader
    {
        void SaveGame(IDependencyResolver dependencyResolver, IGameRepository gameRepository);
        void LoadGame(IDependencyResolver dependencyResolver, IGameRepository gameRepository);
    }
}