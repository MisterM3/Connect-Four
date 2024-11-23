using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectFourVisuals : MonoBehaviour
{


    [SerializeField] GameObject Prefab;
    [SerializeField] GameObject YellowPlayer;
    [SerializeField] GameObject RedPlayer;
    [SerializeField] float space;

    private void OnEnable()
    {
        Debug.Log(2);
        Debug.Log(ConnectFourManager.Instance);
        ConnectFourManager.Instance.onBoardReset += ResetVisuals;
        ConnectFourManager.Instance.onDiskAdded += AddDiskVisual;
    }
    private void OnDisable()
    {
        ConnectFourManager.Instance.onBoardReset -= ResetVisuals;
        ConnectFourManager.Instance.onDiskAdded -= AddDiskVisual;

    }

    private void AddDiskVisual(Vector2Int arg1, Player arg2)
    {
        GameObject go = new();

        if (arg2 == Player.PlayerOne)
        {
            go = YellowPlayer;
        }
        else if (arg2 == Player.PlayerTwo)
        {
            go = RedPlayer;
        }

        Instantiate(go, new Vector3(space * arg1.x, space * arg1.y), Quaternion.identity, this.transform);
    }

    public void ResetVisuals(object sender, Vector2Int dimensionsBoard)
    {
        MakeBoard(dimensionsBoard);
    }

    public void MakeBoard(Vector2Int dimensionsBoard)
    {
        for(int xPos = 0; xPos < dimensionsBoard.x; xPos++)
        {
            for (int yPos = 0; yPos < dimensionsBoard.y; yPos++)
            {
            //    Instantiate(Prefab, new Vector3(space * xPos, space * yPos), Quaternion.identity, this.transform);
            }
        }
    }

}
