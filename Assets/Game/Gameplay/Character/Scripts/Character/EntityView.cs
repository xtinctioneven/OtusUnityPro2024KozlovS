using UnityEngine;

namespace Game.Gameplay
{
    public class EntityView : MonoBehaviour
    {
        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }
    }
}