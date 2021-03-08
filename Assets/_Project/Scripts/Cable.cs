using UnityEngine;

public class Cable : Element, IReceiver
{
    // Channel from which cable takes voltage
    [SerializeField] private int channel;
    [SerializeField] private float _voltage = 0f;
    
    // Invoked when voltage on channel changes
    public void SetInput(float voltage)
    {
        _voltage = voltage;
        UpdateColor( new[] {0}, _voltage);
    }

    private void Start()
    {
        LightInit();
        ChannelManager.GetChannel(channel).AddVoltageListeners(this);
    }
}
