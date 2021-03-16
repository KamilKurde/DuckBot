using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public bool dimmingActive = false;
    [SerializeField] internal Image fadeImage;
    [Range(0.01f, 5f)]public float opacityChangeTime;
    [SerializeField] private AudioMixer audioMixer;

    internal void Start()
    {
        GameManager.audioMixer = audioMixer;
        GameSave.LoadAudioSettings();
    }

    internal void HandleOpacityChange(float time)
    {
        var opacityChangeStep = 1f / opacityChangeTime * time;
        var currentOpacity = fadeImage.color.a;
        var targetOpacity = dimmingActive ? 1f : 0f;
        if (currentOpacity < targetOpacity)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + opacityChangeStep);
            if (fadeImage.color.a > targetOpacity)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetOpacity);
            }
        }
        else if (currentOpacity > targetOpacity)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a - opacityChangeStep);
            if (fadeImage.color.a < targetOpacity)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetOpacity);
            }
        }
    }

    private void Update()
    {
        HandleOpacityChange(Time.deltaTime);
    }

    private static IEnumerator LoadSceneCoroutine(int sceneIndex, float opacityChangeTime)
    {
        yield return new WaitForSeconds(opacityChangeTime);
        SceneManager.LoadScene(sceneIndex);
    }

    internal void LoadScene(int sceneIndex)
    {
        dimmingActive = true;
        GameManager.Clear();
        StartCoroutine(LoadSceneCoroutine(sceneIndex, opacityChangeTime));
    }
}
