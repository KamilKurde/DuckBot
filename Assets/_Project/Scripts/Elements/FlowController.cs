using System.Collections.Generic;
using UnityEngine;

public class FlowController : Element, IReceiver, ISource, IInteractable, IPlaceable
{
    [SerializeField] private bool firstChannelIsActive = true;

    [SerializeField] private Animator animator;
    
    [Header("Inputs")]
    [SerializeField] private int inputChannel1;
    [SerializeField] private int inputChannel2;
    [Header("Outputs")]
    [SerializeField] private int outputChannel;

    private bool isVisible;

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
        firstChannelIsActive = !firstChannelIsActive;
        animator.SetTrigger("StateChange");
        if (firstChannelIsActive)
        {
            //animator.Play("State1");
            GameManager.GetChannel(inputChannel1).AddVoltageReceiver(this);
            GameManager.GetChannel(inputChannel2).RemoveVoltageReceiver(this);
        }
        else
        {
            //animator.Play("State2");
            GameManager.GetChannel(inputChannel2).AddVoltageReceiver(this);
            GameManager.GetChannel(inputChannel1).RemoveVoltageReceiver(this);
        }
        GameManager.GetChannel(outputChannel).UpdateVoltage();
    }

    

    public void Hide()
    {
        if (isVisible)
        {
            isVisible = false;
            transform.position -= Vector3.up * 10;
        }
    }

    public void Show() {
        if (!isVisible)
        {
            isVisible = true;
            transform.position += Vector3.up * 10;
        }
    }

    public bool IsVisible()
    {
        return isVisible;
    }

    public void Place(Vector3 where, float rotation, List<int> inputChannels, List<int> outputChannels)
    {
        transform.position = new Vector3(where.x, transform.rotation.y, where.z);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rotation);
        
        GameManager.GetChannel(inputChannel1).RemoveVoltageReceiver(this);
        inputChannel1 = inputChannels[0];
        GameManager.GetChannel(inputChannel1).AddVoltageReceiver(this);
        
        GameManager.GetChannel(inputChannel2).RemoveVoltageReceiver(this);
        inputChannel2 = inputChannels[1];
        GameManager.GetChannel(inputChannel2).AddVoltageReceiver(this);
        
        GameManager.GetChannel(outputChannel).RemoveVoltageSource(this);
        outputChannel = outputChannels[0];
        GameManager.GetChannel(outputChannel).AddVoltageSource(this);
    }
}
