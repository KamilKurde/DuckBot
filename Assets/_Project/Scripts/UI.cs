using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public bool dimmingActive = false;
    [SerializeField] internal Image fadeImage;
    [Range(0.01f, 5f)]public float opacityChangeTime;
    internal float _opacityChangeStep;

    private void Awake()
    {
        _opacityChangeStep = opacityChangeTime / 50f;
    }

    private void FixedUpdate()
    {
        var currentOpacity = fadeImage.color.a;
        var targetOpacity = dimmingActive ? 1f : 0f;
        if (currentOpacity < targetOpacity)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + _opacityChangeStep);
            if (fadeImage.color.a > targetOpacity)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetOpacity);
            }
        }
        else if (currentOpacity > targetOpacity)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a - _opacityChangeStep);
            if (fadeImage.color.a < targetOpacity)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetOpacity);
            }
        }
    }
    
    

    private static IEnumerator LoadSceneCoroutine(int sceneIndex, float opacityChangeTime)
    {
        yield return new WaitForSeconds(opacityChangeTime);
        SceneManager.LoadScene(sceneIndex);
    }

    internal void LoadScene(int sceneIndex)
    {
        dimmingActive = true;
        StartCoroutine(LoadSceneCoroutine(sceneIndex, opacityChangeTime));
    }
}
