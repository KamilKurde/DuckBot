#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using UnityEngine;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `VoltageData`. Inherits from `AtomEventEditor&lt;VoltageData, VoltageDataEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(VoltageDataEvent))]
    public sealed class VoltageDataEventEditor : AtomEventEditor<VoltageData, VoltageDataEvent> { }
}
#endif
