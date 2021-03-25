using DG.Tweening;
using UnityEngine;

public class Settings : MonoBehaviour
{
    internal float shortAnimTime = GameManager.shortAnimationLenght;
    internal float mediumAnimTime = GameManager.mediumAnimationLenght;
    [SerializeField] private CanvasGroup confirmation;
    private GameObject _musicManager;

    private CanvasGroup _canvasGroup;
    
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnResetButtonClicked()
    {
        confirmation.DOFade(1f, shortAnimTime).SetUpdate(true);
        confirmation.interactable = true;
        confirmation.blocksRaycasts = true;
    }

    public void OnConfirmationButtonClicked()
    {
        if (GameManager.uiManager != null)
        {
            _musicManager = GameObject.Find("MusicManager");
            if (_musicManager != null)
            {
                _musicManager.SetActive(false);
            }
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
        _canvasGroup.DOFade(state ? 1f : 0f, shortAnimTime).SetUpdate(true);
        _canvasGroup.interactable = state;
        _canvasGroup.blocksRaycasts = state;

        confirmation.alpha = 0f;
        confirmation.interactable = false;
        confirmation.blocksRaycasts = false;
        
        if (GameManager.uiManager == null)
        {
            return;
        }
        
        GameManager.uiManager.pauseGroup.DOFade(state ? 0f : 1f, shortAnimTime).SetUpdate(true);
        GameManager.uiManager.pauseGroup.interactable = !state;
        GameManager.uiManager.pauseGroup.blocksRaycasts = !state;
    }

    public void OnCloseButtonClicked()
    {
        ChangeVisibilityTo(false);
        GameSave.SaveGame();
    }
}
