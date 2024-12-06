using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class CharacterEntity : IEntity
    {
        private CharacterConfig _characterConfig;
        private readonly Dictionary<Type, object> _components = new();
        private string _characterName;
        public string Name => _characterName;

        public CharacterEntity(CharacterConfig config)
        {
            _characterConfig = config;
            _characterName = config.CharacterName;
            StatsComponent statsComponent = new StatsComponent(config.StatsData);
            statsComponent.TryGetStat(StatType.Attack, out Stat attackStat);
            AttackComponent attackComponent = new AttackComponent(attackStat);
            statsComponent.TryGetStat(StatType.Defence, out Stat defenceStat);
            DefenceComponent defenceComponent = new DefenceComponent(defenceStat);
            statsComponent.TryGetStat(StatType.Health, out Stat healthStat);
            HealthComponent healthComponent = new HealthComponent(healthStat);
            statsComponent.TryGetStat(StatType.Initiative, out Stat initiativeStat);
            SpeedComponent speedComponent = new SpeedComponent(initiativeStat);
            DeathComponent deathComponent = new DeathComponent();
            AbilityComponent abilityComponent = new AbilityComponent(config.Abilities);
            abilityComponent.Install(this);
            TeamComponent teamComponent = new TeamComponent();
            GridPositionComponent gridPositionComponent = new GridPositionComponent(new Vector2(-1, -1));
            LinkComponent linkComponent = new LinkComponent();
            StatusEffectsComponent statusEffectsComponent = new StatusEffectsComponent(this);

            _components.Add(typeof(StatsComponent), statsComponent);
            _components.Add(typeof(AttackComponent), attackComponent);
            _components.Add(typeof(DefenceComponent), defenceComponent);
            _components.Add(typeof(HealthComponent), healthComponent);
            _components.Add(typeof(SpeedComponent), speedComponent);
            _components.Add(typeof(DeathComponent), deathComponent);
            _components.Add(typeof(AbilityComponent), abilityComponent);
            _components.Add(typeof(TeamComponent), teamComponent);
            _components.Add(typeof(GridPositionComponent), gridPositionComponent);
            _components.Add(typeof(LinkComponent), linkComponent);
            _components.Add(typeof(StatusEffectsComponent), statusEffectsComponent);
            // viewComponentOld.Value.SetStats($"<color=blue>{attackComponent.Value}</color> / <color=red>{lifeComponent.Value}</color>");
            // abilityComponent.Install();
            // vfxComponent.Install(abilityComponent.GetAbilities(), anchorComponent.Value);
            // viewComponentOld.Value.gameObject.name += $" {config.HeroName}";
        }

        public TType GetEntityComponent<TType>()
        {
            return (TType)_components.GetValueOrDefault(typeof(TType));
        }

        public void RemoveComponent<TType>()
        {
            var component = _components.GetValueOrDefault(typeof(TType));
            if (component != null)
            {
                _components.Remove(typeof(TType));
            }
        }

        public void RemoveAllComponents()
        {
            _components.Clear();
        }
    }
}