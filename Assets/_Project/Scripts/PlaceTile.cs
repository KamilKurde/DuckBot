using System.Collections.Generic;
using UnityEngine;

public class PlaceTile : Element
{
    [Header("Inputs")]
    public List<int> _inputs = new List<int>();
    [Header("Outputs")] 
    public List<int> _outputs = new List<int>();

    [SerializeField] private float rotation;
    [SerializeField] private GameObject _gameObject = null;
    public IPlaceable placeable = null;

    public bool HasPlaceable => placeable != null;
    
    public void SetPlaceable(IPlaceable placeable)
    {
        this.placeable = placeable;
        this.placeable.Place(transform.position,rotation, _inputs, _outputs);
    }

    private void Start()
    {
        if (_gameObject == null) return;
        placeable = _gameObject.GetComponent<IPlaceable>();
        _gameObject = null;
    }
}
