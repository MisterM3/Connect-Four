using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class mainting most of the visuals
/// </summary>
[RequireComponent(typeof(MakeHoops))]
public class ConnectFourVisuals : MonoBehaviour
{

    private Vector3[] _topRowPositions;
    private Hoop[,] _hoops;
    [SerializeField] private MakeHoops _makeHoops;
    [SerializeField] private BallThrowAnimation _ballThrowAnimation;
    [SerializeField] private GameObject _ball;

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
        Vector3 rowPos = _topRowPositions[position.x];
        Vector3 finalPos = _hoops[position.x, position.y].WorldPosition;

        //Plays animation before placing the disk
        _ballThrowAnimation.ThrowBallAnim(rowPos, finalPos, () => AfterAnimation(position, player));
    }
    private void AddDiskVisual(Vector2Int position, Player player)
    {
        _hoops[position.x, position.y].SetDisk(player);
    }

    //Throw the ball behind the machine if the row is already full
    private void MissThrow(int position)
    {
        //Fix to have nice height
        Vector3 rowPos = _topRowPositions[position] - new Vector3(.75f, 0, 0);
        Vector3 finalPos = _topRowPositions[position] - new Vector3(.75f, 1, 0);

        //Plays animation before placing the disk
        _ballThrowAnimation.ThrowBallAnim(rowPos, rowPos, () => ConnectFourManager.Instance.NextTurn());
    }


    ///I use a delegate here to continue the game only after the animtion is done
    private void AfterAnimation(Vector2Int position, Player player)
    {
        AddDiskVisual(position, player);

        if (ConnectFourManager.Instance.HasEnded)
        {
            UIStateMachine.Instance.SwitchUI(UIStates.EndingGame);
            _ballThrowAnimation.ResetBall();
        }
        else
        {
            ConnectFourManager.Instance.NextTurn();
        }
    }

    public void ResetVisuals(object sender, Vector2Int dimensionsBoard)
    {
        if (_topRowPositions == null) _topRowPositions = new Vector3[dimensionsBoard.x];
        if (_hoops == null) _hoops = new Hoop[dimensionsBoard.x, dimensionsBoard.y];
        _hoops = _makeHoops.CreateHoops(dimensionsBoard.x, dimensionsBoard.y, out _topRowPositions, out float ballSize);
        _ball.transform.localScale = new Vector3(ballSize, ballSize, ballSize);
    }


}
