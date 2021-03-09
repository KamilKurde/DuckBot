using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Reference of type `Material`. Inherits from `AtomReference&lt;Material, MaterialPair, MaterialConstant, MaterialVariable, MaterialEvent, MaterialPairEvent, MaterialMaterialFunction, MaterialVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class MaterialReference : AtomReference<
        Material,
        MaterialPair,
        MaterialConstant,
        MaterialVariable,
        MaterialEvent,
        MaterialPairEvent,
        MaterialMaterialFunction,
        MaterialVariableInstancer>, IEquatable<MaterialReference>
    {
        public MaterialReference() : base() { }
        public MaterialReference(Material value) : base(value) { }
        public bool Equals(MaterialReference other) { return base.Equals(other); }
        protected override bool ValueEquals(Material other)
        {
            throw new NotImplementedException();
        }
    }
}
