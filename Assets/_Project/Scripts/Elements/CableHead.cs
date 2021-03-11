using UnityAtoms;
using UnityEngine;

public class CableHead : Element, ISource
{
    [SerializeField] private Renderer tileRenderer;
    [SerializeField] private MaterialValueList headMaterials;
    [SerializeField] private int channel;
    [SerializeField] private float voltage = 0f;
    public float GetOutput()
    {
        return voltage;
    }

    private void Start()
    {
        LightInit();
        UpdateColor(0, voltage);
        UpdateColor(1, voltage, headMaterials);
        var tileMaterials = tileRenderer.materials;
        tileMaterials[0] = GetComponent<Renderer>().materials[1];
        tileRenderer.GetComponent<Renderer>().materials = tileMaterials;
        GameManager.GetChannel(channel).AddVoltageSource(this);
    }
}
