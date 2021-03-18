using UnityEngine;
using NaughtyAttributes;

public class DDOL : MonoBehaviour
{
    [Tag]
    [SerializeField] private string objectTag;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        if (objectTag != "" && GameObject.FindGameObjectsWithTag(objectTag).Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
