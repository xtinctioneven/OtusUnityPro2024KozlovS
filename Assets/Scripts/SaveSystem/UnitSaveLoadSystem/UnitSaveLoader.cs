using System;
using System.Collections.Generic;
using System.Linq;
using Codice.Client.BaseCommands.BranchExplorer;
using GameEngine;
using UnityEditor;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class UnitSaveLoader : SaveLoader<UnitManager, UnitsSaveData>
    {
        const string UNITS_PREFAB_FOLDER = "Assets/Prefabs/UnitObjects";
        protected override UnitsSaveData ConvertToData(UnitManager unitManager)
        {
            UnitsSaveData unitsSaveData = new UnitsSaveData();
            List<Unit> units = unitManager.GetAllUnits().ToList();
            foreach (Unit unit in units)
            {
                var key = unit.gameObject.name;
                UnitsSaveData.UnitData unitData = new UnitsSaveData.UnitData();
                unitData.Type = unit.Type;
                unitData.Position = unit.Position;
                unitData.Rotation = unit.Rotation;
                unitData.HitPoints = unit.HitPoints;
                unitsSaveData.UnitDataCollection.Add(key, unitData);
            }
            return unitsSaveData;
        }

        protected override void SetupData(UnitManager unitManager, UnitsSaveData unitsSaveData)
        {
            var units = unitManager.GetAllUnits().ToList();
            Dictionary<string, UnitsSaveData.UnitData> unitDataCollection = unitsSaveData.UnitDataCollection;
            foreach (var unit in units)
            {
                string key = unit.gameObject.name;
                if (unitDataCollection.ContainsKey(key))
                {
                    UnitsSaveData.UnitData unitData = unitDataCollection[key];
                    if (unitData.Type != unit.Type)
                    {
                        throw new ArgumentException($"Unit Type mismatch! Type in Unit: {unit.Type}, Type in UnitData: {unitData.Type}");
                    }
                    unit.transform.rotation = Quaternion.Euler(unitData.Rotation);
                    unit.transform.position = unitData.Position;
                    unit.HitPoints = unitData.HitPoints;
                    unitDataCollection.Remove(key);
                }
                else
                {
                    unitManager.DestroyUnit(unit);
                }
            }

            if (unitDataCollection.Count > 0)
            {
                string[] prefabFolder = new []{UNITS_PREFAB_FOLDER};
                foreach (var unitDataPair in unitDataCollection)
                {
                    string prefabName = unitDataPair.Value.Type;
                    var guids = AssetDatabase.FindAssets(prefabName, prefabFolder);
                    if (guids.Length < 1)
                    {
                        Debug.LogError($"Failed to find prefab for unit: {prefabName}");
                    }
                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    Unit unitPrefab = AssetDatabase.LoadAssetAtPath<Unit>(path);
                    unitManager.SpawnUnit(unitPrefab, unitDataPair.Value.Position,
                        Quaternion.Euler(unitDataPair.Value.Rotation));
                }
                unitDataCollection.Clear();
            }
        }
    }
}