using Leopotam.EcsLite.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Client
{
    public class CharacterMoveController : MonoBehaviour
    {
        [SerializeField] private Entity _character;

        private void Update()
        {
            Vector3 inputDirection = MoveInput.GetDirection();
            ref MoveDirection moveDirection = ref _character.GetData<MoveDirection>();
            moveDirection.Value = inputDirection;
        }
    }
}