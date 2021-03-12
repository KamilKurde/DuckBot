using System.Collections.Generic;
using UnityEngine;

public class Switch : PlaceableElement, ISource, IReceiver, IInteractable
{
    [Header("Inputs")]
    [SerializeField] private int inputChannel = 0;
    [Header("Outputs")]
    [SerializeField] private int outputChannel = 0;
    // Start is called before the first frame update
    private bool isActive = false;
    private Material lightMaterial;
    
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
            material = voltageMaterials[0];
        }

        materials[2] = material;
        GetComponent<Renderer>().materials = materials;
    }

    public void SetInput(float voltage)
    {
        _voltage = voltage;
        UpdateColor(new []{1}, _voltage);
        GameManager.GetChannel(outputChannel).UpdateVoltage();
    }

    // Start is called before the first frame update
    private void Start()
    {
        lightMaterial = GetComponent<Renderer>().materials[2];
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
