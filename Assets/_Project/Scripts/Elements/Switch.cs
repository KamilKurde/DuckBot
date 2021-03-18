using System.Collections.Generic;
using UnityEngine;

public class Switch : PlaceableElement, ISource, IReceiver, IInteractable
{
    [Header("Inputs")]
    [SerializeField] private int inputChannel = 0;
    [Header("Outputs")]
    [SerializeField] private int outputChannel = 0;
    private bool _isActive = false;
    private AudioSource _switchSound;
    [SerializeField] private Material lightMaterial;
    [SerializeField] private Material darkMaterial;
    
    private float _voltage = 0f;

    public float GetOutput() { return _voltage; }

    private void SetLight(bool state)
    {
        var materials = GetComponent<Renderer>().materials;
        Material material = null;
        if (state)
        {
            material = lightMaterial;
        }
        else
        {
            material = darkMaterial;
        }

        materials[2] = material;
        GetComponent<Renderer>().materials = materials;
    }

    public void SetInput(float voltage)
    {
        _voltage = voltage;
        UpdateColor(1, _voltage);
        GameManager.GetChannel(outputChannel).UpdateVoltage();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _switchSound = GetComponent<AudioSource>();
        SetLight(false);
        GameManager.GetChannel(inputChannel).AddVoltageListener(this);
    }

    protected override void UpdateChannels(List<int> inputChannels, List<int> outputChannels)
    {
        ChangeReceiverChannel(ref inputChannel, inputChannels[0]);
        ChangeSourceChannel(ref outputChannel, outputChannels[0]);
    }

    public void Interact()
    {
        _isActive = !_isActive;
        if (_isActive)
        {
            GameManager.GetChannel(outputChannel).AddVoltageSource(this);
            ChangeListenerToReceiver(inputChannel);
            _switchSound.Play();
        }
        else
        {
            GameManager.GetChannel(outputChannel).RemoveVoltageSource(this);
            ChangeReceiverToListner(inputChannel);
            _switchSound.Play();
        }
        SetLight(_isActive);
    }
}
