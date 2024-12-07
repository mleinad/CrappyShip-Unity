using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalData : ScriptableObject
{
    [SerializeField]
    protected List<string> texFiles;
    
    [SerializeField]
    protected List<string> hidden_textFiles;
    
    [SerializeField]
    protected List<Texture> imagesFiles;
    
    [SerializeField]
    protected List<Texture> hidden_imagesFiles;
}
