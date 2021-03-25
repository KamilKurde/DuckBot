using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UI : MonoBehaviour
{
    internal float shortAnimTime = GameManager.shortAnimationLenght;
    internal float mediumAnimTime = GameManager.mediumAnimationLenght;
    [SerializeField] internal Image fadeImage;
    [Range(0.01f, 5f)]public float opacityChangeTime;

    private void OnEnable()
    {
        GameManager.audioMixer = GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
        DOTween.SetTweensCapacity(500, 50);
    }

    internal void Start()
    {
        Time.timeScale = 1f;
        fadeImage.DOFade(0f, opacityChangeTime);
        GameSave.LoadAudioSettings();
    }

    internal void LoadScene(int sceneIndex)
    {
        fadeImage.DOFade(1f, opacityChangeTime).SetUpdate(true).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneIndex);
        });
        GameManager.Clear();
    }
}
