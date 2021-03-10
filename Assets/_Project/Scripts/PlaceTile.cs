using System;
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
    public IPlacable placable = null;

    public void SetPlacable(IPlacable placable)
    {
        this.placable = placable;
        this.placable.Place(transform.position,rotation, _inputs, _outputs);
    }

    public bool HasPlacable()
    {
        return placable != null;
    }

    private void Start()
    {
        if (_gameObject != null)
        {
            placable = _gameObject.GetComponent<IPlacable>();
        }
    }
}
