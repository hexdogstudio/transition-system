using System;
using UnityEngine;

public abstract class TransitionLayer : MonoBehaviour
{
    public float Progress { get; protected set; }
    public bool IsDone { get; protected set; }
    public Action OnComplete { get; protected set; }
    public abstract void Show(float time, float delay);
    public abstract void ShowImmediately();
    public abstract void Hide(float time, float delay);
    public abstract void HideImmediately();

    protected void InvokeAndClearCallback()
    {
        Action callback = OnComplete;
        OnComplete = null;
        callback?.Invoke();
    }
}
