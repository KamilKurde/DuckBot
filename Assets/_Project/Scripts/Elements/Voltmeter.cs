using System.Globalization;
using TMPro;
using UnityEngine;

public class Voltmeter : Element, IListener
{
    [SerializeField] private TextMeshPro textMeshPro;
    [Header("Inputs")]
    [SerializeField] private int inputChannel;
    public void SetInput(float voltage)
    {
        textMeshPro.text = voltage.ToString(CultureInfo.InvariantCulture) + " V";
    }
    
    void Start()
    {
        ChannelManager.GetChannel(inputChannel).AddVoltageListener(this);
    }

}
