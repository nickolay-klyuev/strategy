using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostEffectsScript : MonoBehaviour
{
    private PostProcessVolume volume;
    private Bloom bloom;

    // Start is called before the first frame update
    void Start()
    {
        bloom = ScriptableObject.CreateInstance<Bloom>();
        bloom.enabled.Override(true);
        bloom.intensity.Override(5f);
        bloom.threshold.Override(.6f);
        volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, bloom);
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(volume, true, true);
    }

    public void StartWinAnimation()
    {
        StartCoroutine(GameOverAnimation());
    }

    private IEnumerator GameOverAnimation()
    {
        while (bloom.threshold.value > 0f || bloom.intensity.value < 100f)
        {
            yield return new WaitForSeconds(.04f);
            bloom.threshold.value -= .01f;
            bloom.intensity.value += 2f;
        }

        LevelsController.LoadNextScene();
    }
}
