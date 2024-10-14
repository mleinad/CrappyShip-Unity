using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TabGroup : MonoBehaviour
{


    [SerializeField]
    List<Tab> tab_buttons;
    
    public List<GameObject> objects_to_swap;
    public Tab selected_tab;

   public void Subscribe(Tab button){
        if(tab_buttons==null)
        {
            tab_buttons = new List<Tab>();
        }

        tab_buttons.Add(button);
   }

   public void OnTabEnter(Tab button)
    {
        ResetTabs();
        if(selected_tab == null||button!=selected_tab)
        {
     //   button.background.sprite = tab_hover;
        button.animator.SetTrigger("Highlighted");
        }
    }
        
   public void OnTabExit(Tab button)
   {
        ResetTabs();
   }

   public void OnTabSelected(Tab button)
    {
       if(selected_tab != null)
       {
        selected_tab.Deselect();
       }
       
        selected_tab = button;

        selected_tab.Select();

        ResetTabs();
        //button.background.sprite = tab_active;
        button.animator.SetTrigger("Pressed");


        int index = button.transform.GetSiblingIndex();
        for(int i =0; i<objects_to_swap.Count;i++)
        {
            if( i == index)
            { 
                objects_to_swap[i].SetActive(true);
            }
            else
            {
                objects_to_swap[i].SetActive(false);
            }
        }
    }


   public void ResetTabs()
   {
    foreach(Tab button in tab_buttons)
    {
        if(selected_tab!=null && button == selected_tab){continue;}
       // button.background.sprite = tab_idle;
        button.animator.SetTrigger("Normal");
    }
   }


}
                