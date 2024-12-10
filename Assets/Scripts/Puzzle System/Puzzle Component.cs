using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//component
public interface IPuzzleComponent 
{
     bool CheckCompletion();
     void ResetPuzzle();  
}
