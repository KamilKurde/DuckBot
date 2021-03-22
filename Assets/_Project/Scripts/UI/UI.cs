using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UI : MonoBehaviour
{
    [SerializeField] internal Image fadeImage;
    [Range(0.01f, 5f)]public float opacityChangeTime;

    private void OnEnable()
    {
        GameManager.audioMixer = GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
    }

    internal void Start()
    {
        fadeImage.DOFade(0f, opacityChangeTime);
        GameSave.LoadAudioSettings();
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
