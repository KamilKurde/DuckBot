using UnityEngine;

public class Button : Element, ISource, IReceiver
{
    [SerializeField] private int inputChannel;
    [SerializeField] private int outputChannel;
    
    private float _voltage = 0f;

    public float GetOutput() { return _voltage; }

    public void SetInput(float voltage)
    {
        _voltage = voltage;
        UpdateColor(new []{1}, _voltage);
    }

    private void OnTriggerEnter(Collider other)
    {
        ChannelManager.GetChannel(outputChannel).AddVoltageSource(this);
    }

    private void OnTriggerExit(Collider other)
    {
        ChannelManager.GetChannel(outputChannel).RemoveVoltageSource(this);
    }

    // Start is called before the first frame update
    private void Start()
    {
        LightInit();
        ChannelManager.GetChannel(inputChannel).AddVoltageListeners(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
