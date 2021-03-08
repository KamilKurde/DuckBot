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
        return _voltage;
    }

    private List<Source> sources = new List<Source>();
    private List<Receiver> receivers = new List<Receiver>();

    public void UpdateVoltage()
    {
        _voltage = 0f;
        foreach (var t in sources)
        {
            _voltage += t.GetOutput();
        }

        foreach (var t in receivers)
        {
            t.SetInput(_voltage);
        }
    }

    public bool IsEmpty()
    {
        return sources.Count == 0 && receivers.Count == 0;
    }

    public void AddVoltageSource(Source source)
    {
        sources.Add(source);
        UpdateVoltage();
    }

    public void RemoveVoltageSource(Source source)
    {
        sources.Remove(source);
        UpdateVoltage();
    }
        
    public void AddVoltageListeners(Receiver receiver)
    {
        receivers.Add(receiver);
        UpdateVoltage();
    }

    public void RemoveVoltageListeners(Receiver receiver)
    {
        receivers.Remove(receiver);
        UpdateVoltage();
    }
}