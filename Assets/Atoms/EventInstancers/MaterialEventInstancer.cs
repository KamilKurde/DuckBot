using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Instancer of type `Material`. Inherits from `AtomEventInstancer&lt;Material, MaterialEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/Material Event Instancer")]
    public class MaterialEventInstancer : AtomEventInstancer<Material, MaterialEvent> { }
}
