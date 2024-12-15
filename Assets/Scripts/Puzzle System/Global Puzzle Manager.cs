using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC.Actions;
using UnityEngine;

public class GlobalPuzzleManager : MonoBehaviour
{
   public List<PuzzleComposite> PuzzleList;

   private PuzzleComposite currentPuzzle = null;
   
   public float totalTime;
   public float timeSinceLastAction;
   
   public float hintTime = 10f;
   public float timeLimit = 100f;
   private void Update()
   {
      if (currentPuzzle)
      {
         totalTime += Time.deltaTime;
         
         timeSinceLastAction += Time.deltaTime;

         if (timeSinceLastAction >= hintTime)
         {
            Hint();
         }

         if (totalTime >= timeLimit)
         {
           SolvePuzzle();
         }
      }   
   }

   void Hint()
   {
      
   }
   
   public void StartNewPuzzle(PuzzleComposite current)
   {
      currentPuzzle = current;
      totalTime = 0f;
   }

   public void SetAction()
   {
      timeSinceLastAction = 0f;
   }

   void SolvePuzzle()
   {
      RequestHintState request = HintsUIManager.Instance.requestHintState;
      HintsUIManager.Instance.SwitchState(request);
            
      request.text = "Solve Puzzle";
      request.SetHint(HintsUIManager.Instance.offState);
      
      currentPuzzle.Solve();
      currentPuzzle.gameObject.SetActive(false);
   }
}
