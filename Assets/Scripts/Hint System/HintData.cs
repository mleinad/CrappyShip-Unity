
using System;
using Mono.CSharp;
using UnityEngine;

public interface HintData
{
    public Vector3[] Area { get; set; }
    public bool PlayerInArea();
    
}