using System.Collections.Generic;
using UnityEngine;

public abstract class PlaceableElement : Element, IPlaceable
{
    private bool _isVisible = true;

    public void Hide()
    {
        if (!_isVisible) return;
        _isVisible = false;
        transform.position += Vector3.up * -10;
        // Disconnects element from channels
        UpdateChannels(new List<int> {0,0,0}, new List<int> {0,0,0});
    }

    public void Show()
    {
        if (_isVisible) return;
        _isVisible = true;
        transform.position += Vector3.up * 10;
    }

    public bool IsVisible()
    {
        return _isVisible;
    }

    protected abstract void UpdateChannels(List<int> inputChannels, List<int> outputChannels);

    public void Place(Vector3 where, float rotation, List<int> inputChannels, List<int> outputChannels)
    {
        transform.position = new Vector3(where.x, transform.position.y, where.z);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotation, transform.rotation.eulerAngles.z);
        UpdateChannels(inputChannels, outputChannels);
        Show();
    }
}
