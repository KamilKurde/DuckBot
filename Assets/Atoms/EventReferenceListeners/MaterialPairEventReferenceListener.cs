using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `MaterialPair`. Inherits from `AtomEventReferenceListener&lt;MaterialPair, MaterialPairEvent, MaterialPairEventReference, MaterialPairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/MaterialPair Event Reference Listener")]
    public sealed class MaterialPairEventReferenceListener : AtomEventReferenceListener<
        MaterialPair,
        MaterialPairEvent,
        MaterialPairEventReference,
        MaterialPairUnityEvent>
    { }
}
