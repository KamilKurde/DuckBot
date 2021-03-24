using DG.Tweening;
using UnityAtoms;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    internal float shortAnimTime = GameManager.shortAnimationLenght;
    internal float mediumAnimTime = GameManager.mediumAnimationLenght;

    [SerializeField] private VoltageDataValueList voltageList;

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
        // Default color for voltage near 0
        var lastMaximalColor = Color.gray;

        for (var i = 0; i < voltageList.Count; i++)
        {
            // If voltage isn't lower than voltage from current element in array
            if (voltage >= voltageList[i].value)
            {
                lastMaximalVoltage = voltageList[i].value;
                lastMaximalColor = voltageList[i].color;
                continue;
            }

            // DON'T TOUCH IT UNDER ANY CIRCUMSTANCES
            var voltageFromStartOfVoltageLevel = voltage - lastMaximalVoltage;                  // For  4V it will return 0,7 ( 3,3 + 0,7 = 4 )
            var maxVoltageFromStartOfVoltageLevel = voltageList[i].value - lastMaximalVoltage;        // For 4V it will return 1,7 ( 5 - 3,3 = 1,7 )
            // It returns numbers in 0f..1f where 0f means take color from previous voltage level, 1f means take color from current voltage level, and 0,5f means take the color in the middle of one and the other
            var divider = voltageFromStartOfVoltageLevel / maxVoltageFromStartOfVoltageLevel;   // For 4V it will return 0,41 ( 0,7 / 1,7 = 0,41 )
            // From color of previous voltage level, to color of current voltage level
            color = Color.Lerp(
                lastMaximalColor,
                voltageList[i].color,
                divider
            );
            break;
            // IT JUST WORKS ~Todd Howard
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
            // Check for time since scene's start
            if (Time.time - GameManager.sceneStartTime > 1f)
            {
                // If scene started a while ago change color with animation
                materials[index].DOColor(color, shortAnimTime);
                materials[index].DOColor(color, "_EmissionColor", shortAnimTime);
            }
            else
            {
                // If scene just started then change color without animation
                materials[index].color = color;
                materials[index].SetColor("_EmissionColor", color);
            }
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