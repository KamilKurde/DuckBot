using UnityEngine;
using UnityEngine.UI;

public class AudioControlls : MonoBehaviour
{
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider soundsVolume;

    private void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        masterVolume.SetValueWithoutNotify(GameSave.AudioVolume);
        musicVolume.SetValueWithoutNotify(GameSave.MusicVolume);
        soundsVolume.SetValueWithoutNotify(GameSave.SfxVolume);
        GameSave.LoadAudioSettings();
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
