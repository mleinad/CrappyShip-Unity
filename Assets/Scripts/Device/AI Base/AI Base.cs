
using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AIBase : MonoBehaviour
{
    public Transform aiParent;
    
    public PuzzleComposite composite;
    public Material hologramMaterial;

    public GameObject hologramCube;
    
    bool beenTriggered = false;
    IBaseContent content;
    
    private void Start()
    {
        EventManager.Instance.onTriggerSolved += PuzzleSolved;
        content = GetComponent<IBaseContent>();
        content.SetManager(this);
    }

    private void Update()
    {
        if (beenTriggered)
        {
            aiParent.transform.LookAt(Player.Instance.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {   
        
        //compare to check if is player USE singleton
        if (other.transform == Player.Instance.transform)
        {
            if (!beenTriggered)
            { 
                beenTriggered = true;
                HologramManager.Instance.docked = true;
                InstantiateAI();
                content.Play();
                
            }
        }
    }
    
    public void Display(GameObject target, Transform offset)
    {
        Debug.Log(target.name);
        GameObject newTarget = Instantiate(target, offset);
        
        newTarget.transform.localPosition = Vector3.zero;
        newTarget.GetComponent<Renderer>().material = hologramMaterial;
    }
    
    public void PointTowards(GameObject target)
    {
        Instantiate(hologramCube);        
        hologramCube.transform.position = target.transform.position;
    }
    private void InstantiateAI()
    {
        Transform aiTransform = HologramManager.Instance.hologramObject.transform;
        aiTransform.gameObject.SetActive(true); 
        aiTransform.DOMove(transform.position, 5f).SetEase(Ease.OutBounce); //.OnComplete()
        
        aiTransform.SetParent(aiParent);
        aiTransform.localPosition = Vector3.zero;
        aiTransform.localRotation = Quaternion.identity;
        aiTransform.localScale = Vector3.one * 2f;
            
    }

    private void PuzzleSolved(IPuzzleComponent component)
    {
        if (composite == component)
        {
            DestroyAI();
        }
    }
    
    private void DestroyAI()
    {
        HologramManager.Instance.ResetPosition();
        HologramManager.Instance.hologramObject.SetActive(false);
        content.Disable();
        //further disable logic
    }
}
