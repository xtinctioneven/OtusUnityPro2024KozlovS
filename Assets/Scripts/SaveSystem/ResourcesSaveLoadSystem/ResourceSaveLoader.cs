using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class ResourceSaveLoader : SaveLoader<ResourceService, ResourcesSaveData>
    {
        [SerializeField] private bool isErrorOnExcessiveSaveData = true;
        [SerializeField] private bool isErrorOnNoSaveDataForResource = true;
        protected override ResourcesSaveData ConvertToData(ResourceService resourceService)
        {
            ResourcesSaveData resourcesSaveData = new ResourcesSaveData();
            List<Resource> resources = resourceService.GetResources().ToList();
            foreach (Resource resource in resources)
            {
                string key = resource.ID;
                ResourcesSaveData.ResourceData resourceData = new ResourcesSaveData.ResourceData
                {
                    Amount = resource.Amount
                };
                resourcesSaveData.ResourceDataCollection.Add(key, resourceData);
            }
            return resourcesSaveData;
        }

        protected override void SetupData(ResourceService resourceService, ResourcesSaveData resourcesSaveData)
        {
            var resources = resourceService.GetResources();
            Dictionary<string, ResourcesSaveData.ResourceData> resourceDataCollection = resourcesSaveData.ResourceDataCollection;
            foreach (Resource resource in resources)
            {
                string key = resource.ID;
                if (resourceDataCollection.ContainsKey(key))
                {
                    ResourcesSaveData.ResourceData resourceData = resourceDataCollection[key];
                    resource.Amount = resourceData.Amount;
                    resourceDataCollection.Remove(key);
                }
                else if (isErrorOnNoSaveDataForResource)
                {
                    throw new ArgumentException($"Could not find appropriate ResourceData to load for Resource with Id: {key}");
                }
            }

            if (isErrorOnExcessiveSaveData && resourceDataCollection.Count > 0)
            {
                throw new Exception($"Excessive save data! There is no Resource to load data into! " +
                                    $"There are {resourceDataCollection.Count} resources to load!");
            }
        }
    }
}