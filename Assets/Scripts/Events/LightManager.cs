using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
   [SerializeField]
   Light playerLight;
   [SerializeField]
   Material environmentMaterial;
   
   public static LightManager Instance { get; private set; }

   private void Awake()
   {

         // Implement Singleton Pattern
         if (Instance == null)
         {
            Instance = this;  // Set this instance as the singleton instance
         }
         else if (Instance != this)
         {
            Destroy(gameObject);  // Destroy the extra instance to ensure there is only one
         }

         DontDestroyOnLoad(gameObject);  // Optional: Persist the singleton across scenes
  }


   public void EnablePlayerLight(bool state)
   {
      playerLight.enabled = state;
   }

   public void EnableEnviormentLight(bool state)
   {
      if(state) environmentMaterial.EnableKeyword("_EMISSION");
      else environmentMaterial.DisableKeyword("_EMISSION");
   }
}
