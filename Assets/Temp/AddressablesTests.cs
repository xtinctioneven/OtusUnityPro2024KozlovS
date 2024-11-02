using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesTests : MonoBehaviour
{
    private string tileName = "Tile";
    private string treeName = "Tree";
    List<AsyncOperationHandle<Sprite>> loadedSprites = new();

    private void Start()
    {
        Test();
        Test2()
    }

    public async void Test()
    {
        var tile = await Addressables.LoadAssetAsync<GameObject>(tileName).Task;
        var tree = await Addressables.LoadAssetAsync<GameObject>(treeName).Task;
        Debug.Log(tile);
        Debug.Log(tree);
    }

    public async void Test2()
    {
        var list = new List<Task<Sprite>>();
        list.Add(Addressables.LoadAssetAsync<Sprite>($"{tileName}.png").Task);
        await Task.WhenAll(list);
        for (var i = 0; i < list.Count; i++)
        {
            //            Progress.Item.Render
        }
    }

    public async void Test3()
    { }
}
