using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Value List of type `Material`. Inherits from `AtomValueList&lt;Material, MaterialEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Material", fileName = "MaterialValueList")]
    public sealed class MaterialValueList : AtomValueList<Material, MaterialEvent> { }
}
