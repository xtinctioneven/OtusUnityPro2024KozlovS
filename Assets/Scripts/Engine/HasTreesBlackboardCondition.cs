using Atomic.AI;
using Game;
using Game.Engine;
using UnityEngine;

namespace Engine
{
    public sealed class HasTreesBlackboardCondition : IBlackboardCondition
    {
        public bool Invoke(IBlackboard blackboard)
        {
            if (!blackboard.TryGetTreeService(out TreeService treeService))
            {
                return false;
            }
            return treeService.FindClosest(Vector3.zero, out GameObject tree);
        }
    }
}