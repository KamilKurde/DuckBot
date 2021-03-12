using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;

    public Menu menu;
    public int levelNumber = 0;
    
    private static string NameFromIndex(int buildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }
    
    void Start()
    {
        /*var scene = SceneUtility.GetScenePathByBuildIndex(levelNumber);
        Debug.Log(levelNumber);
        var sceneName = scene.name.Replace('_', ' ');*/
        textMeshPro.text = NameFromIndex(levelNumber);
    }

    public void LoadScene()
    {
        menu.LoadScene(levelNumber);
    }
}