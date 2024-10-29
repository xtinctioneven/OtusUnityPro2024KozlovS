namespace SaveSystem
{
    public abstract class SaveLoader<TService, TData> : ISaveLoader
    {
        public void SaveGame(IDependencyResolver dependencyResolver, IGameRepository gameRepository)
        {
            TService service = dependencyResolver.Resolve<TService>();
            TData data = ConvertToData(service);
            gameRepository.SetData(data);
        }

        public void LoadGame(IDependencyResolver dependencyResolver, IGameRepository gameRepository)
        {
            TService service = dependencyResolver.Resolve<TService>();
            if (gameRepository.TryGetData(out TData data))
            {
                SetupData(service, data);
            }
            else
            {
                SetupDefaultData(service);
            }
        }

        protected abstract TData ConvertToData(TService service);
        protected abstract void SetupData(TService service, TData data);

        protected virtual void SetupDefaultData(TService service)
        {
        }
    }
}