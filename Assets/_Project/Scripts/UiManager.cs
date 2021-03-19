using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : UI
{
    [Header("Groups")]
    [SerializeField] private CanvasGroup uiGroup;
    [SerializeField] private CanvasGroup levelEndedGroup;
    [SerializeField] public CanvasGroup pauseGroup;
    [Header("Images")]
    [SerializeField] private Image eqState;
    [SerializeField] private Sprite emptyEqTexture;
    [SerializeField] private Sprite fullEqTexture;
    [Header("Texts")]
    [SerializeField] private Text levelText;
    [SerializeField] private Text interactText;
    [SerializeField] private Text pickUpText;
    [Header("Strings")]
    [SerializeField] private string pickUp;
    [SerializeField] private string place;
    [SerializeField] private string eqFull;

    private void Awake()
    {
        GameManager.uiManager = this;
        interactText.enabled = false;
        pickUpText.enabled = false;
        var scene = SceneManager.GetActiveScene();
        levelText.text = "Stage " + scene.path[29] + ": " + scene.name;
    }

    private void Update()
    {
        //HandleOpacityChange(Time.deltaTime);
        interactText.enabled = GameManager.player.HasInteractable();

        if (GameManager.player == null)
        {
            return;
        }

        pickUpText.enabled = true;

        switch (GameManager.player.state)
        {
            case EqState.NoEq:
                pickUpText.enabled = false;
                eqState.sprite = emptyEqTexture;
                break;
            case EqState.NoTile:
                pickUpText.enabled = false;
                break;
            case EqState.CanTake:
                pickUpText.text = pickUp;
                eqState.sprite = emptyEqTexture;
                break;
            case EqState.CanPlace:
                pickUpText.text = place;
                eqState.sprite = fullEqTexture;
                break;
            case EqState.CantPlace:
                pickUpText.text = eqFull;
                break;
        }
    }

    public void EndLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            LoadMenu();
        }
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart()
    {
        GameManager.player.SetPauseState(false);
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        GameManager.player.SetPauseState(false);
        LoadScene(0);
    }

    private void OnDestroy()
    {
        GameManager.uiManager = null;
    }

    public void OnResumeButtonClicked()
    {
        GameManager.player.SetPauseState(false);
    }

    public void DisplayLevelEndedGroup()
    {
        pauseGroup.DOFade(0f, 0.5f);
        pauseGroup.interactable = false;
        pauseGroup.blocksRaycasts = false;
        uiGroup.DOFade(0f, 0.5f);
        levelEndedGroup.DOFade(1f, 0.5f);
        levelEndedGroup.interactable = true;
        levelEndedGroup.blocksRaycasts = true;
    }

    public void OnContinueButtonClicked()
    {
        EndLevel();
    }
}
