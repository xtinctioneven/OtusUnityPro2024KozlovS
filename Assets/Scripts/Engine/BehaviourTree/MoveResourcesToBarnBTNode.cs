using System;
using Atomic.AI;

namespace Game.Engine
{
    [Serializable]
    public class MoveResourcesToBarnBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            var character = blackboard.GetCharacter();
            var characterStorage = character.GetComponent<ResourceStorageComponent>();

            var barn = blackboard.GetBarn();
            var barnStorage = barn.GetComponent<ResourceStorageComponent>();

            return MoveResources(characterStorage, barnStorage);
        }

        private static BTResult MoveResources(ResourceStorageComponent fromStorage, ResourceStorageComponent toStorage)
        {
            if (toStorage.FreeSlots == 0)
            {
                return BTResult.FAILURE;
            }

            var resourcesToAdd = Math.Min(toStorage.FreeSlots, fromStorage.Current);
            fromStorage.RemoveResources(resourcesToAdd);
            toStorage.AddResources(resourcesToAdd);

            return BTResult.SUCCESS;
        }
    }
}