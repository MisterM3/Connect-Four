using System.Collections;
using UnityEngine;

/// <summary>
/// Basic SpotLights that change colour every couple seconds
/// </summary>
public class ArcadeLight : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private float _timeTillSwitch = 5f;

    // Using a courotine instead of Update so I don't have to keep track of the timer myself and can just use WaitForSeconds
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
            yield return new WaitForSeconds(_timeTillSwitch);
        }
    }

    private void SetRandomLightColor()
    {
        _light.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }
}
