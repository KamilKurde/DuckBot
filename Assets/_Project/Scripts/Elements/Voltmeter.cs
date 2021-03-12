using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Voltmeter : PlaceableElement, IListener
{
    [SerializeField] private TextMeshPro voltageText;
    [Header("Inputs")]
    [SerializeField] private int inputChannel;

    public void SetInput(float voltage)
    {
        voltageText.text = voltage.ToString(CultureInfo.InvariantCulture) + " U";
    }
    
    private void Start()
    {
        GameManager.GetChannel(inputChannel).AddVoltageListener(this);
    }

    protected override void UpdateChannels(List<int> inputChannels, List<int> outputChannels)
    {
        ChangeLinstenerChannel(ref inputChannel, inputChannels[0]);
    }
}
