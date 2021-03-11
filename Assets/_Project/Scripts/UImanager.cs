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

    [SerializeField] public CanvasGroup pauseGroup;
    [SerializeField] private Text levelText;
    [SerializeField] private Text interactText;
    [SerializeField] private Text pickUpText;
    [Header("Texts")]
    [SerializeField] private string pickUp;
    [SerializeField] private string place;
    [SerializeField] private string eqFull;

    private void Start()
    {
        GameManager.uImanager = this;
        _opacityChangeStep = opacityChangeTime / 50f;
        interactText.enabled = false;
        pickUpText.enabled = false;
        var scene = SceneManager.GetActiveScene();
        levelText.text = "Stage " + scene.path[29] + ": " + scene.name.Replace('_', ' ');
    }

    private void Update()
    {
        if (GameManager.player.HasInteractable())
        {
            interactText.enabled = true;
        }
        else
        {
            interactText.enabled = false;
        }

        if (GameManager.player == null)
        {
            return;
        }

        if (GameManager.player.state == EqState.NoTile)
        {
            pickUpText.enabled = false;
            return;
        }

        pickUpText.enabled = true;

        switch (GameManager.player.state)
        {
            case EqState.CanPlace:
                pickUpText.text = place;
                break;
            case EqState.CanTake:
                pickUpText.text = pickUp;
                break;
            case EqState.CantPlace:
                pickUpText.text = eqFull;
                break;
        }
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
