using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Button : Element, Source, Receiver
{
    [SerializeField] private int inputChannel;
    [SerializeField] private int outputChannel;
    
    private float _voltage = 0f;

    public float GetOutput() { return _voltage; }

    public void SetInput(float voltage) { _voltage = voltage; }

    private void OnTriggerEnter(Collider other)
    {
        ChannelManager.GetChannel(inputChannel).AddVoltageListeners(this);
        ChannelManager.GetChannel(outputChannel).AddVoltageSource(this);
    }

    private void OnTriggerExit(Collider other)
    {
        ChannelManager.GetChannel(outputChannel).RemoveVoltageSource(this);
        ChannelManager.GetChannel(inputChannel).RemoveVoltageListeners(this);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
