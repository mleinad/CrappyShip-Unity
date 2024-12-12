using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "UIElementData", menuName = "UI/Element Data")]
public class UIElementData : ScriptableObject
{
    public List<GameObject> elements;
    
}