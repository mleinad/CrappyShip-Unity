using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalPuzzleManager : MonoBehaviour
{
    IEletricalComponent[] eletricalComponents_graph;

    void GenerateGraph(){

        eletricalComponents_graph = transform.GetComponentsInChildren<IEletricalComponent>();
    }

    void SetSignalRecursive(List<IEletricalComponent> adj_comp)
    {
        foreach(IEletricalComponent comp in adj_comp)
        {
            
        }
    }
}
