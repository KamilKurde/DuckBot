using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public readonly Channel[] channels = new Channel[10];

    // Start is called before the first frame update
    public void reInitialize()
    {
        if (channels[0] == null)
        {
            for (int i = 0; i < channels.Length; i++)
            {
                channels[i] = new Channel();
            }
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
