using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `Material`. Inherits from `AtomEventReferenceListener&lt;Material, MaterialEvent, MaterialEventReference, MaterialUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Material Event Reference Listener")]
    public sealed class MaterialEventReferenceListener : AtomEventReferenceListener<
        Material,
        MaterialEvent,
        MaterialEventReference,
        MaterialUnityEvent>
    { }
}
