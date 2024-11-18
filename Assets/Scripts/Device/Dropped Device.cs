using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DroppedDevice : MonoBehaviour, IPuzzleComponent
{
    CapsuleCollider sphere_collider;
    DragNDrop dragNDrop;
    Interactable interactable;
    
    
    [SerializeField]
    GameObject handDevice;
    [SerializeField]
    Light lightDevice;
   
    
    bool state;
    bool onHand = false;

    MeshRenderer meshRenderer;

    private void Start()
    {
        dragNDrop = GetComponent<DragNDrop>();
        sphere_collider = GetComponent<CapsuleCollider>();
        interactable = GetComponent<Interactable>();
        sphere_collider.enabled = false;
        handDevice.SetActive(false);
        state = false;
        meshRenderer = GetComponent<MeshRenderer>(); 
        LightManager.Instance.EnablePlayerLight(false);
        LightManager.Instance.EnableEnviormentLight(false);

    
    }

    private void Update()
    {
        if(onHand) return;
        if(dragNDrop.IsPickedUp())
        {
            sphere_collider.enabled = true;
        }
        else sphere_collider.enabled = false;

        if(interactable.WasTriggered() && state == false)
        {
            Pickup();    
        }
    }

    void Pickup()
    {
        state = true; 
        
        lightDevice.enabled = false;
        meshRenderer.enabled = false;
        //meshRenderer.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
        StartCoroutine(SetUp(1f));
    }
    public bool CheckCompletion() => state;

    public void ResetPuzzle()
    {
        state = false;
    }


    IEnumerator SetUp(float time)
    {
        handDevice.SetActive(true);
        yield return new WaitForSeconds(time);
        
        EventManager.Instance.OnAiInteraction(this);
        
        
        yield return new WaitForSeconds(time + 3f);
        
        LightManager.Instance.EnableEnviormentLight(true);
        LightManager.Instance.EnablePlayerLight(true);
        
        gameObject.SetActive(false);

    }
}
