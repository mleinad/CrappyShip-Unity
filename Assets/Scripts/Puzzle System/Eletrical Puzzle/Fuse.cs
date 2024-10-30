using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using QFSW.QC;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(DragNDrop))]
public class Fuse : MonoBehaviour
{
    bool isAttachted;
    DragNDrop dragNDrop;
    Rigidbody rigidbody;

    CapsuleCollider capsuleCollider;

    FuseBox fuseBox;
    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
        dragNDrop = GetComponent<DragNDrop>();
        if(transform.parent!=null) isAttachted = true;
    
    
        capsuleCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttachted)
        {
            dragNDrop.enabled = false;
            rigidbody.isKinematic = true;
        }
        else
        {
            if(dragNDrop.IsPickedUp())
            {
                GetClosestFusebox();
            }else  capsuleCollider.enabled = false;
        }
    }

    [Command]
    public void ShortFuse()
    {
        isAttachted = false;
        rigidbody.isKinematic= false;
        transform.SetParent(null);
        dragNDrop.enabled = true;
        rigidbody.AddExplosionForce(50f, Vector3.up, 20f);
        Debug.Log("detatched");

    }

    void AttachFuse(FuseBox fuseBox)
    {
        transform.SetParent(fuseBox.transform);
        transform.rotation = Quaternion.identity;
        transform.localPosition = new Vector3 (0, 1.705668f, 0);
        capsuleCollider.enabled = false;
        fuseBox.fuse = this;
        isAttachted = true;
    }

     void GetClosestFusebox()
    {
        capsuleCollider.enabled = true;
    } 

    void OnTriggerEnter(Collider other)
    {

        FuseBox fuseBox = other.GetComponent<FuseBox>();

        if(fuseBox != null)
        {
            if(!dragNDrop.IsPickedUp())
            {
                AttachFuse(fuseBox);
                Debug.Log("reatached fuse");
            }

        }


    }
}
