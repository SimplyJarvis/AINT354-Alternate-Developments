using UnityEngine;

public class MazeExit : MazePassage
{

    private static Quaternion
        normalRotation = Quaternion.Euler(0f, -90f, 0f),
        mirroredRotation = Quaternion.Euler(0f, 90f, 0f);


    private bool isMirrored;

    private MazeExit OtherSideOfDoor
    {
        get
        {
            return otherCell.GetEdge(direction.GetOpposite()) as MazeExit;
        }
    }

    public override void Initialize(MazeCell primary, MazeCell other, MazeDirection direction)
    {
        base.Initialize(primary, other, direction);
       
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
           
        }
    }
}