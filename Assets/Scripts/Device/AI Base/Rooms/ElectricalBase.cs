using System.Collections;
using UnityEngine;

public class ElectricalBase : MonoBehaviour, IBaseContent
{
    [SerializeField] GameObject comp1, comp2, comp3, comp4;
    [SerializeField] Transform offset1, offset2, offset3, offset4;
    [SerializeField] private GameObject highlightObject;
    
    AIBase manager;
    bool isDisplaying = false;
    public void Play()
    {
        isDisplaying = true;
        StartCoroutine(Content());
    }

    public void SetManager(AIBase aiBase)
    {
        manager = aiBase;
    }

    public void Disable()
    {
        this.enabled = false;
    }

    IEnumerator Content()
    {
        
        yield return new WaitForSeconds(0.3f);
        ShowComponents();
        yield return new WaitForSeconds(2f);
     //   manager.PointTowards(highlightObject);
       
        yield return new WaitForSeconds(4f);
    }
    
    public void Update()
    {
        if (!isDisplaying) return;
        Rotate(offset1);
        Rotate(offset2);
        Rotate(offset3);
        Rotate(offset4);
    }

    void ShowComponents()
    {
        manager.Display(comp1, offset1);
        manager.Display(comp2, offset2);
        manager.Display(comp3, offset3);
        manager.Display(comp4, offset4);
    }


    void Rotate(Transform objTransform)
    {
        objTransform.Rotate(Vector3.up, 10f * Time.deltaTime);
    }
}