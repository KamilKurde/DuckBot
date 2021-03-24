using UnityEngine;

public class CableHead : Element, ISource
{
    [SerializeField] private int channel;
    [SerializeField] private float voltage = 0f;
    [Header("Tile")]
    [Tooltip("Changes this renderer's texture on given index with texture of this object from given index")]
    [SerializeField] private Renderer tileRenderer;
    [Tooltip("Index of material to change")]
    [SerializeField] private int tileRendererMaterialIndex;
    [Tooltip("Index of material in this object's renderer to take texture from")]
    [SerializeField] private int sourceMaterialIndex;
    
    public float GetOutput(int id)
    {
        return voltage;
    }

    private void Start()
    {
        LightInit();
        UpdateColor( new []{0, 1, sourceMaterialIndex}, voltage);
        var tileMaterials = tileRenderer.materials;
        tileMaterials[tileRendererMaterialIndex] = GetComponent<Renderer>().materials[sourceMaterialIndex];
        tileRenderer.GetComponent<Renderer>().materials = tileMaterials;
        GameManager.GetChannel(channel).AddVoltageSource(this);
    }
}
