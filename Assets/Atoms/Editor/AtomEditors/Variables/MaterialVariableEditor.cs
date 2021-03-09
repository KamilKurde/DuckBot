using UnityEditor;
using UnityAtoms.Editor;
using UnityEngine;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `Material`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(MaterialVariable))]
    public sealed class MaterialVariableEditor : AtomVariableEditor<Material, MaterialPair> { }
}
