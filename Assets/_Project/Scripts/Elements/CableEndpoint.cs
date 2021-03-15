using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CableEndpoint : Element, IReceiver
{
    [SerializeField] private TextMeshPro textMeshPro;
    [HideInInspector] public bool requirementFullfiled = false;
    [SerializeField] private float requiredVoltage = 0;
    [Header("Inputs")]
    [SerializeField] private int inputChannel;
    public void SetInput(float voltage)
    {
        // If current and required voltage are closer than 0.1V
        if (Math.Abs(voltage - requiredVoltage) < 0.1f)
        {
            textMeshPro.color = Color.white;
            if (_light != null)
            {
                _light.enabled = true;
            }
            requirementFullfiled = true;
        }
        else
        {
            textMeshPro.color = Color.gray;
            if (_light != null)
            {
                _light.enabled = false;
            }
            requirementFullfiled = false;
        }
        GameManager.CheckRequirements();
    }

    private void Start()
    {
        GameManager.AddEndpoint(this);
        LightInit();
        textMeshPro.text = requiredVoltage.ToString(CultureInfo.InvariantCulture);
        GameManager.GetChannel(inputChannel).AddVoltageReceiver(this);
    }
}
