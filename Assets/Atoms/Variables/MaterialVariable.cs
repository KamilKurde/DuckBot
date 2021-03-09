using System;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Variable of type `Material`. Inherits from `AtomVariable&lt;Material, MaterialPair, MaterialEvent, MaterialPairEvent, MaterialMaterialFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Material", fileName = "MaterialVariable")]
    public sealed class MaterialVariable : AtomVariable<Material, MaterialPair, MaterialEvent, MaterialPairEvent, MaterialMaterialFunction>
    {
        protected override bool ValueEquals(Material other)
        {
            throw new NotImplementedException();
        }
    }
}
