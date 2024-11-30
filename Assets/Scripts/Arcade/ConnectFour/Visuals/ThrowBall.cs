using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{

    [SerializeField] private float speed = 1;
    private float amount = .5f;
    public float Amount => amount;
    private int direction = 1;

    private bool active = false;

    private float amountPerRow = 0;


    private void Start()
    {
        ConnectFourManager.Instance.onBoardReset += ResetSlider;
        ConnectFourManager.Instance.onDiskAdded += DisableSlider;
        ConnectFourManager.Instance.onDiskMissed += DisableSlider;
        ConnectFourManager.Instance.onPlayerWon += DisableSlider;
        ConnectFourManager.Instance.onTurnStart += Activate;
        amountPerRow = 1.0f / ConnectFourManager.Instance.BoardDimensions.x;
    }

    private void DisableSlider(object sender, Player e)
    {
        SetActive(false);
    }

    private void Activate(object sender, EventArgs e)
    {
        SetActive(true);
    }

    private void DisableSlider(Vector2Int arg1, Player arg2)
    {
        SetActive(false);
    }


    private void DisableSlider(int i)
    {
        SetActive(false);
    }

    private void ResetSlider(object sender, Vector2Int boardDimensions)
    {
        amountPerRow = 1.0f / boardDimensions.x;
        ResetAmount(true);
    }

    public void Setup(float speed) => this.speed = speed;

    public void SetActive(bool active) => this.active = active;

    public void ResetAmount(float speed, bool active = true)
    {
        Setup(speed);
        ResetAmount(active);
    }

    public void ResetAmount(bool active = true)
    {
        amount = .5f;
        SetActive(active);
    }

    void Update()
    {
        if (!active)
            return;
        UpdateAmount();

        if (Input.anyKeyDown)
        {
            int rowBallToThrow = (int)(amount / amountPerRow);
            ConnectFourManager.Instance.PlayTurn(rowBallToThrow);
        }
    }

    public void UpdateAmount()
    {
        amount += speed * direction * Time.deltaTime;

        if (amount >= 1 && direction == 1)
        {
            direction *= -1;

            // Still keep the track on the correct amount by decreasing amount by the overflow
            amount -= (amount - 1);
        }
        else if (amount <= 0 && direction == -1)
        {
            direction *= -1;

            //Keep track by multiplying the minus amount
            amount *= -1;
        }
    }
}
