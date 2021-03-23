using System.Collections.Generic;
using UnityEngine;

public class Switch : PlaceableElement, ISource, IReceiver, IInteractable
{
    [SerializeField] private bool isActive = false;
    [Header("Inputs")]
    [SerializeField] public int inputChannel = -10;
    [Header("Outputs")]
    [SerializeField] public int outputChannel = -13;
    private AudioSource _switchSound;
    [SerializeField] private Material lightMaterial;
    [SerializeField] private Material darkMaterial;
    
    private float _voltage = 0f;

    public float GetOutput(int id) { return _voltage; }

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

    public void SetInput(float voltage, int id)
    {
        _voltage = voltage;
        UpdateColor(1, _voltage);
        GameManager.GetChannel(outputChannel).UpdateVoltage();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _switchSound = GetComponent<AudioSource>();
        SetLight(isActive);
        GameManager.GetChannel(inputChannel).AddVoltageListener(this);
    }

    protected override void UpdateChannels(List<int> inputChannels, List<int> outputChannels)
    {
        isActive = false;
        SetLight(isActive);
        GameManager.RemoveAllReferencesTo(this);
        inputChannel = inputChannels[0];
        outputChannel = outputChannels[0];
        GameManager.GetChannel(inputChannel).AddVoltageListener(this);
    }

    public void Interact()
    {
        _switchSound.Play();
        isActive = !isActive;
        if (isActive)
        {
            GameManager.GetChannel(outputChannel).AddVoltageSource(this);
            ChangeListenerToReceiver(inputChannel);
        }
        else
        {
            GameManager.GetChannel(outputChannel).RemoveVoltageSource(this);
            ChangeReceiverToListner(inputChannel);
        }
        SetLight(isActive);
    }
}
