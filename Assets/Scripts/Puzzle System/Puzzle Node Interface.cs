using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzleNode
{
    public void PlayActions();

    public void CheckStatus();
    public void ExecuteNode();
    public void Reset();

}


