using System.Collections.Generic;

public interface ISource
{ 
    public float GetOutput();
}

public interface IReceiver
{
    public void SetInput(float voltage);
}
public class Channel
{
    private float _voltage = 0f;

    private List<ISource> sources = new List<ISource>();
    private List<IReceiver> receivers = new List<IReceiver>();

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

    public void AddVoltageSource(ISource source)
    {
        sources.Add(source);
        UpdateVoltage();
    }

    public void RemoveVoltageSource(ISource source)
    {
        sources.Remove(source);
        UpdateVoltage();
    }
        
    public void AddVoltageListeners(IReceiver receiver)
    {
        receivers.Add(receiver);
        UpdateVoltage();
    }

    public void RemoveVoltageListeners(IReceiver receiver)
    {
        receivers.Remove(receiver);
        UpdateVoltage();
    }
}