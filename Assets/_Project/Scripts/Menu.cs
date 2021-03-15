using UnityEngine;

public class Menu : UI
{
    [SerializeField] private CanvasGroup selectLevelGroup;
    [SerializeField] private GameObject buttonPrefab;

    private void Awake()
    {
        for (var i = 0; i < GameSave.CurrentLevelId; i++)
        {
            var button = Instantiate(buttonPrefab, selectLevelGroup.transform, false);
            button.GetComponent<LevelSelectionButton>().menu = this;
            button.GetComponent<LevelSelectionButton>().levelNumber = i + 1;
        }
    }
    public void OnPlayButtonClicked()
    {
        LoadScene(GameSave.CurrentLevelId);
    }

    public void OnSelectLevelButtonClicked()
    {
        var isVisible = selectLevelGroup.interactable;
        selectLevelGroup.alpha = isVisible ? 0f : 1f;
        selectLevelGroup.interactable = !isVisible;
    }

    public void OnResetButtonClicked()
    {
        GameSave.CurrentLevelId = 1;
        dimmingActive = true;
        LoadScene(0);
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
