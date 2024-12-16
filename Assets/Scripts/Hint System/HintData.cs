using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using QFSW.QC.Actions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "HintData", menuName = "Hints/HintData", order = 1)]
public class HintData : ScriptableObject
{
    public SerializedDictionary<Composite, List<string>> textHintsDictionary;
    public SerializedDictionary<Composite, List<Texture>>imageTextures;
    public SerializedDictionary<Composite, List<VideoClip>> videos;
    public SerializedDictionary<Composite, List<Transform>> transformList;
    

}