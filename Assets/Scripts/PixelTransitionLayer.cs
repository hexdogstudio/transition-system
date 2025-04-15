using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PixelTransitionLayer : TransitionLayer
{
    private Image cover;
    private Material material;
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
        material.SetFloat("_Visibility", 0.0f);
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
        material.SetFloat("_Visibility", 1.0f);
        gameObject.SetActive(true);
    }

    void Awake()
    {
        cover = GetComponent<Image>();
        material = new Material(cover.material);
        cover.material = material;
    }
    void OnEnable()
    {
        RectTransform rectTransform = transform as RectTransform;
        material.SetFloat("_Ratio", rectTransform.rect.height / rectTransform.rect.width);
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

        material.SetFloat("_Visibility", 0.0f);
        yield return new WaitForSecondsRealtime(delay);

        while (t < time)
        {
            t += Time.unscaledDeltaTime;
            Progress = Mathf.Clamp01(t / time);
            material.SetFloat("_Visibility", Progress);
            yield return null;
        }

        material.SetFloat("_Visibility", 1.0f);
        Progress = 1.0f;
        IsDone = true;
        tween = null;
        InvokeAndClearCallback();
    }
    private IEnumerator HideTween(float time, float delay)
    {
        float t = 0.0f;

        material.SetFloat("_Visibility", 1.0f);
        yield return new WaitForSecondsRealtime(delay);

        while (t < time)
        {
            t += Time.unscaledDeltaTime;
            Progress = Mathf.Clamp01(t / time);
            material.SetFloat("_Visibility", 1.0f - Progress);
            yield return null;
        }

        material.SetFloat("_Visibility", 0.0f);
        Progress = 1.0f;
        IsDone = true;
        tween = null;
        gameObject.SetActive(false);
        InvokeAndClearCallback();
    }

}
