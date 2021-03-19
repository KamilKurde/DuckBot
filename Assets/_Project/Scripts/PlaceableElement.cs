using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class PlaceableElement : Element, IPlaceable
{
    [SerializeField] private Animator sizeAnimator;
    private bool _isVisible = true;

    private void ChangeState()
    {
        sizeAnimator.SetTrigger("ChangeState");
    }

    public void Hide()
    {
        if (!_isVisible) return;
        _isVisible = false;
        ChangeState();
        UpdateChannels(new List<int> {0,0,0}, new List<int> {0,0,0});
    }

    public void Show()
    {
        if (_isVisible) return;
        _isVisible = true;
        ChangeState();
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
