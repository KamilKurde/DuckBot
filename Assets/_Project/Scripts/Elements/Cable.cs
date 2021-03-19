using UnityEngine;

public class Cable : Element, IListener
{
    // Channel from which cable takes voltage
    [SerializeField] private float _voltage = 0f;
    [Header("Inputs")]
    [SerializeField] private int inputChannel;
    
    // Invoked when voltage on channel changes
    public void SetInput(float voltage, int id)
    {
        _voltage = voltage;
        UpdateColor( 0, voltage);
    }

    private void Start()
    {
        GameManager.GetChannel(inputChannel).AddVoltageListener(this);
    }
}
