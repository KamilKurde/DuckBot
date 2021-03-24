using TMPro;
using UnityEngine;

public class ReceiverAntenna : Element, IReceiver, ISource, IInteractable
{
    [SerializeField] private ReceiverAntennaSatelite _satelite;
    [SerializeField] private TextMeshPro channelInfoText;

    [Header("Outputs")] 
    [SerializeField] private int outputChannel = 0;

    [SerializeField, Range(1, 3)] private int currentChannel;

    [SerializeField] private float _voltage = 0f;

    private AudioSource radio_sound;

    private void Start()
    {
        radio_sound = GetComponent<AudioSource>();
        GameManager.GetChannel(-currentChannel).AddVoltageReceiver(this);
        GameManager.GetChannel(outputChannel).AddVoltageSource(this);
        channelInfoText.text = currentChannel.ToString();
    }

    private void IncrementChannel()
    {
        currentChannel++;
        if (currentChannel > 3)
        {
            currentChannel = 1;
        }
    }

    public void SetInput(float voltage, int id)
    {
        _voltage = voltage;
        GameManager.GetChannel(outputChannel).UpdateVoltage();
        _satelite.voltage = voltage;
    }

    public float GetOutput(int id)
    {
        return _voltage;
    }

    public void Interact()
    {
        var oldChannel = -currentChannel;
        IncrementChannel();
        GameManager.GetChannel(oldChannel).RemoveVoltageReceiver(this);
        GameManager.GetChannel(-currentChannel).AddVoltageReceiver(this);
        channelInfoText.text = currentChannel.ToString();
        GameManager.GetChannel(outputChannel).UpdateVoltage();
        radio_sound.Play();
    }
}
