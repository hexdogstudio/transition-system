using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeTransitionLayer : TransitionLayer
{
    private CanvasGroup cover;
    private Coroutine tween;

    public override void Hide(float time, float delay)
    {
        IsDone = false;
        Progress = 0.0f;
        tween = StartCoroutine(HideTween(time, delay));
    }

    public override void HideImmediately()
    {
        InterruptTweening();
        cover.alpha = 0.0f;
        gameObject.SetActive(false);
    }

    public override void Show(float time, float delay)
    {
        gameObject.SetActive(true);
        IsDone = false;
        Progress = 0.0f;
        tween = StartCoroutine(ShowTween(time, delay));
    }

    public override void ShowImmediately()
    {
        InterruptTweening();
        cover.alpha = 1.0f;
        gameObject.SetActive(true);
    }

    void Awake()
    {
        cover = GetComponent<CanvasGroup>();
    }

    private void InterruptTweening()
    {
        if (tween != null)
        {
            StopCoroutine(tween);
            tween = null;
        }
        Progress = 1.0f;
        IsDone = true;
    }
    private IEnumerator ShowTween(float time, float delay)
    {
        float t = 0.0f;

        cover.alpha = 0.0f;
        yield return new WaitForSecondsRealtime(delay);

        while (t < time)
        {
            t += Time.unscaledDeltaTime;
            Progress = Mathf.Clamp01(t / time);
            cover.alpha = Progress;
            yield return null;
        }

        cover.alpha = 1.0f;
        Progress = 1.0f;
        IsDone = true;
        tween = null;
        InvokeAndClearCallback();
    }
    private IEnumerator HideTween(float time, float delay)
    {
        float t = 0.0f;

        cover.alpha = 1.0f;
        yield return new WaitForSecondsRealtime(delay);

        while (t < time)
        {
            t += Time.unscaledDeltaTime;
            Progress = Mathf.Clamp01(t / time);
            cover.alpha = 1 - Progress;
            yield return null;
        }

        cover.alpha = 0.0f;
        Progress = 1.0f;
        IsDone = true;
        tween = null;
        gameObject.SetActive(false);
        InvokeAndClearCallback();
    }
}
