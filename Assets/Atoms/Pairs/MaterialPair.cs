using System;
using UnityEngine;
namespace UnityAtoms
{
    /// <summary>
    /// IPair of type `&lt;Material&gt;`. Inherits from `IPair&lt;Material&gt;`.
    /// </summary>
    [Serializable]
    public struct MaterialPair : IPair<Material>
    {
        public Material Item1 { get => _item1; set => _item1 = value; }
        public Material Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private Material _item1;
        [SerializeField]
        private Material _item2;

        public void Deconstruct(out Material item1, out Material item2) { item1 = Item1; item2 = Item2; }
    }
}