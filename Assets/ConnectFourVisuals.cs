using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MakeHoops))]
public class ConnectFourVisuals : MonoBehaviour
{

    private Vector3[] topRowPositions;
    private Hoop[,] hoops;
    [SerializeField] private MakeHoops makeHoops;
    [SerializeField] private BallThrow ballThrowAnimation;
    [SerializeField] private GameObject ball;

    private void OnEnable()
    {
        ConnectFourManager.Instance.onBoardReset += ResetVisuals;
        ConnectFourManager.Instance.onDiskAdded += AddDisk;
    }
    private void OnDisable()
    {
        ConnectFourManager.Instance.onBoardReset -= ResetVisuals;
        ConnectFourManager.Instance.onDiskAdded -= AddDisk;
    }

    private void AddDisk(Vector2Int position, Player player)
    {

        Vector3 rowPos = topRowPositions[position.x];
        Vector3 finalPos = hoops[position.x, position.y].WorldPosition;

        //Plays animation before placing the disk
        ballThrowAnimation.ThrowBallAnim(rowPos, finalPos, () => AddDiskVisual(position, player));
    }

    private void AddDiskVisual(Vector2Int position, Player player)
    {
        hoops[position.x, position.y].SetDisk(player);
    }


    public void ResetVisuals(object sender, Vector2Int dimensionsBoard)
    {
        if (topRowPositions == null) topRowPositions = new Vector3[dimensionsBoard.x];
        if (hoops == null) hoops = new Hoop[dimensionsBoard.x, dimensionsBoard.y];
        Debug.Log(makeHoops);
        hoops = makeHoops.CreateHoops(dimensionsBoard.x, dimensionsBoard.y, out topRowPositions, out float ballSize);
        ball.transform.localScale = new Vector3(ballSize, ballSize, ballSize);
    }


}
