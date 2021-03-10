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

    protected abstract void UpdateChannels(List<int> inputChannels, List<int> outputChannels);

    public void Place(Vector3 where, float rotation, List<int> inputChannels, List<int> outputChannels)
    {
        transform.position = new Vector3(where.x, transform.position.y, where.z);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotation, transform.rotation.eulerAngles.z);
        UpdateChannels(inputChannels, outputChannels);
        Show();
    }
}
