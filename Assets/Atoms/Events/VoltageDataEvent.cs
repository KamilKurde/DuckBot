using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `VoltageData`. Inherits from `AtomEvent&lt;VoltageData&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/VoltageData", fileName = "VoltageDataEvent")]
    public sealed class VoltageDataEvent : AtomEvent<VoltageData>
    {
    }
}
