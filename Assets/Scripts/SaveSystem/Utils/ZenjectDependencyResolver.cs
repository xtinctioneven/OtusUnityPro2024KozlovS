using Zenject;

namespace SaveSystem
{
    public class ZenjectDependencyResolver : IDependencyResolver
    {
        private DiContainer diContainer;

        ZenjectDependencyResolver(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public TDependency Resolve<TDependency>()
        {
            return this.diContainer.Resolve<TDependency>();
        }
    }
    
    public interface IDependencyResolver
    {
        public TDependency Resolve<TDependency>();
    }
}