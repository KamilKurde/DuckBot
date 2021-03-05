using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Source
{ 
    public float GetOutput();
}

public interface Receiver
{
    public void SetInput(float voltage);
}
public class Channel
{
    private float _voltage = 0f;

    public float GetVoltage()
    {
        return _voltage / 10f;
    }

    private List<Source> _sources = new List<Source>();
    private List<Receiver> _receivers = new List<Receiver>();

    public void UpdateVoltage()
    {
        _voltage = 0f;
        foreach (var t in _sources)
        {
            _voltage += (int) t.GetOutput();
        }

        foreach (var t in _receivers)
        {
            t.SetInput(_voltage);
        }
        Debug.Log("Voltage updated: " + _voltage);
    }

    public void AddVoltageSource(Source source)
    {
        _sources.Add(source);
        UpdateVoltage();
    }

    public void RemoveVoltageSource(Source source)
    {
        _sources.Remove(source);
        UpdateVoltage();
    }
        
    public void AddVoltageListeners(Receiver receiver)
    {
        _receivers.Add(receiver);
        UpdateVoltage();
    }

    public void RemoveVoltageListeners(Receiver receiver)
    {
        _receivers.Remove(receiver);
        UpdateVoltage();
    }
}