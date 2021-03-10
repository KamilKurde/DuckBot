using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public static class GameManager
{
    // Dictionary that holds channels
    private static Dictionary<int, Channel> _channels = new Dictionary<int, Channel>();
    private static List<CableEndpoint> _endpoints = new List<CableEndpoint>();
    public static IPlacable placable;

    public static Channel GetChannel(int channelNumber)
    {
        // If channel doesn't exist, create one
        if (!_channels.ContainsKey(channelNumber))
            _channels.Add(channelNumber, new Channel());
        return _channels[channelNumber];
    }

    public static void AddEndpoint(CableEndpoint endPoint)
    {
        _endpoints.Add(endPoint);
    }

    public static void CheckRequirements()
    {
        if (_endpoints.Count == 0)
        {
            return;
        }
        var allRequirementsFullfiled = true;
        foreach (var _ in _endpoints.Where(endpoint => !endpoint.requirementFullfiled))
        {
            allRequirementsFullfiled = false;
        }

        if (allRequirementsFullfiled)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    // Removing all references to Object (eg. when object is picked up)
    public static void RemoveAllReferencesTo(Element element)
    {
        if (_channels.Count == 0)
            return;
        var isSource = element is ISource;
        var isListener = element is IListener;
        var isReceiver = element is IReceiver;
        var isEndpoint = element is CableEndpoint;
        if (isEndpoint)
        {
            _endpoints.Remove(element as CableEndpoint);
        }
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
                if (_channels[key].IsEmpty())
                    unusedChannelskeys.Add(key);
            }
        }

        foreach (var key in unusedChannelskeys)
        {
            _channels.Remove(key);
        }
    }
}
