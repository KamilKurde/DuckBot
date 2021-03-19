#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `VoltageData`. Inherits from `AtomDrawer&lt;VoltageDataEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(VoltageDataEvent))]
    public class VoltageDataEventDrawer : AtomDrawer<VoltageDataEvent> { }
}
#endif
