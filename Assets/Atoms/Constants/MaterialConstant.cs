using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Constant of type `Material`. Inherits from `AtomBaseVariable&lt;Material&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-teal")]
    [CreateAssetMenu(menuName = "Unity Atoms/Constants/Material", fileName = "MaterialConstant")]
    public sealed class MaterialConstant : AtomBaseVariable<Material> { }
}
