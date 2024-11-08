using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Zenject;

namespace SampleGame
{
    public sealed class MenuLoader: IInitializable
    {
        public async Task LoadMenu()
        {
            await Addressables.LoadSceneAsync("Menu", LoadSceneMode.Additive).Task;
        }

        public void Initialize()
        {
            Addressables.LoadAssetAsync<GameObject>("MenuScreen");
        }
    }
}