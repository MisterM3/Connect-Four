using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBar : MonoBehaviour
{

    [SerializeField] private ThrowBall throwBall;
    [SerializeField] private GameObject objectVisualToMove;
    [SerializeField] private float size = 1;

    private Vector3 distance;
    private Vector3 zeroValuePosition;

#if UNITY_EDITOR
    [SerializeField, Range(0,1)] private float currentPos;
#endif


    private void Start()
    {
        Vector3 rotationBasedLenght = Quaternion.Euler(this.transform.eulerAngles) * new Vector3(size, 0, 0);
        zeroValuePosition = (this.transform.position - (rotationBasedLenght * .5f));
        distance = rotationBasedLenght;
    }

    private void Update()
    {
        SetVisual(throwBall.Amount);
    }

    private void SetVisual(float amount)
    {
        if (objectVisualToMove == null)
            return;
        objectVisualToMove.transform.position = zeroValuePosition + distance * amount;
    }


    private void OnDrawGizmosSelected()
    {

        Vector3 rotationBasedLenght = Quaternion.Euler(this.transform.eulerAngles) * new Vector3(size, 0, 0);
        zeroValuePosition = (this.transform.position - (rotationBasedLenght * .5f));
        distance = rotationBasedLenght;

        Gizmos.DrawLine(zeroValuePosition, zeroValuePosition + distance);

        SetVisual(currentPos);

    }

}
