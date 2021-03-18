using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : UI
{
    [SerializeField] public CanvasGroup pauseGroup;
    [SerializeField] private Image eqState;
    [SerializeField] private Sprite emptyEqTexture;
    [SerializeField] private Sprite fullEqTexture;
    [SerializeField] private Text levelText;
    [SerializeField] private Text interactText;
    [SerializeField] private Text pickUpText;
    [Header("Texts")]
    [SerializeField] private string pickUp;
    [SerializeField] private string place;
    [SerializeField] private string eqFull;

    private void Awake()
    {
        GameManager.uImanager = this;
        interactText.enabled = false;
        pickUpText.enabled = false;
        var scene = SceneManager.GetActiveScene();
        levelText.text = "Stage " + scene.path[29] + ": " + scene.name;
    }

    private void Update()
    {
        HandleOpacityChange(Time.deltaTime);
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
        dimmingActive = true;
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        GameManager.player.SetPauseState(false);
        dimmingActive = true;
        LoadScene(0);
    }

    private void OnDestroy()
    {
        GameManager.uImanager = null;
    }

    public void OnResumeButtonClicked()
    {
        GameManager.player.SetPauseState(false);
    }
}
