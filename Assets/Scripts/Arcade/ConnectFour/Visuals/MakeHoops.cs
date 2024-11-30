using UnityEngine;

/// <summary>
/// Makes the basketball machine the correct amount of hoops, and the correct size
/// </summary>
public class MakeHoops : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _leftWallHoop;
    [SerializeField] private GameObject _hoop;

    [Header("On Object")]
    //Position to use as parent for the hoops
    [SerializeField] private Transform _parentMiddle;
    [SerializeField] private GameObject _backWall;

    //In the same frame gameobject don't get delted properly, do keep this one so all the temporary gameobjects are deleted between games
    [SerializeField] private Transform _tempParent;


    public void DestroyHoops()
    {
        _parentMiddle.DestroyAllChildren();
        _tempParent.DestroyAllChildren();
    }

    ///Creates all hoops for the machine itself by looking at the amount of rows and columns
    ///Gives out information for things needed for animations and to make sure the ball is the correct size
    public Hoop[,] CreateHoops(int rows, int columns, out Vector3[] topPositions, out float ballSize)
    {
        DestroyHoops();

        Hoop[,] hoops = new Hoop[rows, columns];
        topPositions = new Vector3[rows];

        //We start at left wall, so start with the special wall
        GameObject hoopPrefabToInitialize = _leftWallHoop;

        float sizeHoop = 1.0f / rows;
        Vector3 backWallScale = _backWall.transform.localScale;

        ballSize = sizeHoop;
        //I want the backwall to be a little bit higher than the nets go, so add one to column to add the height
        float heightBackWall = sizeHoop * (columns + 1);
        _backWall.transform.localScale = new Vector3(backWallScale.x, heightBackWall, backWallScale.z);


        for (int xPos = 0; xPos < rows; xPos++)
        {
            //After the left side has been made, we can switch to the other hoop with the wall on the other side
            if (xPos == 1)
                hoopPrefabToInitialize = _hoop;

            for (int yPos = 0; yPos < columns; yPos++)
            {
                Transform hoopTf = Instantiate(hoopPrefabToInitialize, new Vector3(-sizeHoop * xPos, sizeHoop * yPos), Quaternion.identity).transform;
                hoopTf.SetParent(_parentMiddle, false);
                hoopTf.localScale = new Vector3(sizeHoop, sizeHoop, sizeHoop);

                if (!hoopTf.TryGetComponent<Hoop>(out Hoop hoop))
                {
                    Debug.LogWarning("GameObject on MakeHoop doesn't have hoop script, add hoop script to hoop objects on makehoops! " + gameObject.name);
                }
                hoops[xPos, yPos] = hoop;
            }

            //Need to get worldposition for animations so first make it to check worldposition and then destroy as it's not usefull anymore
            const float zAmount = -.1f;

            Transform rowTF = Instantiate(new GameObject(), new Vector3(-sizeHoop * xPos - sizeHoop * .5f, heightBackWall, zAmount), Quaternion.identity).transform;
            rowTF.SetParent(_parentMiddle, false);
            topPositions[xPos] = rowTF.position;
            //Put it on this parent, ready to be deleted (can't delete using Destroy, it won't delete the gameobject)
            rowTF.SetParent(_tempParent);

        }

        return hoops;
    }





}
