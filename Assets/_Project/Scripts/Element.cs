using UnityAtoms.BaseAtoms;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    [SerializeField] private FloatValueList voltageList;

    [SerializeField] private ColorValueList colorList;

    // Component that's null when object doesn't have Light Component
    internal Light _light = null;

    // Method which updates materials with given indexes and light component (if present)
    internal void UpdateColor(int[] indexes, float voltage)
    {
        if (this == null)
        {
            return;
        }

        var materials = GetComponent<Renderer>().materials;
        var color = Color.gray;

        var lastMaximalVoltage = 0f;
        var lastMaximalColor = Color.gray;

        for (var i = 0; i < voltageList.Count; i++)
        {
            if (voltage >= lastMaximalVoltage && voltage < voltageList[i])
            {
                
                // IT JUST WORKS
                var startingColor = lastMaximalColor;
                var targetColor = colorList[i];
                var voltageFromStart = voltage - lastMaximalVoltage;
                var maxVoltageFromStartInRange = voltageList[i] - lastMaximalVoltage;
                var divider = voltageFromStart / maxVoltageFromStartInRange;
                color = Color.Lerp(
                    startingColor,
                    targetColor,
                    divider
                );
                break;
                // ~Todd Howard
            }

            lastMaximalVoltage = voltageList[i];
            lastMaximalColor = colorList[i];
        }

        if (_light != null && color == Color.gray)
        {
            _light.enabled = false;
        }
        else if (_light != null)
        {
            _light.color = color;
            _light.enabled = true;
        }

        foreach (var index in indexes)
        {
            materials[index].color = color;
            materials[index].SetColor("_EmissionColor", color);
        }

        GetComponent<Renderer>().materials = materials;
    }

    internal void UpdateColor(int index, float voltage)
    {
        UpdateColor(new[] {index}, voltage);
    }

    // Method to invoke when prefab has light component
    internal void LightInit() { _light = GetComponent<Light>(); }

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

    private void OnDisable() { GameManager.RemoveAllReferencesTo(this); }
}