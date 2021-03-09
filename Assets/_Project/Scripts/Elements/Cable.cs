using UnityEngine;

public class Cable : Element, IListener
{
    // Channel from which cable takes voltage
    [SerializeField] private float voltage = 0f;
    [Header("Inputs")]
    [SerializeField] private int inputChannel;
    
    // Invoked when voltage on channel changes
    public void SetInput(float voltage)
    {
        this.voltage = voltage;
        UpdateColor( new[] {0}, this.voltage);
    }

    private void Start()
    {
        LightInit();
        ChannelManager.GetChannel(inputChannel).AddVoltageListener(this);
    }
}
