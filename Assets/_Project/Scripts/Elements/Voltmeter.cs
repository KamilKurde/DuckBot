using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Voltmeter : PlaceableElement, IListener
{
    [SerializeField] private TextMeshPro voltageText;
    [Header("Inputs")]
    [SerializeField] private int inputChannel = -10;

    private float _voltage;

    private string VoltageAsString => _voltage.ToString("N1").Replace(',', '.') + " U";

    public void SetInput(float voltage, int id)
    {
        if (Math.Abs(voltage - _voltage) < 0.1f)
        {
            voltageText.text = VoltageAsString;
            return;
        }
        
        _voltage = voltage;
        
        voltageText.DOFade(0f, 0.1f).OnComplete(() =>
        {
            voltageText.text = VoltageAsString;
            voltageText.DOFade(1f, 0.1f);
        });
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
