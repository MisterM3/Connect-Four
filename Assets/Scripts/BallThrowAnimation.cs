using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrowAnimation : MonoBehaviour
{


    [SerializeField] GameObject ball;
    [SerializeField] Transform posX;

    private Vector3 startPositionBall;
    private Vector3 endPos;

    public float multiply = 1f;
    public float downUp = 1f;

    private void Awake()
    {
        startPositionBall = ball.transform.position;
    }

    private void Start()
    {
        ConnectFourManager.Instance.onTurnStart += ResetPosition;
    }

    private void ResetPosition(object sender, EventArgs e)
    {
        ResetBall();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            //StartCoroutine(ThrowBallAnimation());
        }
    }

    public void ThrowBallAnim(Vector3 positionRow, Vector3 finalSpot, Action animationDone)
    {
        StartCoroutine(ThrowBallAnimation(positionRow, finalSpot, animationDone));
    }

    public IEnumerator ThrowBallAnimation(Vector3 positionRowPos, Vector3 finalSpotPos, Action animationDone)
    {
        float time = 0;

        while ((time < 1))
        {
            time = Mathf.Min(time + Time.deltaTime,1);
            float easyInOutValue = EaseInOutBack(time, downUp: downUp);
            float yValue = (startPositionBall.y * (1 - easyInOutValue)) + positionRowPos.y * (easyInOutValue);

            //Wait until the ball goes up to move it along the z axis

            float test = EaseInSine(time);

            float zValue = (startPositionBall.z * (1 - test)) + positionRowPos.z * (test);
            float xValue = (startPositionBall.x * (1 - test)) + positionRowPos.x * (test);

            ball.transform.position = new Vector3(xValue, yValue, zValue);
            yield return null;
        }

        time = 0;
        while (time < 1)
        {
            time = Mathf.Min(time + Time.deltaTime, 1);
            float easyInOutValue = EaseOutSine(time);
            float yValue = (positionRowPos.y * (1 - easyInOutValue)) + finalSpotPos.y * (easyInOutValue);

            ball.transform.position = new Vector3(ball.transform.position.x, yValue, ball.transform.position.z);
            yield return null;
        }

        animationDone?.Invoke();
    }


    public float EaseInOutBack(float x, float downUp = 1.0158f)
    {
        float c1 = downUp;
        float c2 = c1 * 1.525f;

        if (x < .5f)
        {
            return ((Mathf.Pow(2 * x, 2) * ((c1 + 1) * 2 * x - c1)) / 2);
        } else
        {
            return ((Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2);
        }
    }

    public float EaseInSine(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2f);
    }

    public float EaseOutSine(float x)
    {
        return Mathf.Sin((x * Mathf.PI) / 2);
    }





    public void ResetBall()
    {
        ball.transform.position = startPositionBall;
    }
}
