#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Constant property drawer of type `Material`. Inherits from `AtomDrawer&lt;MaterialConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(MaterialConstant))]
    public class MaterialConstantDrawer : VariableDrawer<MaterialConstant> { }
}
#endif
