using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Instancer of type `MaterialPair`. Inherits from `AtomEventInstancer&lt;MaterialPair, MaterialPairEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/MaterialPair Event Instancer")]
    public class MaterialPairEventInstancer : AtomEventInstancer<MaterialPair, MaterialPairEvent> { }
}
