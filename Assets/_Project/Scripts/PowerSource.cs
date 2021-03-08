using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerSource : Element, Source
{
    [SerializeField] private int channel;
    [SerializeField] private float voltage = 0f;
    public float GetOutput()
    {
        return voltage;
    }
    // Start is called before the first frame update
    void Start()
    {
        ChannelManager.GetChannel(channel).AddVoltageSource(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
