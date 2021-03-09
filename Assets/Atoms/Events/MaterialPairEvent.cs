using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `MaterialPair`. Inherits from `AtomEvent&lt;MaterialPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/MaterialPair", fileName = "MaterialPairEvent")]
    public sealed class MaterialPairEvent : AtomEvent<MaterialPair>
    {
    }
}
