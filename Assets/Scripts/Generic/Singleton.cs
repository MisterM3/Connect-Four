using UnityEngine;

/// <summary>
/// A generalized class to quickly make singletons
/// </summary>
/// <typeparam name="T">The name of the subclass</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    //Standard system to make sure only one of this scripts is in the scene at the same time (needs to be called from subclass)
    protected void InitializeSingleton(T ownObject)
    {
        if (Instance != null)
        {
            Debug.LogWarning($"There are more than one {ownObject.name} in scene, delete one!");
            Destroy(ownObject);
            OnSingletonFailInitialize();
            return;
        }

        Instance = ownObject;
        OnSingletonSucceedInitialize();
    }

    //Functions to give extra functionality to initialization
    protected virtual void OnSingletonFailInitialize() { }
    protected virtual void OnSingletonSucceedInitialize() { }
}
