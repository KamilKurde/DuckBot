using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ChannelManager
{
    private static Dictionary<int, Channel> _channels = new Dictionary<int, Channel>();

    public static Channel GetChannel(int channelNumber)
    {
        if (!_channels.ContainsKey(channelNumber))
            _channels.Add(channelNumber, new Channel());
        return _channels[channelNumber];
    }

    public static void RemoveAllReferencesTo(Element element)
    {
        if (_channels.Count == 0)
        {
            return;
        }
        var isSource = element is Source;
        var isReceiver = element is Receiver;
        foreach (var key in _channels.Keys)
        {
            lock (_channels[key])
            {
                if (isSource)
                    _channels[key].RemoveVoltageSource(element as Source);
                if (isReceiver)
                    _channels[key].RemoveVoltageListeners(element as Receiver);
                if (_channels[key].IsEmpty())
                    _channels.Remove(key);
            }
        }
    }
}
