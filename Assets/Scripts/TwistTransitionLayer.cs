using System.Collections;
using UnityEngine;

public class TwistTransitionLayer : TransitionLayer
{
    [SerializeField] private RectTransform cover;
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
        cover.localScale = new Vector3();
        cover.eulerAngles = new Vector3();
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
        cover.localScale = new Vector3(1, 1, 1);
        cover.eulerAngles = new Vector3();
        gameObject.SetActive(true);
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

        cover.localScale = new Vector3();
        cover.eulerAngles = new Vector3();
        yield return new WaitForSecondsRealtime(delay);

        while (t < time)
        {
            t += Time.unscaledDeltaTime;
            Progress = Mathf.Clamp01(t / time);
            cover.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Progress);
            cover.eulerAngles = new Vector3(0, 0, Mathf.Lerp(0, -360, Progress));
            yield return null;
        }

        cover.localScale = new Vector3(1, 1, 1);
        cover.eulerAngles = new Vector3();
        Progress = 1.0f;
        IsDone = true;
        tween = null;
        InvokeAndClearCallback();
    }
    private IEnumerator HideTween(float time, float delay)
    {
        float t = 0.0f;

        cover.localScale = new Vector3(1, 1, 1);
        cover.eulerAngles = new Vector3();
        yield return new WaitForSecondsRealtime(delay);

        while (t < time)
        {
            t += Time.unscaledDeltaTime;
            Progress = Mathf.Clamp01(t / time);
            cover.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, 1 - Progress);
            cover.eulerAngles = new Vector3(0, 0, Mathf.Lerp(0, 360, 1 - Progress));
            yield return null;
        }

        cover.localScale = new Vector3();
        cover.eulerAngles = new Vector3();
        Progress = 1.0f;
        IsDone = true;
        tween = null;
        gameObject.SetActive(false);
        InvokeAndClearCallback();
    }

}
