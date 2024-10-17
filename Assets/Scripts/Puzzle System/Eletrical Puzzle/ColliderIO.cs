using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderIO : MonoBehaviour
{

    IEletricalComponent component;
    
    int ID;
    int type;   //1 input, 2 output, 3 i/o
   
   void Awake()
   {
    component= GetComponentInParent<IEletricalComponent>();
   }

    public int GetID()=> ID;
    public int Get_Type()=> type;


    void Start()
    {
        ID = transform.GetSiblingIndex();

        Debug.Log(transform.parent.name + " " + ID);
    }
}
