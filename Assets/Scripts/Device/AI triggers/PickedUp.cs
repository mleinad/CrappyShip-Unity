using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickedUp : MonoBehaviour, IPuzzleComponent
{
    bool state;

    [SerializeField]
    DragNDrop  dragNDrop;


    void Update()
    {
        if(dragNDrop.IsPickedUp())
        {
            state = true;
        }
    }

    public bool CheckCompletion()
    {
       return state;
    }

    public void ResetPuzzle()
    {
        state = false;
    }
}
