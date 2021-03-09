#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `Material`. Inherits from `AtomDrawer&lt;MaterialValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(MaterialValueList))]
    public class MaterialValueListDrawer : AtomDrawer<MaterialValueList> { }
}
#endif
