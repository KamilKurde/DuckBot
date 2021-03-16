using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class LevelSelectionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;

    public Menu menu;
    public int levelNumber = 0;
    
    private static string NameFromIndex(int buildIndex)
    {
        var path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
        var slash = path.LastIndexOf('/');
        var name = path.Substring(slash + 1);
        var dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }
    
    void Start()
    {
        textMeshPro.text = NameFromIndex(levelNumber);
    }

    public void LoadScene()
    {
        menu.LoadScene(levelNumber);
    }
}