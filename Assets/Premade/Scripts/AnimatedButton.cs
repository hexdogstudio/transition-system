using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnimatedButton : MonoBehaviour
{
    private EaseCurve curve = new EaseCurve(Ease.Type.OutBack);
    private Coroutine scaleProcess;

    public void Animate()
    {
        InterruptScaleProcess();
        transform.localScale = new Vector2(1.15f, 1.15f);
        scaleProcess = StartCoroutine(ScaleProcess());
    }

    private void Awake()
    {
        RectTransform rectTransform = transform as RectTransform;
        if (rectTransform.pivot != new Vector2(0.5f, 0.5f))
        {
            int prefix = rectTransform.pivot.y == 0 ? 1 : -1;
            rectTransform.localPosition += new Vector3(0, rectTransform.rect.height / 2 * prefix);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
    }
    private void InterruptScaleProcess()
    {
        if (scaleProcess != null)
        {
            StopCoroutine(scaleProcess);
            transform.localScale = new Vector2(1, 1);
            scaleProcess = null;
        }
    }
    private IEnumerator ScaleProcess()
    {
        Vector2 start = transform.localScale;
        Vector2 end = new Vector2(1, 1);
        float t = 0.0f;
        float time = 0.25f;

        yield return new WaitForSeconds(.1f);

        while (t < time)
        {
            t += Time.deltaTime;
            transform.localScale = Vector2.LerpUnclamped(start, end, curve.Calc(t / time));
            yield return null;
        }

        transform.localScale = end;
    }
}
