using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioGroup
{
    Master,
    Sound,
    Music
}

public static class GameManager
{
    // Dictionary that holds channels
    private static Dictionary<int, Channel> _channels = new Dictionary<int, Channel>();
    private static List<CableEndpoint> _endpoints = new List<CableEndpoint>();
    public static IPlaceable placeable;
    public static UImanager uImanager;
    public static Player player;
    public static AudioMixer audioMixer;

    private static float LinearToDecibel(float linear)
    {
        var value = linear / 100;
        if (Mathf.Approximately(value, 0f))
        {
            return -80f;
        }
        return Mathf.Log10(value) * 20;
    }

    public static void SetVolume(AudioGroup audioGroup, float volume)
    {
        var groupName = "";
        switch (audioGroup)
        {
            case AudioGroup.Master:
                groupName = "Master";
                break;
            case AudioGroup.Sound:
                groupName = "Sound";
                break;
            case AudioGroup.Music:
                groupName = "Music";
                break;
        }
        audioMixer.SetFloat(groupName, LinearToDecibel(volume));
    }

    public static Channel GetChannel(int channelNumber)
    {
        // If channel doesn't exist, create one
        if (!_channels.ContainsKey(channelNumber))
        {
            _channels.Add(channelNumber, new Channel());
        }
        return _channels[channelNumber];
    }

    public static void Clear()
    {
        _channels.Clear();
        _endpoints.Clear();
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
        foreach (var endpoint in _endpoints)
        {
            if (!endpoint.requirementFullfiled)
            {
                allRequirementsFullfiled = false;
            }
        }

        if (!allRequirementsFullfiled) return;
        player.levelFinished = true;
        if (uImanager == null)
        {
            return;
        }
        uImanager.EndLevel();
    }

    // Removing all references to Object (eg. when object is picked up)
    public static void RemoveAllReferencesTo(Element element)
    {
        if (_channels.Count == 0)
        {
            return;
        }
        var isEndpoint = element is CableEndpoint;
        if (isEndpoint)
        {
            _endpoints.Remove(element as CableEndpoint);
        }
        var unusedChannelsKeys = new List<int>();
        var channelsKeys = _channels.Keys;
        foreach (var key in channelsKeys)
        {
            lock (_channels[key])
            {
                _channels[key].RemoveReferencesTo(element);
                if (_channels[key].IsEmpty())
                    unusedChannelsKeys.Add(key);
            }
        }

        foreach (var key in unusedChannelsKeys)
        {
            _channels.Remove(key);
        }
    }
}
