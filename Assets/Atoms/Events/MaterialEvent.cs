using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `Material`. Inherits from `AtomEvent&lt;Material&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Material", fileName = "MaterialEvent")]
    public sealed class MaterialEvent : AtomEvent<Material>
    {
    }
}
