#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Material`. Inherits from `AtomDrawer&lt;MaterialEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(MaterialEvent))]
    public class MaterialEventDrawer : AtomDrawer<MaterialEvent> { }
}
#endif
