using System.Collections.Generic;
using UnityEngine;

public static class ChannelManager
{
    // Dictionary that holds channels
    private static Dictionary<int, Channel> _channels = new Dictionary<int, Channel>();

    public static Channel GetChannel(int channelNumber)
    {
        // If channel doesn't exist, create one
        if (!_channels.ContainsKey(channelNumber))
            _channels.Add(channelNumber, new Channel());
        return _channels[channelNumber];
    }

    // Removing all references to Object (eg. when object is picked up)
    public static void RemoveAllReferencesTo(Element element)
    {
        if (_channels.Count == 0)
            return;
        var isSource = element is ISource;
        var isListener = element is IListener;
        var isReceiver = element is IReceiver;
        var unusedChannelskeys = new List<int>();
        foreach (var key in _channels.Keys)
        {
            lock (_channels[key])
            {
                if (isSource)
                    _channels[key].RemoveVoltageSource(element as ISource);
                if (isListener)
                    _channels[key].RemoveVoltageListener(element as IListener);
                if (isReceiver)
                    _channels[key].RemoveVoltageReceiver(element as IReceiver);
                if(_channels[key].IsEmpty())
                    unusedChannelskeys.Add(key);
            }
        }

        foreach (var key in unusedChannelskeys)
        {
            _channels.Remove(key);
        }
    }
}
