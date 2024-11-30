using UnityEngine;

/// <summary>
/// Holds functions usefull for tweening and lerping
/// </summary>
/// Functions helpfull for easier/lerping/tweening (https://easings.net/)
public static class LerpFunctions
{
    public static float EaseInOutBack(float x, float downUp = 1.0158f)
    {
        float c1 = downUp;
        float c2 = c1 * 1.525f;

        if (x < .5f)
        {
            return ((Mathf.Pow(2 * x, 2) * ((c1 + 1) * 2 * x - c1)) / 2);
        }
        else
        {
            return ((Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2);
        }
    }

    public static float EaseInSine(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2f);
    }

    public static float EaseOutSine(float x)
    {
        return Mathf.Sin((x * Mathf.PI) / 2);
    }

}
