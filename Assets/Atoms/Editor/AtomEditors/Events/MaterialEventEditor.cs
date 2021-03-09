#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using UnityEngine;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Material`. Inherits from `AtomEventEditor&lt;Material, MaterialEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(MaterialEvent))]
    public sealed class MaterialEventEditor : AtomEventEditor<Material, MaterialEvent> { }
}
#endif
