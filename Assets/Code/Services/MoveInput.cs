using UnityEngine;

namespace Client
{
    public static class MoveInput
    {
        public static Vector3 GetDirection()
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                direction.z = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                direction.z = -1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                direction.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction.x = 1;
            }

            return direction;
        }
    }
}