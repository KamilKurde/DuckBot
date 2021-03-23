using System.Collections.Generic;
using UnityEngine;

enum ManipulatorMode
{
    Add,
    Substract
}

public class Manipulator : PlaceableElement, IReceiver, ISource, IInteractable
{
    [SerializeField] private Material activatedMaterial;
    [SerializeField] private Material deactivatedMaterial;
    [Header("Inputs")]
    [SerializeField] private int inputChannel1 = -10;
    [SerializeField] private int inputChannel2 = -11;
    [Header("Outputs")]
    [SerializeField] private int outputChannel = -13;
    [SerializeField] private ManipulatorMode mode = ManipulatorMode.Add;
    private AudioSource _switchSound;

    private float _inputVoltage1 = 0f;
    private float _inputVoltage2 = 0f;
    private float _outputVoltage = 0f;

    private void Start()
    {
        _switchSound = GetComponent<AudioSource>();
        GameManager.GetChannel(inputChannel1).AddVoltageReceiver(this);
        GameManager.GetChannel(inputChannel2).AddVoltageReceiver(this);
        GameManager.GetChannel(outputChannel).AddVoltageSource(this);
        UpdateMaterials();
    }
    
    private void UpdateMaterials()
    {
        var materials = GetComponent<Renderer>().materials;
        switch (mode)
        {
            case ManipulatorMode.Add:
                materials[2] = activatedMaterial;
                materials[3] = deactivatedMaterial;
                break;
            case  ManipulatorMode.Substract:
                materials[2] = deactivatedMaterial;
                materials[3] = activatedMaterial;
                break;
        }

        GetComponent<Renderer>().materials = materials;
    }

    private void UpdateVoltages()
    {
        var channel1HaveHigherVoltage = _inputVoltage1 > _inputVoltage2;
        if (mode == ManipulatorMode.Add)
        {
            _outputVoltage = _inputVoltage1 + _inputVoltage2;
        }

        if (mode == ManipulatorMode.Substract)
        {
            _outputVoltage = channel1HaveHigherVoltage ? _inputVoltage1 - _inputVoltage2 : _inputVoltage2 - _inputVoltage1;
        }
        
        GameManager.GetChannel(outputChannel).UpdateVoltage();
        UpdateColor(1, _outputVoltage);
    }

    protected override void UpdateChannels(List<int> inputChannels, List<int> outputChannels)
    {
        ChangeReceiverChannel(ref inputChannel1, inputChannels[0]);
        ChangeReceiverChannel(ref inputChannel2, inputChannels[1]);
        ChangeSourceChannel(ref outputChannel, outputChannels[0]);
    }

    public void SetInput(float voltage, int channelId)
    {
        if (channelId == inputChannel1)
        {
            _inputVoltage1 = voltage;
        }

        if (channelId == inputChannel2)
        {
            _inputVoltage2 = voltage;
        }

        UpdateVoltages();
    }

    public float GetOutput(int channelId)
    {
        return _outputVoltage;
    }

    public void Interact()
    {
        switch (mode)
        {
            case ManipulatorMode.Add:
                mode = ManipulatorMode.Substract;
                break;
            case  ManipulatorMode.Substract:
                mode = ManipulatorMode.Add;
                break;
        }
        UpdateMaterials();
        _switchSound.Play();
        UpdateVoltages();
    }
}
