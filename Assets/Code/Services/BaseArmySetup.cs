using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite.Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Client
{
    public class BaseArmySetup : MonoBehaviour
    {
        [SerializeField]
        private Material _armyMaterial;
        
        [SerializeField] 
        private List<Entity> _unitPrefabs = new List<Entity>();
        
        [SerializeField] 
        [OnValueChanged("OnSpawnPointsContainerChanged")] 
        private Transform _spawnPointsContainer;
        
        [SerializeField]
        private Transform _unitsContainer;
        
        [SerializeField] 
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Left")]
        [OnValueChanged("OnSpawnPointsChanged")]
        private List<Transform> _spawnPoints;
        
        [SerializeField]
        [VerticalGroup("Split/Right")]
        [ValueDropdown("UnitTypeDropdown")]
        private List<Entity> _unitsList = new List<Entity>();
        
        [SerializeField, HideInInspector] private Entity[] _spawnedUnits;
        private Entity _unitType;
        private Entity[] UnitTypeDropdown() { return _unitPrefabs.ToArray(); }

        private void OnSpawnPointsContainerChanged(Transform newValue)
        {
            _spawnPoints = new List<Transform>();
            _spawnPoints.AddRange(_spawnPointsContainer.GetComponentsInChildren<Transform>().Where(x => x != newValue));
            RefreshArmy();
        }

        private void OnSpawnPointsChanged()
        {
            RefreshArmy();   
        }
        
        private void RefreshArmy()
        {
            _unitsList = new List<Entity>();
            foreach (Transform spawnPoint in _spawnPoints)
            { 
                _unitsList.Add(_unitType);
            }
        }

        [Button]
        private void RefreshUnitsOnScene()
        {
            DestroyUnitsOnScene();
            SpawnUnitsOnScene();
        }

        [Button]
        private void DestroyUnitsOnScene()
        {
            if (_spawnedUnits != null)
            {
                for (int i = _spawnedUnits.Length-1; i >= 0; i--)
                {
                    if (_spawnedUnits[i] != null)
                    {
                        DestroyImmediate(_spawnedUnits[i].gameObject);
                    }
                }
                _spawnedUnits = null;
            }
        }

        private void SpawnUnitsOnScene()
        {
            _spawnedUnits = new Entity[_unitsList.Count];
            for (int i = 0; i < _unitsList.Count; i++)
            {
                if (_unitsList[i] == null)
                {
                    Debug.LogError($"Unit #{i} is null!");
                }
                _spawnedUnits[i] = Instantiate(_unitsList[i], _spawnPoints[i].position,
                    Quaternion.identity, _unitsContainer).GetComponent<Entity>();
                SetupUnit(_spawnedUnits[i], i);
            }
        }

        private void SetupUnit(Entity entity, int index)
        {
            Team team = this.GetComponent<BaseInstaller>().GetTeam();
            entity.GetComponent<UnitInstaller>().SetTeam(team);
            entity.gameObject.name = $"{team} Unit #{index + 1} - {_unitsList[index].name}";
            ApplyArmyMaterial(entity);
        }

        private void ApplyArmyMaterial(Entity unit)
        {
            unit.GetComponent<UnitInstaller>().Recolor(_armyMaterial);
        }

        [Button]
        private void RecolorArmy()
        {
            for (int i = 0; i < _unitsList.Count; i++)
            {
                ApplyArmyMaterial(_spawnedUnits[i]);
            }
        }

        [Button]
        private void ClearData()
        {
            DestroyUnitsOnScene();
            _unitsList.Clear();
            _spawnedUnits = null;
            _spawnPoints.Clear();
            _spawnPointsContainer = null;
        }
    }
}