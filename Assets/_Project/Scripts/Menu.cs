using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Menu : UI
{
    [SerializeField] private CanvasGroup selectLevelGroup;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Image tutorialImage;
    private GameObject musicManager;

    private void Awake()
    {
        for (var i = 0; i < GameSave.CurrentLevelId; i++)
        {
            var button = Instantiate(levelButtonPrefab, selectLevelGroup.transform, false);
            button.GetComponent<LevelSelectionButton>().menu = this;
            button.GetComponent<LevelSelectionButton>().levelNumber = i + 1;
        }
    }
    
    public void OnPlayButtonClicked()
    {
        LoadScene(GameSave.CurrentLevelId);
    }

    public void OnTutorialButtonClicked()
    {

        if (selectLevelGroup.interactable)
        {
            OnSelectLevelButtonClicked();
        }

        if (tutorialImage.enabled)
        {
            tutorialImage.DOFade(0f, 0.5f).OnComplete(() => {
                tutorialImage.enabled = false;
            });
        }
        else
        {
            tutorialImage.enabled = true;
            tutorialImage.DOFade(1f, 0.5f);
        }
        //tutorialImage.enabled = !tutorialImage.enabled;
    }

    public void OnSelectLevelButtonClicked()
    {
        if (tutorialImage.enabled)
        {
            tutorialImage.enabled = false;
        }
        
        var isVisible = selectLevelGroup.interactable;
        selectLevelGroup.alpha = isVisible ? 0f : 1f;
        selectLevelGroup.interactable = !isVisible;
        selectLevelGroup.blocksRaycasts = !isVisible;
    }

    public void Reset()
    {
        musicManager = GameObject.Find("MusicManager");
        musicManager.SetActive(false);
        GameSave.CurrentLevelId = 1;
        dimmingActive = true;
        LoadScene(0);
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
