using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



//client
public class PuzzleManager : MonoBehaviour
{

[SerializeField]
PuzzleComposite composite;

    void Start()
    {
        EventManager.Instance.onTriggerDebug += solved;
    }
    void Update()
    {
            if(composite.CheckCompletion()){
             EventManager.Instance.OnTriggerDebug("solved puzzle one");
        }
    }

    private void solved(string s)
    {
        Debug.Log(s);
        EventManager.Instance.onTriggerDebug -= solved;

    }

}
