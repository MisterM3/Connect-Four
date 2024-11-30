public static class MathFunctions
{
    /// <summary>
    /// Changes a range of values, into a different range. For Example (0 - 100) to (-1 - 1)
    /// </summary>
    /// <returns>Updated Value</returns>
    public static float ConvertRange( float value, float oldMin, float oldMax, float newMin = 0, float newMax = 1)
    {
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        return (((value - oldMin) * newRange) / oldRange) + newMin;
    }
}
