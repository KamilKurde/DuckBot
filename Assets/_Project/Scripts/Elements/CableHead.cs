using UnityEngine;

public class CableHead : Element, ISource
{
    [SerializeField] private int channel;
    [SerializeField] private float voltage = 0f;
    public float GetOutput()
    {
        return voltage;
    }

    private void Start()
    {
        LightInit();
        UpdateColor(new []{0}, voltage);
        GameManager.GetChannel(channel).AddVoltageSource(this);
    }
}
