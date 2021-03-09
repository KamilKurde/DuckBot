using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Adds Variable Instancer's Variable of type Material to a Collection or List on OnEnable and removes it on OnDestroy. 
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync Material Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncMaterialVariableInstancerToCollection : SyncVariableInstancerToCollection<Material, MaterialVariable, MaterialVariableInstancer> { }
}
