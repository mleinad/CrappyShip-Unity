using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzleBehavior 
{
     void StartPuzzle();
     bool CheckCompletion();
     void ResetPuzzle();  

}
