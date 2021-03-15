using UnityAtoms;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    // Get materials from inspector
    [SerializeField] internal MaterialValueList voltageMaterials;
    // Component that's null when object doesn't have Light Component
    internal Light _light = null;
    // Method which updates materials with given indexes and light component (if present)
    private void UpdateColor(int[] indexes, float voltage, MaterialValueList materialsList)
    {
        var materials = GetComponent<Renderer>().materials;
        Material material = null;

        if (Mathf.Approximately(voltage, 0f))
        {
            material = materialsList[0];
            if (_light != null)
                _light.enabled = false;
        }
        else if (voltage > 0 && voltage <= 2)
        {
            material = materialsList[1];
            if (_light != null)
            {
                _light.color = new Color(0f, 1f, 0f);
                _light.enabled = true;
            }
        }
        else if (voltage > 2 && voltage <= 4)
        {
            material = materialsList[2];
            if (_light != null)
            {
                _light.color = new Color(0f, 0.66f, 1f);
                _light.enabled = true;
            }
        }
        else if (voltage >= 4)
        {
            material = materialsList[3];
            if (_light != null)
            {
                _light.color = new Color(1f, 0f, 0f);
                _light.enabled = true;
            }
        }

        foreach (var index in indexes)
        {
            materials[index] = material;
        }
        GetComponent<Renderer>().materials = materials;
    }

    internal void UpdateColor(int index, float voltage, MaterialValueList materialsList)
    {
        UpdateColor(new []{index}, voltage, materialsList);
    }

    internal void UpdateColor(int[] indexes, float voltage)
    {
        UpdateColor(indexes, voltage, voltageMaterials);
    }

    internal void UpdateColor(int index, float voltage)
    {
        UpdateColor(new []{index}, voltage);
    }

    // Method to invoke when prefab has light component
    internal void LightInit()
    {
        _light = GetComponent<Light>();
    }

    internal void ChangeListenerToReceiver(int channel)
    {
        if (!(this is IReceiver)) return;
        GameManager.GetChannel(channel).RemoveVoltageListener(this as IListener);
        GameManager.GetChannel(channel).AddVoltageReceiver(this as IReceiver);
    }

    internal void ChangeReceiverToListner(int channel)
    {
        if (!(this is IReceiver)) return;
        GameManager.GetChannel(channel).RemoveVoltageReceiver(this as IReceiver);
        GameManager.GetChannel(channel).AddVoltageListener(this as IListener);
    }

    internal void ChangeLinstenerChannel(ref int from, int to)
    {
        if (!(this is IListener)) return;
        GameManager.GetChannel(from).RemoveVoltageListener(this as IListener);
        GameManager.GetChannel(to).AddVoltageListener(this as IListener);
        from = to;
    }

    internal void ChangeReceiverChannel(ref int from, int to)
    {
        if (!(this is IReceiver)) return;
        GameManager.GetChannel(from).RemoveVoltageReceiver(this as IReceiver);
        GameManager.GetChannel(to).AddVoltageReceiver(this as IReceiver);
        from = to;
    }

    internal void ChangeSourceChannel(ref int from, int to)
    {
        if (!(this is ISource)) return;
        GameManager.GetChannel(from).RemoveVoltageSource(this as ISource);
        GameManager.GetChannel(to).AddVoltageSource(this as ISource);
        from = to;
    }

    private void OnDestroy()
    {
        GameManager.RemoveAllReferencesTo(this);
    }
}
