using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Value List of type `VoltageData`. Inherits from `AtomValueList&lt;VoltageData, VoltageDataEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/VoltageData", fileName = "VoltageDataValueList")]
    public sealed class VoltageDataValueList : AtomValueList<VoltageData, VoltageDataEvent> { }
}
