using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{

   [SerializeField]
   Material environmentMaterial;
   [SerializeField]
   List<Light> lights;

   public static LightManager Instance { get; private set; }
   private void Start()
   {
      Instance = this;
      EventManager.Instance.onTurnOnLights += TurnOnLights;
   }

   void LoadLights()
   {
      //load lights from game object
   }

   public void EnableEnviormentLight(bool state)
   {
      if(state) environmentMaterial.EnableKeyword("_EMISSION");
      else environmentMaterial.DisableKeyword("_EMISSION");
   }

   public void TurnOnLights(bool state)
   { 
      foreach(var light in lights)
      {
         light.enabled = state;
      }
   }
}
