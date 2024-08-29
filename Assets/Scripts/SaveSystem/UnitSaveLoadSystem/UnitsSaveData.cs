using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public class UnitsSaveData
    {
        public Dictionary<string, UnitData> UnitDataCollection = new Dictionary<string, UnitData>();

        public struct UnitData
        {
            public string Id;
            public string Type;
            public Vector3 Position;
            public Vector3 Rotation;
            public int HitPoints;
        }
    }
}