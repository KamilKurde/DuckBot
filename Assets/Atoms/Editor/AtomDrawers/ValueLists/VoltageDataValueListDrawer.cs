#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `VoltageData`. Inherits from `AtomDrawer&lt;VoltageDataValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(VoltageDataValueList))]
    public class VoltageDataValueListDrawer : AtomDrawer<VoltageDataValueList> { }
}
#endif
