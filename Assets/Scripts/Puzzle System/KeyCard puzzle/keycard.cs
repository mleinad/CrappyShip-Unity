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

        

        if(state == false)
        {
        transform.GetComponent<Rigidbody>().isKinematic =true;
        dragNDrop.enabled = false;
        transform.DOMove(Player.Instance.GetPlayerRightHand(),2f);//improve
        StartCoroutine(TurnOffCrosshairAfterDelay(0.2f));
        state = true;

        }
        // transform.position = new Vector3 (0,0,0);
        //add animation

    }
     IEnumerator TurnOffCrosshairAfterDelay(float delay)
        {
        yield return new WaitForSeconds(delay);
        Player.Instance.CrosshairOff();
    }
}
