using System;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `Material`. Inherits from `AtomEventReference&lt;Material, MaterialVariable, MaterialEvent, MaterialVariableInstancer, MaterialEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class MaterialEventReference : AtomEventReference<
        Material,
        MaterialVariable,
        MaterialEvent,
        MaterialVariableInstancer,
        MaterialEventInstancer>, IGetEvent 
    { }
}
