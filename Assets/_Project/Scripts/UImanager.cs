using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public bool dimmingActive = false;
    [SerializeField] private Image fadeImage;
    [Range(0.01f, 5f)]public float opacityChangeTime;
    private float _opacityChangeStep;

    private void Start()
    {
        GameManager.uImanager = this;
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

    public static IEnumerator LoadNextScene(float opacityChangeTime)
    {
        yield return new WaitForSeconds(opacityChangeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndLevel()
    {
        dimmingActive = true;
        StartCoroutine(LoadNextScene(opacityChangeTime));
    }

    private void OnDestroy()
    {
        GameManager.uImanager = null;
    }
}
