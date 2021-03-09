using System;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `MaterialPair`. Inherits from `AtomEventReference&lt;MaterialPair, MaterialVariable, MaterialPairEvent, MaterialVariableInstancer, MaterialPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class MaterialPairEventReference : AtomEventReference<
        MaterialPair,
        MaterialVariable,
        MaterialPairEvent,
        MaterialVariableInstancer,
        MaterialPairEventInstancer>, IGetEvent 
    { }
}
