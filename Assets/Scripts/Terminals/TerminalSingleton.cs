using UnityEngine;

public class TerminalSingleton : MonoBehaviour
{
    public Terminal [] terminals;
    
    public static TerminalSingleton Instance;
    private void Awake()
    {
        Instance = this;
        terminals = FindObjectsOfType<Terminal>();
    }
    
    public void DisableOtherTerminals(Terminal current)
    {
        foreach (var terminal in terminals)
        {
            if(terminal == current) continue;
            
            terminal.gameObject.SetActive(false);
        }
    }
    
    public void ReenableTerminals(Terminal current)
    {
        foreach (var terminal in terminals)
        {
            if (terminal == current) continue;

            terminal.gameObject.SetActive(true);
            terminal.camera.enabled = false;
        }
    }
}
