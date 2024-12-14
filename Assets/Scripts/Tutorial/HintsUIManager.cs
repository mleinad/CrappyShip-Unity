using QFSW.QC;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class HintsUIManager : MonoBehaviour  //Tutorial State Machine
{

     #region  UI elements

     public GameObject a;
     public GameObject b;
     public GameObject c;
     public GameObject d;
     
     #endregion
     
     
     //handle hint/help/puzzle solving system...
     
     HintBaseState currentState;
     
     //states 
     public HintOffState offState;    //off state by default
     public HintPointState pointState;     //points to an object on screen
     public HintMediaState mediaState;     //shows a graphical explanation
     public RequestHintState requestHintState;  //shows a hint request
     public HintTextState textState;     //simple comment/text hint 
     
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

     public void SwitchState(HintBaseState state)
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


     [Preserve]
     [Command]
     void GiveHint()
     {
          SwitchState(requestHintState);  //requests hint
          
          requestHintState.SetHint(textState); //hint type
          textState.SetHintText("you should pay more attention to this tutorial.");  //hint content 
     }
}
