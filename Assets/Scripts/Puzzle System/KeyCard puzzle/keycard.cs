using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keycard : MonoBehaviour, IPuzzleComponent
{
    // Start is called before the first frame update
   
    CapsuleCollider sphere_collider;
    DragNDrop dragNDrop;
    Interactable interactable;
    bool state;

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
        state = true;
    }
}
