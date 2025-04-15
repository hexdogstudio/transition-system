using UnityEngine;
using UnityEngine.AddressableAssets;

public static class SystemInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        GameObject gameObject = Addressables.InstantiateAsync("system").WaitForCompletion();
        gameObject.name = "[System]";
        Object.DontDestroyOnLoad(gameObject);
    }
}
