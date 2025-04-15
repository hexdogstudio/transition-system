using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int targetLayer;
    [SerializeField] private float fadeInTime = 0.5f;
    [SerializeField] private float fadeInDelay = 0.0f;
    [SerializeField] private float fadeOutTime = 0.5f;
    [SerializeField] private float fadeOutDelay = 0.25f;

    public void Load(string scene)
    {
        StartCoroutine(LoadAsync(scene));
    }


    private IEnumerator LoadAsync(string scene)
    {
        TransitionLayer layer = TransitionManager.Instance.Layers[targetLayer];
        AsyncOperation aop = SceneManager.LoadSceneAsync(scene);
        aop.allowSceneActivation = false;

        // Transition: FADE-IN
        layer.Show(fadeInTime, fadeInDelay);

        while (aop.progress < 0.9f || !layer.IsDone)
            yield return null;

        aop.allowSceneActivation = true;
        // Transition: FADE-OUT
        layer.Hide(fadeOutTime, fadeOutDelay);
    }
}
