using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    private void OnDestroy()
    {
        ChannelManager.RemoveAllReferencesTo(this);
    }
}
