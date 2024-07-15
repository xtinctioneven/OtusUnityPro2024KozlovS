using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{   
    public class Enemy : Unit, IInitializable
    {
        private EnemyAttackAgent _enemyAttackAgent;
        private EnemyMoveAgent _enemyMoveAgent;

        public EnemyAttackAgent EnemyAttackAgent { get { return _enemyAttackAgent; } }
        public EnemyMoveAgent EnemyMoveAgent { get { return _enemyMoveAgent; } }

        [Inject]
        private void Construct(
            EnemyAttackAgent enemyAttackAgent,
            EnemyMoveAgent enemyMoveAgent
            )
        {
            _enemyAttackAgent = enemyAttackAgent;
            _enemyMoveAgent = enemyMoveAgent;
        }

        [Inject]
        public void Initialize()
        {
            _enemyAttackAgent.Condition
                .Append(HitPointsComponent.IsHitPointsExists)
                .Append(EnemyMoveAgent.IsReached);
        }

    }
}
