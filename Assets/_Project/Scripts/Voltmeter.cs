using System.Globalization;
using TMPro;
using UnityEngine;

public class Voltmeter : Element, IReceiver
{
    [SerializeField] private int channel;
    [SerializeField] private TextMeshPro textMeshPro;
    public void SetInput(float voltage)
    {
        textMeshPro.text = voltage.ToString(CultureInfo.InvariantCulture) + " V";
    }
    
    void Start()
    {
        ChannelManager.GetChannel(channel).AddVoltageListeners(this);
    }

}
