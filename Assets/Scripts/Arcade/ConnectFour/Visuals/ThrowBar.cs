using UnityEngine;

/// <summary>
/// Visual Bar for the throwing point
/// </summary>
public class ThrowBar : MonoBehaviour
{

    [SerializeField] private ThrowBall _throwBall;
    [SerializeField] private GameObject _objectVisualToMove;
    [SerializeField] private float _size = 1;

    private Vector3 _distance;
    private Vector3 _zeroValuePosition;

    /// Using Unity Editor here as I only want to use this value in editor mode, and NOT in play mode (this stops it from being compiled if this value is still in
#if UNITY_EDITOR
    [SerializeField, Range(0,1)] private float _currentPos;
#endif


    private void Start()
    {
        Vector3 rotationBasedLenght = Quaternion.Euler(this.transform.eulerAngles) * new Vector3(_size, 0, 0);
        _zeroValuePosition = (this.transform.position - (rotationBasedLenght * .5f));
        _distance = rotationBasedLenght;
    }

    private void Update()
    {
        SetVisual(_throwBall.Amount);
    }

    private void SetVisual(float amount)
    {
        if (_objectVisualToMove == null)
            return;
        _objectVisualToMove.transform.position = _zeroValuePosition + _distance * amount;
    }


    /// This is to see how far the line will go, and helps it so I don't have to manually check it value after value
    /// It also updated the gameobject itself, so I can move it with the bar itself
    private void OnDrawGizmosSelected()
    {
        Vector3 rotationBasedLenght = Quaternion.Euler(this.transform.eulerAngles) * new Vector3(_size, 0, 0);
        _zeroValuePosition = (this.transform.position - (rotationBasedLenght * .5f));
        _distance = rotationBasedLenght;

        Gizmos.DrawLine(_zeroValuePosition, _zeroValuePosition + _distance);
        SetVisual(_currentPos);

    }

}
