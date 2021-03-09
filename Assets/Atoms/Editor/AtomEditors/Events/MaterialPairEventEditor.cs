#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using UnityEngine;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `MaterialPair`. Inherits from `AtomEventEditor&lt;MaterialPair, MaterialPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(MaterialPairEvent))]
    public sealed class MaterialPairEventEditor : AtomEventEditor<MaterialPair, MaterialPairEvent> { }
}
#endif
