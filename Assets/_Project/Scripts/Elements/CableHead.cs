using UnityEngine;

public class CableHead : Element, ISource
{
    [SerializeField] private Renderer tileRenderer;
    [SerializeField] private int channel;
    [SerializeField] private float voltage = 0f;
    public float GetOutput()
    {
        return voltage;
    }

    private void Start()
    {
        LightInit();
        UpdateColor( new []{0, 1}, voltage);
        var tileMaterials = tileRenderer.materials;
        tileMaterials[0] = GetComponent<Renderer>().materials[1];
        tileRenderer.GetComponent<Renderer>().materials = tileMaterials;
        GameManager.GetChannel(channel).AddVoltageSource(this);
    }
}
