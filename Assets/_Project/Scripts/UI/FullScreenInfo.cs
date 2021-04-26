using DG.Tweening;
using MyBox;
using TMPro;
using UnityEngine;

public class FullScreenInfo : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    [SerializeField] private bool appendVersionAtTheEnd = false;

    [ConditionalField("appendVersionAtTheEnd"), SerializeField] private TextMeshProUGUI TMP;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        if (appendVersionAtTheEnd)
        {
            TMP.text += Application.version;
        }
    }

    public void Show()
    {
        _canvasGroup.DOFade(1f, 0.5f).SetUpdate(true);
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        _canvasGroup.DOFade(0f, 0.5f).SetUpdate(true);
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}