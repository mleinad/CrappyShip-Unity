using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class MiniMapElement : MonoBehaviour
{
    // Start is called before the first frame update
   

    [SerializeField]
    private Sprite sprite;
   
    [SerializeField]
    GameObject element_prefab;

    private RectTransform element_transform;
    private Transform canvas_transform;

    void Awake(){
    }

    void Start()
    {
        canvas_transform = Mini_map_controller.Instance.GetCanvasTransform();


        InstatiateElement();
    }

    // Update is called once per frame
    void Update()
    {
        element_transform.position = new Vector3(transform.position.x, 0, transform.position.z);    //update frame by frame, might be unecessary
    }


    void InstatiateElement(){
         GameObject element;
       element = Instantiate(element_prefab, canvas_transform);
       element.GetComponent<SpriteRenderer>().sprite = sprite;
       element_transform = element.GetComponent<RectTransform>();
    }


    public Sprite GetSprite()
    {
        return sprite;
    }

}
