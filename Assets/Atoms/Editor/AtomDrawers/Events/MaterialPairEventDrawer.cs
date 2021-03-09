#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `MaterialPair`. Inherits from `AtomDrawer&lt;MaterialPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(MaterialPairEvent))]
    public class MaterialPairEventDrawer : AtomDrawer<MaterialPairEvent> { }
}
#endif
