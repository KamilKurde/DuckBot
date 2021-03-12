using System.Collections.Generic;
using UnityEngine;

public class DirectionController : PlaceableElement, IInteractable, IReceiver, ISource
{
    [SerializeField] private int channel1 = 0;
    [SerializeField] private int channel2 = 0;
    [SerializeField] private bool firstChannelIsInput = true;
    private float _voltage = 0f;
    void Start()
    {
        GameManager.GetChannel(channel1).AddVoltageReceiver(this);
        GameManager.GetChannel(channel2).AddVoltageSource(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void UpdateChannels(List<int> inputChannels, List<int> outputChannels)
    {
        if (firstChannelIsInput)
        {
            ChangeReceiverChannel(ref channel1, inputChannels[0]);
            ChangeSourceChannel(ref channel2, outputChannels[0]);
        }
        else
        {
            ChangeSourceChannel(ref channel1, outputChannels[0]);
            ChangeReceiverChannel(ref channel2, inputChannels[0]);
        }
    }

    public void Interact()
    {
        firstChannelIsInput = !firstChannelIsInput;
        if (firstChannelIsInput)
        {
            GameManager.GetChannel(channel1).RemoveVoltageSource(this);
            GameManager.GetChannel(channel1).AddVoltageReceiver(this);
            
            GameManager.GetChannel(channel2).RemoveVoltageReceiver(this);
            GameManager.GetChannel(channel2).AddVoltageSource(this);
        }
        else
        {
            GameManager.GetChannel(channel2).RemoveVoltageSource(this);
            GameManager.GetChannel(channel2).AddVoltageReceiver(this);
            
            GameManager.GetChannel(channel1).RemoveVoltageReceiver(this);
            GameManager.GetChannel(channel1).AddVoltageSource(this);
        }
    }

    public void SetInput(float voltage)
    {
        _voltage = voltage;
        GameManager.GetChannel(firstChannelIsInput ? channel1 : channel2).UpdateVoltage();
    }

    public float GetOutput()
    {
        return _voltage;
    }
}
