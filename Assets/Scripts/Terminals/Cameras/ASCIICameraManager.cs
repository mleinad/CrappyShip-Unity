using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ASCIICameraManager : MonoBehaviour
{
    
    public static ASCIICameraManager Instance;

    private int _activeCameras;
    public int MaxActiveCameras = 10;
    public int ActiveCameras
    {
        get => _activeCameras;
        set
        {
            if (_activeCameras != value)
            {
                Debug.Log(value);
                if (_activeCameras > MaxActiveCameras)
                {
                    DestroyFirst();
                }
            }
        }
    }
    void Start()
    {
        Instance = this;        
        Position();
        _activeCameras = transform.childCount;
    }

    void Position()
    {
        float offset =0;
        foreach (Transform child in transform)
        {
            child.localPosition = new Vector3(offset, 0, 0);
            offset += 20f;
        }
    }

    public void AddObject(Transform newCamTransform)
    {
        Vector3 newCamPos = transform.GetChild(transform.childCount - 1).localPosition;
        newCamTransform.parent = transform;
        newCamPos.x += 20f;
        newCamTransform.localPosition = newCamPos;
    }

    void DestroyFirst()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
