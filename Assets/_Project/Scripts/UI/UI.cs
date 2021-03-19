using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UI : MonoBehaviour
{
    [SerializeField] internal Image fadeImage;
    [Range(0.01f, 5f)]public float opacityChangeTime;

    internal void Start()
    {
        GameManager.audioMixer = GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
        GameSave.LoadAudioSettings();
        fadeImage.DOFade(0f, opacityChangeTime);
    }

    internal void LoadScene(int sceneIndex)
    {
        fadeImage.DOFade(1f, opacityChangeTime).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneIndex);
        });
        GameManager.Clear();
    }
}
