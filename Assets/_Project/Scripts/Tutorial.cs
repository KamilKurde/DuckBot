using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private GameObject tutorial_image;
    int lifeTime = 10;

    void Start()
    {
        tutorial_image = GameObject.Find("Tutorial");
        Destroy(tutorial_image, lifeTime);
    }
}