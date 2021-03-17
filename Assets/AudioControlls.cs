using UnityEngine;
using UnityEngine.UI;

public class AudioControlls : MonoBehaviour
{
    [SerializeField] private CanvasGroup sliderGroup;
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider soundsVolume;
    public bool optionsAreVisible = false;

    private void Start()
    {
        masterVolume.SetValueWithoutNotify(GameSave.AudioVolume);
        musicVolume.SetValueWithoutNotify(GameSave.MusicVolume);
        soundsVolume.SetValueWithoutNotify(GameSave.SfxVolume);
    }

    public void ChangeOptionsVisibility()
    {
        optionsAreVisible = !optionsAreVisible;
        sliderGroup.alpha = optionsAreVisible ? 1f : 0f;
        sliderGroup.interactable = optionsAreVisible;
        sliderGroup.blocksRaycasts = optionsAreVisible;
    }

    public void SetMasterVolume(float value)
    {
        GameSave.AudioVolume = value;
    }

    public void SetMusicVolume(float value)
    {
        GameSave.MusicVolume = value;
    }

    public void SetSoundsVolume(float value)
    {
        GameSave.SfxVolume = value;
    }
}
