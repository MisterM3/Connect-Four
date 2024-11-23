using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

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

    protected virtual void OnSingletonFailInitialize() { }
    protected virtual void OnSingletonSucceedInitialize() { }
}
