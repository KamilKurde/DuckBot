using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowController : PlaceableElement, IReceiver, ISource, IInteractable
{
    [SerializeField] private bool firstChannelIsActive = true;

    [SerializeField] private Animator animator;
    
    [Header("Inputs")]
    [SerializeField] private int inputChannel1;
    [SerializeField] private int inputChannel2;
    [Header("Outputs")]
    [SerializeField] private int outputChannel;

    private float lastInteract = 0f;

    private float _voltage = 0;

    
    // Start is called before the first frame update
    private void Start()
    {
        GameManager.GetChannel(inputChannel1).AddVoltageReceiver(this);
        GameManager.GetChannel(outputChannel).AddVoltageSource(this);
    }
    
    public void SetInput(float voltage)
    {
        _voltage = voltage;
        UpdateColor( 1, voltage);
    }

    public float GetOutput()
    {
        return _voltage;
    }

    public void Interact()
    {
        if (Time.time - lastInteract < 1f)
        {
            return;
        }

        lastInteract = Time.time;
        firstChannelIsActive = !firstChannelIsActive;
        animator.SetTrigger("StateChange");
        StartCoroutine(ChangeChannels());
    }

    private IEnumerator ChangeChannels()
    {
        yield return new WaitForSeconds(0.5f);
        if (firstChannelIsActive)
        {
            GameManager.GetChannel(inputChannel1).AddVoltageReceiver(this);
            GameManager.GetChannel(inputChannel2).RemoveVoltageReceiver(this);
        }
        else
        {
            GameManager.GetChannel(inputChannel2).AddVoltageReceiver(this);
            GameManager.GetChannel(inputChannel1).RemoveVoltageReceiver(this);
        }
        GameManager.GetChannel(outputChannel).UpdateVoltage();
    }

    protected override void UpdateChannels(List<int> inputChannels, List<int> outputChannels)
    {
        ChangeReceiverChannel(ref inputChannel1, inputChannels[0]);
        /*GameManager.GetChannel(inputChannel1).RemoveVoltageReceiver(this);
        inputChannel1 = inputChannels[0];
        GameManager.GetChannel(inputChannel1).AddVoltageReceiver(this);*/
        ChangeReceiverChannel(ref inputChannel2, inputChannels[1]);
        /*GameManager.GetChannel(inputChannel2).RemoveVoltageReceiver(this);
        inputChannel2 = inputChannels[1];
        GameManager.GetChannel(inputChannel2).AddVoltageReceiver(this);*/
        ChangeSourceChannel(ref outputChannel, outputChannels[0]);
        /*GameManager.GetChannel(outputChannel).RemoveVoltageSource(this);
        outputChannel = outputChannels[0];
        GameManager.GetChannel(outputChannel).AddVoltageSource(this); */
    }
}
