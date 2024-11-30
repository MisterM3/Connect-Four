using UnityEngine;

/// <summary>
/// Simple UI that just disables and enables the gameobject
/// </summary>
public class StateUISimple : MonoBehaviour, IStateUI
{
    //When functioned can be put together and have easy to understand functionality (with one liners), I like to order it this way for readability
    public virtual void DisableState() => gameObject.SetActive(false);
    public virtual void EnableState() => gameObject.SetActive(true);
}
