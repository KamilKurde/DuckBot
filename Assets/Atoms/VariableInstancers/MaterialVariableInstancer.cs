using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Variable Instancer of type `Material`. Inherits from `AtomVariableInstancer&lt;MaterialVariable, MaterialPair, Material, MaterialEvent, MaterialPairEvent, MaterialMaterialFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Material Variable Instancer")]
    public class MaterialVariableInstancer : AtomVariableInstancer<
        MaterialVariable,
        MaterialPair,
        Material,
        MaterialEvent,
        MaterialPairEvent,
        MaterialMaterialFunction>
    { }
}
