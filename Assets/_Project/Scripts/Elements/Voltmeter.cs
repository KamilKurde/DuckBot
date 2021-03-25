using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Voltmeter : PlaceableElement, IListener
{
    [SerializeField] private TextMeshPro voltageText;
    [Header("Inputs")]
    [SerializeField] private int inputChannel = -10;

    public void SetInput(float voltage, int id)
    {
        voltageText.text = voltage.ToString("N1").Replace(',', '.') + " U";
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
