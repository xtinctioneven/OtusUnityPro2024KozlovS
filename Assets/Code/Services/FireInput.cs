using UnityEngine;

namespace Client
{
    public static class FireInput
    {
        public static bool IsFirePressDown()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}