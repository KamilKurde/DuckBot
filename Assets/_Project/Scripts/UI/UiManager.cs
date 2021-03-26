using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : UI
{
    [Header("Groups")]
    [SerializeField] public CanvasGroup uiGroup;
    [SerializeField] private CanvasGroup levelEndedGroup;
    [SerializeField] public CanvasGroup pauseGroup;
    [SerializeField] private CanvasGroup contineButtonGroup;
    [Header("Images")]
    [SerializeField] private Image eqState;
    [SerializeField] private Sprite emptyEqTexture;
    [SerializeField] private Sprite fullEqTexture;
    [Header("Texts")]
    [SerializeField] private Text levelText;
    [SerializeField] private Text interactText;
    [SerializeField] private Text pickUpText;
    [SerializeField] private Text levelFinishedText;
    [Header("Strings")]
    [SerializeField] private string pickUp;
    [SerializeField] private string place;
    [SerializeField] private string eqFull;
    [SerializeField] private string levelFinished;
    [SerializeField] private string gameFinished;
    [Header("Scripts")]
    [SerializeField] private FullScreenInfo endingScript;

    [SerializeField] public Settings settingsScript;

    private int _currentBuildIndex;

    private void Awake()
    {
        GameManager.uiManager = this;
        interactText.enabled = false;
        pickUpText.enabled = false;
        levelText.text = SceneManager.GetActiveScene().name;
        _currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
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

    private void EndLevel()
    {
        if (_currentBuildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            LoadMenu();
            return;
        }
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
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
        levelFinishedText.text = levelFinished;
        if (_currentBuildIndex + 1 > GameSave.CurrentLevelId)
        {
            if (_currentBuildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                GameSave.CurrentLevelId = _currentBuildIndex + 1;
            }
            else
            {
                levelFinishedText.text = gameFinished;
                contineButtonGroup.alpha = 0f;
                contineButtonGroup.interactable = false;
                contineButtonGroup.blocksRaycasts = false;
                endingScript.Show();
            }
        }
        pauseGroup.DOFade(0f, opacityChangeTime);
        pauseGroup.interactable = false;
        pauseGroup.blocksRaycasts = false;
        uiGroup.DOFade(0f, opacityChangeTime);
        levelEndedGroup.DOFade(1f, opacityChangeTime);
        levelEndedGroup.interactable = true;
        levelEndedGroup.blocksRaycasts = true;
    }

    public void OnContinueButtonClicked()
    {
        EndLevel();
    }
}
