using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Button : MonoBehaviour, Source, Receiver
{
    [SerializeField] private DataHolder _dataHolder;
    
    [SerializeField] private int inputChannel;
    [SerializeField] private int outputChannel;
    
    private float _voltage = 0f;

    public float GetOutput() { return _voltage; }

    public void SetInput(float voltage) { _voltage = voltage; }

    private void OnTriggerEnter(Collider other)
    {
        _dataHolder.channels[inputChannel].AddVoltageListeners(this);
        _dataHolder.channels[outputChannel].AddVoltageSource(this);
    }

    private void OnTriggerExit(Collider other)
    {
        _dataHolder.channels[outputChannel].RemoveVoltageSource(this);
        _dataHolder.channels[inputChannel].RemoveVoltageListeners(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _dataHolder.reInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
