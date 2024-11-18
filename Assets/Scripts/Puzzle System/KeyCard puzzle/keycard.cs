using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
public class keycard : MonoBehaviour, IPuzzleComponent
{
    // Start is called before the first frame update
   
    CapsuleCollider sphere_collider;
    DragNDrop dragNDrop;
    Interactable interactable;
    bool state;
    bool onHand = false;

    public bool CheckCompletion()=> state;
    public void ResetPuzzle()=> state = false;
    void Start()
    {
        dragNDrop = GetComponent<DragNDrop>();
        sphere_collider = GetComponent<CapsuleCollider>();
        interactable = GetComponent<Interactable>();
        sphere_collider.enabled = false;
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(onHand) return;
        if(dragNDrop.IsPickedUp())
        {
            sphere_collider.enabled = true;
        }
        else sphere_collider.enabled = false;

        if(interactable.WasTriggered())
        {
            CollectCard();
        }
    }

    void CollectCard()
    {  
        

        if(state == false)
        {
        transform.GetComponent<Rigidbody>().isKinematic =true;
        dragNDrop.enabled = false;

        Player.Instance.RaiseRightArm(1f);

        SetLayerRecursively(transform, 9);

        transform.SetParent(Player.Instance.GetPlayerRightHand());
        
        transform.localPosition = new Vector3(0.204f, 0.074f, 0.003f);
        transform.localRotation = Quaternion.Euler(-255.281f, 261.311f, 91.676f);

        dragNDrop.enabled = false;
        sphere_collider.enabled = false;
        interactable.enabled = false;
        

        StartCoroutine(TurnOffCrosshairAfterDelay(0.2f));
        state = true;

        onHand = true;
        }
        // transform.position = new Vector3 (0,0,0);
        //add animation

    }

     void SetLayerRecursively(Transform transform, int layer)
    {
        transform.gameObject.layer = layer;
        foreach (Transform child in transform)
        {
            SetLayerRecursively(child, layer);
        }
    }
     IEnumerator TurnOffCrosshairAfterDelay(float delay)
        {
        yield return new WaitForSeconds(delay);
        Player.Instance.CrosshairOff();
    }
}
