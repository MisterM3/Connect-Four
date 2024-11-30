using System;
using System.Collections;
using UnityEngine;

public class BallThrowAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    private Vector3 _startPositionBall;

    //How far the ball moves down and up when moving, after testing, 1 was a good value
    private const float DOWN_UP = 1f;

    
    private void Awake()
    {
        //We need the startposition to reset the ball to
        _startPositionBall = _ball.transform.position;
    }

    private void Start()
    {
        ConnectFourManager.Instance.onTurnStart += ResetPosition;
    }

    private void OnDestroy()
    {
        ConnectFourManager.Instance.onTurnStart -= ResetPosition;
    }

    private void ResetPosition(object sender, EventArgs e) => ResetBall();
    public void ResetBall() => _ball.transform.position = _startPositionBall;

    ///Using a courotine, as the animations plays over a lot of different frames
    public void ThrowBallAnim(Vector3 positionRow, Vector3 finalSpot, Action animationDone)
    {
        StartCoroutine(ThrowBallAnimation(positionRow, finalSpot, animationDone));
    }

    public IEnumerator ThrowBallAnimation(Vector3 positionRowPos, Vector3 finalSpotPos, Action animationDone)
    {
        float time = 0;

        while ((time < 1))
        {
            //Makes sure the last value is 1, so the animation always looks the same
            time = Mathf.Min(time + Time.deltaTime,1);
            float easyInOutValue = LerpFunctions.EaseInOutBack(time, downUp: DOWN_UP);
            float yValue = (_startPositionBall.y * (1 - easyInOutValue)) + positionRowPos.y * (easyInOutValue);

            //Wait until the ball goes up to move it along the z axis

            float value = LerpFunctions.EaseInSine(time);

            float zValue = (_startPositionBall.z * (1 - value)) + positionRowPos.z * (value);
            float xValue = (_startPositionBall.x * (1 - value)) + positionRowPos.x * (value);

            _ball.transform.position = new Vector3(xValue, yValue, zValue);
            yield return null;
        }

        time = 0;
        while (time < 1)
        {
            //Makes sure the last value is 1, so the animation always looks the same
            time = Mathf.Min(time + Time.deltaTime, 1);
            float easyInOutValue = LerpFunctions.EaseOutSine(time);
            float yValue = (positionRowPos.y * (1 - easyInOutValue)) + finalSpotPos.y * (easyInOutValue);

            _ball.transform.position = new Vector3(_ball.transform.position.x, yValue, _ball.transform.position.z);
            yield return null;
        }

        //Animation is done, now we can continue the next turn
        animationDone?.Invoke();
    }
}
