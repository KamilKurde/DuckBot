using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Set variable value Action of type `Material`. Inherits from `SetVariableValue&lt;Material, MaterialPair, MaterialVariable, MaterialConstant, MaterialReference, MaterialEvent, MaterialPairEvent, MaterialVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Material", fileName = "SetMaterialVariableValue")]
    public sealed class SetMaterialVariableValue : SetVariableValue<
        Material,
        MaterialPair,
        MaterialVariable,
        MaterialConstant,
        MaterialReference,
        MaterialEvent,
        MaterialPairEvent,
        MaterialMaterialFunction,
        MaterialVariableInstancer>
    { }
}
