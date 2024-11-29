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
    [SerializeField] private BallThrowAnimation ballThrowAnimation;
    [SerializeField] private GameObject ball;

    private void OnEnable()
    {
        ConnectFourManager.Instance.onBoardReset += ResetVisuals;
        ConnectFourManager.Instance.onDiskAdded += AddDisk;
        ConnectFourManager.Instance.onDiskMissed += MissThrow;
    }
    private void OnDisable()
    {
        ConnectFourManager.Instance.onBoardReset -= ResetVisuals;
        ConnectFourManager.Instance.onDiskAdded -= AddDisk;
        ConnectFourManager.Instance.onDiskMissed -= MissThrow;
    }

    private void AddDisk(Vector2Int position, Player player)
    {

        Vector3 rowPos = topRowPositions[position.x];
        Vector3 finalPos = hoops[position.x, position.y].WorldPosition;

        //Plays animation before placing the disk
        ballThrowAnimation.ThrowBallAnim(rowPos, finalPos, () => AfterAnimation(position, player));
    }

    private void MissThrow(int position)
    {
        //Fix to have nice height
        Vector3 rowPos = topRowPositions[position] - new Vector3(.75f, 0, 0);
        Vector3 finalPos = topRowPositions[position] - new Vector3(.75f, 1, 0);

        //Plays animation before placing the disk
        ballThrowAnimation.ThrowBallAnim(rowPos, rowPos, () => ConnectFourManager.Instance.NextTurn());
    }


    private void AfterAnimation(Vector2Int position, Player player)
    {
        AddDiskVisual(position, player);
        ConnectFourManager.Instance.NextTurn();
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
