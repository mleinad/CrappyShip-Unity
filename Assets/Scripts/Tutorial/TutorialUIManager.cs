using UnityEngine;
using UnityEngine.UI;

public class TutorialUIManager : MonoBehaviour  //Tutorial State Machine
{
     //handle hint/help/puzzle solving system...
     
     TutorialBaseState currentState;
     
     //states 
     public TutorialOffState offState;    //off state by default
     public TutorialPointState pointState;     //points to an object on screen
     public TutorialMediaState mediaState;     //shows a graphical explanation
     public TutorialHintState hintState;  //shows a hint

     
     //UI
     public Image backdropImage;
     void Start()
     {
          BackdropState(0, false);
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


     public void BackdropState(float intensity, bool state)
     {
          backdropImage.enabled = state;
          Color color = backdropImage.color;
          color.a = intensity;
          backdropImage.color = color;
     }
}
