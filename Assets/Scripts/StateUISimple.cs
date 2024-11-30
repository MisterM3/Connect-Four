using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateUISimple : MonoBehaviour, IStateUI
{
    public virtual void DisableState()
    {
        gameObject.SetActive(false);
    }

    public virtual void EnableState()
    {
        gameObject.SetActive(true);
    }
}
