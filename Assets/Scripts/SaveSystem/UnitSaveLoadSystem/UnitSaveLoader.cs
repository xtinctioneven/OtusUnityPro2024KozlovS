using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class UnitSaveLoader : SaveLoader<UnitManager, Dictionary<string, UnitData>>
    {
        [SerializeField] private bool isErrorOnExcessiveSaveData = true;
        [SerializeField] private bool isErrorOnNoSaveDataForUnit = true;
        protected override Dictionary<string, UnitData> ConvertToData(UnitManager unitManager)
        {
            Dictionary<string, UnitData> unitDataCollection = new Dictionary<string, UnitData>();
            List<Unit> units = unitManager.GetAllUnits().ToList();
            foreach (Unit unit in units)
            {
                var key = unit.gameObject.name;
                UnitData unitData = new UnitData();
                unitData.Type = unit.Type;
                unitData.Position = unit.Position;
                unitData.Rotation = unit.Rotation;
                unitData.HitPoints = unit.HitPoints;
                unitDataCollection.Add(key, unitData);
            }
            return unitDataCollection;
        }

        protected override void SetupData(UnitManager unitManager, Dictionary<string, UnitData> unitDataCollection)
        {
            var units = unitManager.GetAllUnits();
            foreach (var unit in units)
            {
                string key = unit.gameObject.name;
                if (unitDataCollection.ContainsKey(key))
                {
                    UnitData unitData = unitDataCollection[key];
                    if (unitData.Type != unit.Type)
                    {
                        throw new ArgumentException($"Unit Type mismatch! Type in Unit: {unit.Type}, Type in UnitData: {unitData.Type}");
                    }
                    unit.transform.rotation = Quaternion.Euler(unitData.Rotation);
                    unit.transform.position = unitData.Position;
                    unit.HitPoints = unitData.HitPoints;
                    unitDataCollection.Remove(key);
                }
                else if (this.isErrorOnNoSaveDataForUnit)
                {
                    throw new ArgumentException($"Could not find appropriate UnitData to load for Unit with name: {key}");
                }
            }

            if (this.isErrorOnExcessiveSaveData && unitDataCollection.Count > 0)
            {
                throw new Exception($"Excessive save data! There is no Unit to load data into! " +
                                    $"There are {unitDataCollection.Count} units to load!");
            }
        }
    }
}