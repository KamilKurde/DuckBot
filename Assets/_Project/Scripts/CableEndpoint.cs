using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CableEndpoint : Element, IReceiver
{
    [SerializeField] private float requiredVoltage = 0;
    [SerializeField] private int channel;
    [SerializeField] private Player player;
    [SerializeField] private TextMeshPro textMeshPro;
    [HideInInspector] public bool requirementFullfiled = false;
    public void SetInput(float voltage)
    {
        // If current and required voltage are closer than 0.1V
        if (Math.Abs(voltage - requiredVoltage) < 0.1f)
        {
            textMeshPro.color = Color.white;
            _light.enabled = true;
            requirementFullfiled = true;
        }
        else
        {
            textMeshPro.color = Color.gray;
            _light.enabled = false;
            requirementFullfiled = false;
        }
        player.CheckRequirements();
    }

    private void Start()
    {
        LightInit();
        textMeshPro.text = requiredVoltage.ToString(CultureInfo.InvariantCulture);
        ChannelManager.GetChannel(channel).AddVoltageListeners(this);
    }
}
