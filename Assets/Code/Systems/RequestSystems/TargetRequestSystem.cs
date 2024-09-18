using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class TargetRequestSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<TargetRequest, TeamId, Position>, Exc<Inactive>> _sourceFilter;
        private readonly EcsFilterInject<Inc<Position, TeamId>, Exc<Inactive>> _possibleTargetsFilter;
        private readonly EcsPoolInject<AttackTargetEntity> _targetPool;
        
        public void Run (IEcsSystems systems) 
        {
            EcsPool<TargetRequest> targetRequestPool = _sourceFilter.Pools.Inc1;
            EcsPool<TeamId> sourceTeamIdPool = _sourceFilter.Pools.Inc2;
            EcsPool<Position> sourcePositionPool = _sourceFilter.Pools.Inc3;
            
            EcsPool<Position> targetPositionPool = _possibleTargetsFilter.Pools.Inc1;
            EcsPool<TeamId> targetTeamIdPool = _possibleTargetsFilter.Pools.Inc2;
            
            foreach (int sourceEntity in _sourceFilter.Value)
            {
                TeamId teamId = sourceTeamIdPool.Get(sourceEntity);
                Vector3 sourcePosition = sourcePositionPool.Get(sourceEntity).Value;
                float distanceToClosestTarget = float.MaxValue;
                int targetEntity = -1;
                foreach (int possibleTarget in _possibleTargetsFilter.Value)
                {
                    if (targetTeamIdPool.Get(possibleTarget).Value == teamId.Value)
                    {
                        continue;
                    }
                    Vector3 targetPosition = targetPositionPool.Get(possibleTarget).Value;
                    float tempDistance = (targetPosition - sourcePosition).magnitude;
                    if (tempDistance < distanceToClosestTarget)
                    {
                        targetEntity = possibleTarget;
                        distanceToClosestTarget = tempDistance;
                    }
                }

                if (targetEntity == -1)
                {
                    Debug.LogError("Target entity not found!");
                }
                else
                {
                    _targetPool.Value.Add(sourceEntity) = new AttackTargetEntity { Value = targetEntity };
                    targetRequestPool.Del(sourceEntity);
                }
            }
        }
    }
}