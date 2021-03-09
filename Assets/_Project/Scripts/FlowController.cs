using UnityEngine;

public class FlowController : Element, IReceiver
{
    [Header("Inputs")]
    [SerializeField] private int inputChannel1;
    [SerializeField] private int inputChannel2;
    [Header("Outputs")]
    [SerializeField] private int outputChannel;

    [SerializeField] private byte currentChannel = 1;

    public void SetInput(float voltage)
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
