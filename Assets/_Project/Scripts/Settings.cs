using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private CanvasGroup _confirmation;
    private GameObject musicManager;

    private CanvasGroup canvasGroup;
    
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (GameManager.uImanager == null)
        {
            return;
        }
        
    }

    public void OnResetButtonClicked()
    {
        _confirmation.alpha = 1f;
        _confirmation.interactable = true;
        _confirmation.blocksRaycasts = true;
    }

    public void OnConfirmationButtonClicked()
    {
        if (GameManager.uImanager != null)
        {
            musicManager = GameObject.Find("MusicManager");
            musicManager.SetActive(false);
            GameSave.CurrentLevelId = 1;
            GameManager.uImanager.LoadMenu();
        }
        else
        {
            FindObjectOfType<Menu>().Reset();
        }
    }

    public void ChangeVisibilityTo(bool state)
    {
        canvasGroup.alpha = state ? 1f : 0f;
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
        
        if (GameManager.uImanager == null)
        {
            return;
        }
        
        GameManager.uImanager.pauseGroup.alpha = state ? 0f : 1f;
        GameManager.uImanager.pauseGroup.interactable = !state;
        GameManager.uImanager.pauseGroup.blocksRaycasts = !state;
    }

    public void OnCloseButtonClicked()
    {
        ChangeVisibilityTo(false);
    }
}
