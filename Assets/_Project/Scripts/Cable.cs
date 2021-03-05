using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cable : MonoBehaviour, Receiver
{
    [SerializeField] private DataHolder _dataHolder;
    
    [SerializeField] private int channel;
    [SerializeField] private Material Blue;
    [SerializeField] private Material Black;

    [SerializeField] private float _voltage = 0f;
    // Start is called before the first frame update
    public void SetInput(float voltage)
    {
        _voltage = voltage;
        var mats = GetComponent<Renderer>().materials;
        switch (_voltage)
        {
            case 0f:
                mats[0] = Black;
                break;
            case 3.3f:
            case 3f:
                mats[0] = Blue;
                break;
        }

        GetComponent<Renderer>().materials = mats;
    }

    void Start()
    {
        _dataHolder.reInitialize();
        Debug.Log(_dataHolder.channels[channel] != null);
        _dataHolder.channels[channel].AddVoltageListeners(this);
        _dataHolder.channels[channel].UpdateVoltage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
