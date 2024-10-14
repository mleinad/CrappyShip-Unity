using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Tab : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{


    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselcted;
    public TabGroup tabGroup;
    public Animator animator;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        tabGroup.Subscribe(this);
        animator = GetComponent<Animator>();
    }

    public void Deselect()
    {
        if(onTabDeselcted!=null)
        {
            onTabDeselcted.Invoke();
        }
    }
    public void Select()
    {
        if(onTabSelected!=null)
        {
            onTabSelected.Invoke();
        }
    }

}
