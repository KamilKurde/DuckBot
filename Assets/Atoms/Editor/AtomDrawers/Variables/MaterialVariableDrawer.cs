#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `Material`. Inherits from `AtomDrawer&lt;MaterialVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(MaterialVariable))]
    public class MaterialVariableDrawer : VariableDrawer<MaterialVariable> { }
}
#endif
