using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour, IPuzzleNode
{   
    [SerializeField]
    List<IPuzzleNode> _NodL;

    [SerializeField]
    List<IPuzzleBehavior> _compL;
    
    [SerializeField]
    //List<IActions> _ActL;


        bool has_components;

        bool is_ordered;

        bool state;


    public void ExecuteNode()
    {
        if(has_components){
            CheckComponents();
        }
    
        if(HasChild())
        {
            foreach(IPuzzleNode node in _NodL)
            {
                node.ExecuteNode();
            }
        }
    }
    

    private bool HasChild(){
        return _NodL.Count == 0;
    }

    public void CheckStatus()
    {

    }

    public void PlayActions()
    {
      /*  foreach(IActions action in _ActL){
            action.Perform();
        }*/
    }

    bool CheckComponents(){
        int i = 0;
        foreach(IPuzzleBehavior component in _compL)
        {
            if(component.CheckCompletion())i++;
            if(is_ordered)
            {
                component.ResetPuzzle();
            }
        }
        
        return i==_compL.Count;    
    }

    public void Reset()
    {
        state = false;
    }


}
