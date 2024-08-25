using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class ResourceSaveLoader : SaveLoader<ResourceService, Dictionary<string, ResourceData>>
    {
        [SerializeField] private bool isErrorOnExcessiveSaveData = true;
        [SerializeField] private bool isErrorOnNoSaveDataForResource = true;
        protected override Dictionary<string, ResourceData> ConvertToData(ResourceService resourceService)
        {
            Dictionary<string, ResourceData> resourceDataCollection = new Dictionary<string, ResourceData>();
            var resources = resourceService.GetResources();
            foreach (Resource resource in resources)
            {
                string key = resource.ID;
                ResourceData resourceData = new ResourceData
                {
                    Amount = resource.Amount
                };
                resourceDataCollection.Add(key, resourceData);
            }
            return resourceDataCollection;
        }

        protected override void SetupData(ResourceService resourceService, Dictionary<string, ResourceData> resourceDataCollection)
        {
            var resources = resourceService.GetResources();
            foreach (Resource resource in resources)
            {
                string key = resource.ID;
                if (resourceDataCollection.ContainsKey(key))
                {
                    ResourceData resourceData = resourceDataCollection[key];
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