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
            _light.enabled = true;
            requirementFullfiled = true;
            GameManager.CheckRequirements();
        }
        else
        {
            textMeshPro.color = Color.gray;
            _light.enabled = false;
            requirementFullfiled = false;
        }
    }

    private void Start()
    {
        LightInit();
        textMeshPro.text = requiredVoltage.ToString(CultureInfo.InvariantCulture);
        GameManager.AddEndpoint(this);
        GameManager.GetChannel(inputChannel).AddVoltageReceiver(this);
    }
}
