using System;
using UnityEngine.Events;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// None generic Unity Event of type `Material`. Inherits from `UnityEvent&lt;Material&gt;`.
    /// </summary>
    [Serializable]
    public sealed class MaterialUnityEvent : UnityEvent<Material> { }
}
