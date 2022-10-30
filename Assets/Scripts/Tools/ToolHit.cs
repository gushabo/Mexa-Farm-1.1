using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHit : MonoBehaviour
{
   public virtual void Golpe()
   {
    
   }

   public virtual bool CanBeHit(List<ResourceNodeType> canBeHit)
   {
      return true;
   }
}
