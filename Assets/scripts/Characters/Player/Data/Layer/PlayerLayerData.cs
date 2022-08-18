using System;
using UnityEngine;

[Serializable]
public class PlayerLayerData 
{
   [field: SerializeField] public LayerMask GroundLayer { get; private set; }

   public bool ContainsLayer(LayerMask layerMask, int layer)
   {
      return (1 << layer & layerMask) != 0; //<< is bitwise shifting. we shit 1 'layer' amounts to layer.
   }

   public bool IsGroundLayer(int layer)
   {
      return ContainsLayer(GroundLayer, layer);
   }
}
