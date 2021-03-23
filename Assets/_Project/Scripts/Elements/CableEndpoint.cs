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
    public void SetInput(float voltage, int id)
    {
        // If current and required voltage are closer than 0.1V
        if (Math.Abs(voltage - requiredVoltage) < 0.1f)
        {
            textMeshPro.color = Color.white;
            requirementFullfiled = true;
            UpdateColor(1, voltage);
        }
        else
        {
            textMeshPro.color = Color.gray;
            requirementFullfiled = false;
            UpdateColor(1, 0f);
        }
        GameManager.CheckRequirements();
    }

    private void Start()
    {
        GameManager.AddEndpoint(this);
        LightInit();
        textMeshPro.text = requiredVoltage.ToString("F1", CultureInfo.InvariantCulture) + " V";
        GameManager.GetChannel(inputChannel).AddVoltageReceiver(this);
    }
}
