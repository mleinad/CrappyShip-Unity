using UnityEngine;
using UnityEngine.UI;

public class TutorialUIManager : MonoBehaviour  //Tutorial State Machine
{
     //handle hint/help/puzzle solving system...
     
     TutorialBaseState currentState;
     
     //states 
     TutorialOffState offState = new TutorialOffState();    //off state by default
     
     [SerializeField]
     TutorialPointState pointState;     //points to an object on screen
     [SerializeField]
     TutorialMediaState mediaState;     //shows a graphical explanation
     [SerializeField]
     TutorialBaseState hintState;  //shows a hint

     
     //UI
     public Image backdropImage;
     void Start()
     {
          currentState = offState;
          currentState.EnterState(this);
     }

     void Update()
     {
          currentState.UpdateState(this);
     }

     public void SwithState(TutorialBaseState state)
     {
          currentState.ExitState(this);
          currentState = state;
          state.EnterState(this);
     }
}
