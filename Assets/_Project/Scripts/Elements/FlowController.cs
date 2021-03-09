using System;
using UnityEngine;

public class FlowController : Element, IReceiver, ISource, IInteractable
{
    [SerializeField] private bool firstChannelIsActive = false;

    [SerializeField] private Animator animator;
    
    [Header("Inputs")]
    [SerializeField] private int inputChannel1;
    [SerializeField] private int inputChannel2;
    [Header("Outputs")]
    [SerializeField] private int outputChannel;

    private float _voltage = 0;

    public void SetInput(float voltage)
    {
        _voltage = voltage;
    }

    public float GetOutput()
    {
        return _voltage;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Interact();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        firstChannelIsActive = !firstChannelIsActive;
        if (firstChannelIsActive)
        {
            animator.Play("State1");
            ChannelManager.GetChannel(inputChannel2).RemoveVoltageReceiver(this);
            ChannelManager.GetChannel(inputChannel1).AddVoltageReceiver(this);
        }
        else
        {
            animator.Play("State2");
            ChannelManager.GetChannel(inputChannel1).RemoveVoltageReceiver(this);
            ChannelManager.GetChannel(inputChannel2).AddVoltageReceiver(this);
        }
    }
}
