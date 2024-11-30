using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeLight : MonoBehaviour
{

    [SerializeField] private Light _light;
    [SerializeField] private float timeTillSwitch = 5f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightCourotine());
    }

    
    public IEnumerator LightCourotine()
    {
        //Lights are on all the time, so keep an infinite loop for the courotine
        while(true)
        {
            SetRandomLightColor();
            yield return new WaitForSeconds(timeTillSwitch);
        }
    }

    private void SetRandomLightColor()
    {
        _light.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }
}
