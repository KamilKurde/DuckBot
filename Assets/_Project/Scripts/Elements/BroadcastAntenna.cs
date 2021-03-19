using TMPro;
using UnityEngine;

public class BroadcastAntenna : Element, IReceiver, ISource, IInteractable
{
    [SerializeField] private TextMeshPro channelInfoText;
    [Header("Inputs")]
    [SerializeField] private int inputChannel = 0;

    [SerializeField, Range(1, 3)] private int currentChannel;

    private AudioSource radio_sound;

    private float _voltage = 0f;

    private void Start()
    {
        radio_sound = GetComponent<AudioSource>();
        GameManager.GetChannel(inputChannel).AddVoltageReceiver(this);
        GameManager.GetChannel(-currentChannel).AddVoltageSource(this);
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
        UpdateColor(3, voltage);
        GameManager.GetChannel(-currentChannel).UpdateVoltage();
    }

    public float GetOutput(int id)
    {
        return _voltage;
    }

    public void Interact()
    {
        GameManager.GetChannel(-currentChannel).RemoveVoltageSource(this);
        IncrementChannel();
        channelInfoText.text = currentChannel.ToString();
        GameManager.GetChannel(-currentChannel).AddVoltageSource(this);
        radio_sound.Play();
    }
}
