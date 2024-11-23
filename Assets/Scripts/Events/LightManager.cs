using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{

   [SerializeField]
   Material environmentMaterial;
   [SerializeField]
   List<Light> lights;

   private void Start()
   {
      EventManager.Instance.onTurnOnLights += TurnOnLights;
   }

   public void EnableEnviormentLight(bool state)
   {
      if(state) environmentMaterial.EnableKeyword("_EMISSION");
      else environmentMaterial.DisableKeyword("_EMISSION");
   }

   void TurnOnLights(bool state)
   { 
      foreach(var light in lights)
      {
         light.enabled = state;
      }
   }
}
