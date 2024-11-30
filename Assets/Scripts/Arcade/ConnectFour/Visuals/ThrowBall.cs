using System;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{

    [SerializeField] private float _speed = 1;
    private float _amount = .5f;
    public float Amount => _amount;
    private int _direction = 1;

    private bool _active = false;

    //Calculates how far the width is to hit each row
    private float _amountPerRow = 0;



    /// Start and OnDestroy used for keeping track of visuals even if they are disabled
    private void Start()
    {
        ConnectFourManager.Instance.onBoardReset += ResetSlider;
        ConnectFourManager.Instance.onDiskAdded += DisableSlider;
        ConnectFourManager.Instance.onDiskMissed += DisableSlider;
        ConnectFourManager.Instance.onPlayerWon += DisableSlider;
        ConnectFourManager.Instance.onTurnStart += Activate;
        _amountPerRow = 1.0f / ConnectFourManager.Instance.BoardDimensions.x;
    }

    void Update()
    {
        if (!_active)
            return;
        UpdateAmount();

        if (Input.anyKeyDown)
        {
            int rowBallToThrow = (int)(_amount / _amountPerRow);
            ConnectFourManager.Instance.PlayTurn(rowBallToThrow);
        }
    }

    private void OnDestroy()
    {
        ConnectFourManager.Instance.onBoardReset -= ResetSlider;
        ConnectFourManager.Instance.onDiskAdded -= DisableSlider;
        ConnectFourManager.Instance.onDiskMissed -= DisableSlider;
        ConnectFourManager.Instance.onPlayerWon -= DisableSlider;
        ConnectFourManager.Instance.onTurnStart -= Activate;
    }

    /// Only thing using various events is that they all have different parameters, and I can't use anonymous functions as they won't work well when (un)subsribing from the events
    private void DisableSlider(object sender, Player e) => DisableSlider(0);
    private void DisableSlider(Vector2Int arg1, Player arg2) => DisableSlider(0);
    private void DisableSlider(int i) => SetActive(false);
    private void Activate(object sender, EventArgs e) => SetActive(true);
    public void SetActive(bool active) => this._active = active;


    //Specifc setup for controlling the speed of the slider
    public void Setup(float speed) => this._speed = speed;
    
    
    private void ResetSlider(object sender, Vector2Int boardDimensions)
    {
        _amountPerRow = 1.0f / boardDimensions.x;
        ResetAmount(true);
    }


    public void ResetAmount(bool active = true)
    {
        _amount = .5f;
        SetActive(active);
    }
    public void ResetAmount(float speed, bool active = true)
    {
        Setup(speed);
        ResetAmount(active);
    }


    /// Makes sure the bar moves left and right with the correct time
    public void UpdateAmount()
    {
        _amount += _speed * _direction * Time.deltaTime;

        if (_amount >= 1 && _direction == 1)
        {
            _direction *= -1;

            // Still keep the track on the correct amount by decreasing amount by the overflow
            _amount -= (_amount - 1);
        }
        else if (_amount <= 0 && _direction == -1)
        {
            _direction *= -1;

            //Keep track by multiplying the minus amount
            _amount *= -1;
        }
    }
}
