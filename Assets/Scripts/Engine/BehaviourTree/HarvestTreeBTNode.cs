using System;
using Atomic.AI;
using Game.Content;
using UnityEngine;
using Tree = Game.Content.Tree;

namespace Game.Engine
{
    [Serializable]
    public sealed class HarvestTreeBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            HarvestComponent harvestComponent = blackboard.GetCharacter().GetComponent<HarvestComponent>();
            ResourceStorageComponent resourceStorageComponent = blackboard.GetCharacter().GetComponent<ResourceStorageComponent>();

            GameObject tree = blackboard.GetTarget();

            if (!tree.activeInHierarchy && resourceStorageComponent.IsNotFull())
            {
                return BTResult.FAILURE;
            }
            
            if (resourceStorageComponent.IsNotFull() && tree.activeInHierarchy)
            {
                harvestComponent.Harvest();
                return BTResult.RUNNING;
            }
            return BTResult.SUCCESS;
        }
    }
}