using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class PlaceableElement : Element, IPlaceable
{
    private bool _isVisible = true;

    public void Hide()
    {
        if (!_isVisible) return;
        _isVisible = false;
        transform.DOScale(0f, 0.5f);
        UpdateChannels(new List<int> {0,0,0}, new List<int> {0,0,0});
    }

    public void Show()
    {
        if (_isVisible) return;
        _isVisible = true;
        transform.DOScale(1f, 0.5f);
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
