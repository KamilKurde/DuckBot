using System.Collections.Generic;
using UnityEngine;

public class DirectionController : PlaceableElement, IInteractable, IReceiver, ISource
{
    [Header("Input")]
    [SerializeField] private int inputChannel = -10;
    [Header("Outputs")]
    [SerializeField] private int outputChannel1 = -13;
    [SerializeField] private int outputChannel2 = -14;
    
    [SerializeField] private bool firstChannelIsActive = true;

    private int CurrentOutput
    {
        get => firstChannelIsActive ? outputChannel1 : outputChannel2;
    }
    private float _voltage = 0f;
    void Start()
    {
        GameManager.GetChannel(inputChannel).AddVoltageReceiver(this);
        GameManager.GetChannel(firstChannelIsActive ? outputChannel1 : outputChannel2).AddVoltageSource(this);
        
    }

    protected override void UpdateChannels(List<int> inputChannels, List<int> outputChannels)
    {
        GameManager.RemoveAllReferencesTo(this);
        inputChannel = inputChannels[0];
        outputChannel1 = outputChannels[0];
        outputChannel2 = outputChannels[1];
        GameManager.GetChannel(inputChannel).AddVoltageReceiver(this);
        GameManager.GetChannel(CurrentOutput).AddVoltageSource(this);
    }

    private void UpdateColors()
    {
        UpdateColor(new []{1, firstChannelIsActive ? 3 : 2}, _voltage);
        UpdateColor(firstChannelIsActive ? 2 : 3, 0f);
    }

    public void Interact()
    {
        GetComponent<AudioSource>().Play();
        GameManager.GetChannel(CurrentOutput).RemoveVoltageSource(this);
        firstChannelIsActive = !firstChannelIsActive;
        GameManager.GetChannel(CurrentOutput).AddVoltageSource(this);
        UpdateColors();
    }

    public void SetInput(float voltage, int id)
    {
        _voltage = voltage;
        UpdateColors();
        GameManager.GetChannel(CurrentOutput).UpdateVoltage();
    }

    public float GetOutput(int id)
    {
        return _voltage;
    }
}
