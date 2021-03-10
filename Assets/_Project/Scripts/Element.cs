using UnityAtoms;
using UnityEngine;

public class Element : MonoBehaviour
{
    // Get materials from inspector
    [SerializeField] internal MaterialValueList voltageMaterials;
    // Component that's null when object doesn't have Light Component
    internal Light _light = null;
    // Method which updates materials with given indexes and light component (if present)
    internal void UpdateColor(int[] indexes, float voltage)
    {
        var materials = GetComponent<Renderer>().materials;
        Material material = null;

        if (voltage == 0)
        {
            material = voltageMaterials[0];
            if (_light != null)
                _light.enabled = false;
        }
        if (voltage > 0)
        {
            material = voltageMaterials[1];
            if (_light != null)
            {
                _light.color = new Color(0f, 0.66f, 1f);
                _light.enabled = true;
            }
        }

        if (voltage > 4)
        {
            material = voltageMaterials[2];
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

    internal void UpdateColor(int index, float voltage)
    {
        UpdateColor(new []{index}, voltage);
    }

    // Method to invoke when prefab has light component
    internal void LightInit()
    {
        _light = GetComponent<Light>();
    }

    private void OnDestroy()
    {
        GameManager.RemoveAllReferencesTo(this);
    }
}
