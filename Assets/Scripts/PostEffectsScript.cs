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
        bloom = ScriptableObject.CreateInstance<Bloom>();
        bloom.enabled.Override(true);
        bloom.intensity.Override(0f);
        bloom.threshold.Override(0f);
        
        ColorParameter bloomColor = new ColorParameter();
        bloomColor.value = Color.blue;
        bloom.color.Override(bloomColor);

        volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, bloom);

        while (bloom.intensity.value < 5f)
        {
            yield return new WaitForSeconds(.04f);
            //bloom.threshold.value -= .01f;
            bloom.intensity.value += 0.1f;
        }

        LevelsController.LoadNextScene();
    }
}

/*
    Just for example: 

    Bloom bloom = postProcessVolume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.Bloom>();
    var colorParameter = new UnityEngine.Rendering.PostProcessing.ColorParameter();
    colorParameter.value = Color.red;
    bloom.color.Override(colorParameter);
*/
