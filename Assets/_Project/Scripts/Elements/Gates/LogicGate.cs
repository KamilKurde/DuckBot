using UnityEngine;

enum SimpleLogicModes
{
    Or,
    And
}

public class LogicGate : Element, IInteractable, IReceiver, ISource
{
    [SerializeField] private int inputChannel1;
    [SerializeField] private int inputChannel2;
    [SerializeField] private int outputChannel;

    [SerializeField] private Material noPowerMaterial;
    [SerializeField] private Material orMaterial;
    [SerializeField] private Material andMaterial;
    
    [SerializeField] private SimpleLogicModes mode = SimpleLogicModes.And;
    private AudioSource _switchSound;
    
    private float _inputVoltage1 = 0f;
    private float _inputVoltage2 = 0f;
    private float _outputVoltage = 0f;

    private void Start()
    {
        _switchSound = GetComponent<AudioSource>();
        UpdateLogic();
        GameManager.GetChannel(inputChannel1).AddVoltageReceiver(this);
        GameManager.GetChannel(inputChannel2).AddVoltageReceiver(this);
        GameManager.GetChannel(outputChannel).AddVoltageSource(this);
    }

    private void UpdateLogic()
    {
        var materials = GetComponent<Renderer>().materials;
        var voltage1 = _inputVoltage1 > 0f;
        var voltage2 = _inputVoltage2 > 0f;
        var logicFullfiled = false;
        if (mode == SimpleLogicModes.And)
        {
            materials[1] = andMaterial;
            if (voltage1 && voltage2)
            {
                logicFullfiled = true;
            }
        }
        else if (mode == SimpleLogicModes.Or)
        {
            materials[1] = orMaterial;
            if (voltage1 || voltage2)
            {
                logicFullfiled = true;
            }
        }
        _outputVoltage = logicFullfiled ? _inputVoltage1 + _inputVoltage2 : 0f;

        if (_inputVoltage1 == 0f && _inputVoltage2 == 0f)
        {
            materials[1] = noPowerMaterial;
        }
        GetComponent<Renderer>().materials = materials;
        UpdateColor(2, _outputVoltage);
        GameManager.GetChannel(outputChannel).UpdateVoltage();
    }

    public void Interact()
    {
        _switchSound.Play();
        if (mode == SimpleLogicModes.And)
        {
            mode = SimpleLogicModes.Or;
        }
        else if (mode == SimpleLogicModes.Or)
        {
            mode = SimpleLogicModes.And;
        }
        UpdateLogic();
    }

    public void SetInput(float voltage, int channelId)
    {
        if (inputChannel1 == channelId)
        {
            _inputVoltage1 = voltage;
        }
        else if (inputChannel2 == channelId)
        {
            _inputVoltage2 = voltage;
        }
        else
        {
            Debug.Log("Podany kanał to " + channelId + " a dostępne kanały to " + inputChannel1 + " oraz " + inputChannel2);
        }

        UpdateLogic();
    }

    public float GetOutput(int channelId)
    {
        return _outputVoltage;
    }
}
