using System.Collections.Generic;

namespace SaveSystem
{
    public class ResourcesSaveData
    {
        public Dictionary<string, ResourceData> ResourceDataCollection = new Dictionary<string, ResourceData>();
        public struct ResourceData
        {
            public int Amount;
        }
    }
}