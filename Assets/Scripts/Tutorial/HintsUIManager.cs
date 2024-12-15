using QFSW.QC;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class HintsUIManager : MonoBehaviour  //Tutorial State Machine
{
     //  request/text hint
     public Image backpanel;
     public TMP_Text instructionText;
     public TMP_Text hintText;

     //media panel
     public GameObject mediaPanel;
    
     //point panel
     public Image arrow;
     
     
     public GameObject HintGameObject;
     
     HintBaseState currentState;
     
     //states 
     public HintOffState offState = new HintOffState();    //off state by default
     public HintPointState pointState = new HintPointState();     //points to an object on screen
     public HintMediaState mediaState = new HintMediaState();     //shows a graphical explanation
     public RequestHintState requestHintState = new RequestHintState();  //shows a hint request
     public HintTextState textState = new HintTextState();     //simple comment/text hint 
     
     //UI
     public Image backdropImage;
     
     
     public static HintsUIManager Instance;
     void Start()
     {
          Instance = this;
          
          BackdropState(0, false);
          currentState = offState;
          currentState.EnterState(this);

          if (HintGameObject.activeSelf == false)
          {
               HintGameObject.SetActive(true);
          }
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
     void GiveHint(int i)
     {

          switch (i)
          {
               case 0:
                    SwitchState(requestHintState);  //requests hint
                    requestHintState.SetHint(textState); //hint type
                    textState.SetHintText("you should pay more attention to this tutorial!");  //hint content 
                    break;
               
               case 1:
                    SwitchState(requestHintState);
                    requestHintState.SetHint(mediaState);
                    
                    break;

               case 2:
                    SwitchState(requestHintState);
                    requestHintState.SetHint(pointState);
                    break;
          }
     }
}
