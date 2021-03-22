using DG.Tweening;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private CanvasGroup confirmation;
    private GameObject _musicManager;

    private CanvasGroup _canvasGroup;
    
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnResetButtonClicked()
    {
        confirmation.DOFade(1f, 0.5f);
        confirmation.interactable = true;
        confirmation.blocksRaycasts = true;
    }

    public void OnConfirmationButtonClicked()
    {
        if (GameManager.uiManager != null)
        {
            _musicManager = GameObject.Find("MusicManager");
            _musicManager.SetActive(false);
            GameSave.CurrentLevelId = 1;
            GameManager.uiManager.LoadMenu();
        }
        else
        {
            FindObjectOfType<Menu>().Reset();
        }
    }

    public void ChangeVisibilityTo(bool state)
    {
        _canvasGroup.alpha = state ? 1f : 0f;
        _canvasGroup.interactable = state;
        _canvasGroup.blocksRaycasts = state;
        
        if (GameManager.uiManager == null)
        {
            return;
        }
        
        GameManager.uiManager.pauseGroup.alpha = state ? 0f : 1f;
        GameManager.uiManager.pauseGroup.interactable = !state;
        GameManager.uiManager.pauseGroup.blocksRaycasts = !state;
    }

    public void OnCloseButtonClicked()
    {
        ChangeVisibilityTo(false);
        GameSave.SaveGame();
    }
}
