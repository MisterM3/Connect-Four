using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Split up script in two if time
public class MakeHoops : MonoBehaviour
{
    [SerializeField] GameObject leftWallHoop;
    [SerializeField] GameObject hoop;
    [SerializeField] Transform parentMiddle;
    [SerializeField] GameObject backWall;

    public Hoop[,] CreateHoops(int rows, int columns, out Vector3[] topPositions, out float ballSize)
    {
        DestroyHoops();

        Hoop[,] hoops = new Hoop[rows, columns];
        topPositions = new Vector3[rows];

        //We start at left wall, so start with the special wall
        GameObject hoopPrefabToInitialize = leftWallHoop;

        float sizeHoop = 1.0f / rows;
        Vector3 backWallScale = backWall.transform.localScale;

        ballSize = sizeHoop;
        //I want the backwall to be a little bit higher than the nets go, so add one to column to add the height
        float heightBackWall = sizeHoop * (columns + 1);
        backWall.transform.localScale = new Vector3(backWallScale.x, heightBackWall, backWallScale.z);


        for (int xPos = 0; xPos < rows; xPos++)
        {
            //After the left side has been made, we can switch to the other hoop with the wall on the other side
            if (xPos == 1)
                hoopPrefabToInitialize = hoop;

            for (int yPos = 0; yPos < columns; yPos++)
            {
                Transform hoopTf = Instantiate(hoopPrefabToInitialize, new Vector3(-sizeHoop * xPos, sizeHoop * yPos), Quaternion.identity).transform;
                hoopTf.SetParent(parentMiddle, false);
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
            rowTF.SetParent(parentMiddle, false);
            topPositions[xPos] = rowTF.position;
           // DestroyImmediate(rowTF.gameObject);

        }

        return hoops;
    }

    public void DestroyHoops()
    {
        int childCount = parentMiddle.childCount;

        if (childCount == 0)
            return;

        for(int i  = childCount - 1; i >= 0; i--)
        {
            Destroy(parentMiddle.GetChild(i).gameObject);
           
        }
    }



}
