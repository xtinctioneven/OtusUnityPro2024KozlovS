using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Examples;
using Sirenix.Serialization;
using UnityEngine;

public class TeamGridPresenter : SerializedMonoBehaviour
{
    [SerializeField] private Transform[,] _gridTransforms = new Transform[3, 3];
    // [TableMatrix(HorizontalTitle = "TeamGridView transforms", SquareCells = true)]
    // public GameObject[,] _transforms = new GameObject[3, 3];

    public Vector3 GetPositionOfGridPosition(Vector2 gridPosition)
    {
        Vector3 position =  _gridTransforms[(int)gridPosition.x, (int)gridPosition.y].position;
        return position;
    }
    public Quaternion GetRotationOfGridPosition(Vector2 gridPosition)
    {
        Quaternion rotation =  _gridTransforms[(int)gridPosition.x, (int)gridPosition.y].rotation;
        return rotation;
    }
}