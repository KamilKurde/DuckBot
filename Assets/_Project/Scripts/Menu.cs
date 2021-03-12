using UnityEngine;

public class Menu : UI
{
    [SerializeField] private CanvasGroup selectLevelGroup;
    [SerializeField] private GameObject buttonPrefab;

    private void Awake()
    {
        _opacityChangeStep = opacityChangeTime / 50f;
        for (var i = 0; i < GameSave.currentLevelId; i++)
        {
            var button = Instantiate(buttonPrefab, selectLevelGroup.transform, false);
            button.GetComponent<LevelSelectionButton>().menu = this;
            button.GetComponent<LevelSelectionButton>().levelNumber = i + 1;
            button.GetComponent<Transform>().position = button.GetComponent<Transform>().position + Vector3.down * (160f * (i % 5));
            button.GetComponent<Transform>().position = button.GetComponent<Transform>().position + Vector3.right * (320f * ( i / 5 ));
        }
    }
    public void OnPlayButtonClicked()
    {
        LoadScene(GameSave.currentLevelId);
    }

    public void OnSelectLevelButtonClicked()
    {
        var isVisible = selectLevelGroup.interactable;
        selectLevelGroup.alpha = isVisible ? 0f : 1f;
        selectLevelGroup.interactable = !isVisible;
    }
}
