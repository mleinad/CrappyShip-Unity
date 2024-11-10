using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogContent : MonoBehaviour
{
    public List<string> dialog;

    public bool isRepeated;

    private int count;


    public List<string> GetLines()
    {
        //if been repeated could change...

        return dialog;
    }
}
