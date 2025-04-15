using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    private static TransitionManager instance;
    public static TransitionManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindFirstObjectByType<TransitionManager>();
            return instance;
        }
    }
    public TransitionLayer[] Layers { get; private set; }

    void Awake()
    {
        Layers = GetComponentsInChildren<TransitionLayer>();
    }
    void Start()
    {
        foreach (TransitionLayer layer in Layers)
            if (!layer.IsDone)
                layer.HideImmediately();
    }
}
