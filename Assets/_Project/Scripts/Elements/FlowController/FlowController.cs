using System.Collections.Generic;
using UnityEngine;

public class FlowController : PlaceableElement, IReceiver, ISource, IInteractable
{
    [SerializeField] private bool firstChannelIsActive = true;

    [SerializeField] private Animator animator;
    
    [Header("Inputs")]
    [SerializeField] private int inputChannel1 = -10;
    [SerializeField] private int inputChannel2 = -11;
    [Header("Outputs")]
    [SerializeField] private int outputChannel = -14;

    private AudioSource flow_sound;

    private float _lastInteract = 0f;

    private float _voltage = 0;

    
    // Start is called before the first frame update
    private void Start()
    {
        flow_sound = GetComponent<AudioSource>();
        GameManager.GetChannel(inputChannel1).AddVoltageReceiver(this);
        GameManager.GetChannel(outputChannel).AddVoltageSource(this);
    }
    
    public void SetInput(float voltage, int id)
    {
        _voltage = voltage;
        UpdateColor( 1, voltage);
        GameManager.GetChannel(outputChannel).UpdateVoltage();
    }

    public float GetOutput(int id)
    {
        return _voltage;
    }

    public void Interact()
    {
        if (Time.time - _lastInteract < 0.7f)
        {
            return;
        }
        _lastInteract = Time.time;
        firstChannelIsActive = !firstChannelIsActive;
        animator.SetTrigger("StateChange");
        flow_sound.Play();
    }
    public void ChangeChannels()
    {
        GameManager.RemoveAllReferencesTo(this);
        if (firstChannelIsActive)
        {
            GameManager.GetChannel(inputChannel1).AddVoltageReceiver(this);
        }
        else
        {
            GameManager.GetChannel(inputChannel2).AddVoltageReceiver(this);
        }
        GameManager.GetChannel(outputChannel).AddVoltageSource(this);
    }

    protected override void UpdateChannels(List<int> inputChannels, List<int> outputChannels)
    {
        ChangeReceiverChannel(ref inputChannel1, inputChannels[0]);
        ChangeReceiverChannel(ref inputChannel2, inputChannels[1]);
        ChangeSourceChannel(ref outputChannel, outputChannels[0]);
    }
}
