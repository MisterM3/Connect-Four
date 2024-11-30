using UnityEngine;

/// <summary>
/// Class to hold all different extensionmethods
/// </summary>
public static class ExtensionMethods
{

    public static void DestroyAllChildren(this Transform transform)
    {
        int childCount = transform.childCount;

        if (childCount == 0)
            return;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Object.Destroy(transform.GetChild(i).gameObject);
        }
    }

}
