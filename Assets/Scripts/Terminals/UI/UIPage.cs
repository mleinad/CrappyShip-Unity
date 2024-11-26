using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;
public class UIPage
{
    private List<List<TMP_Text>> content;
    public void StartPage()
    {
        
    }
    public void UpdatePage()
    {
        
    }

    int GetElementID(string s)
    {
        return 0;
    }

    private void SelectPage(int page)
    {
        EnableUI(content[page], true);
        foreach (var pages in content.Where(pages => pages != content[page]))
        {
            EnableUI(pages, false);
        }
    }
    
    private void EnableUI(List<TMP_Text> layout, bool enable)
    {
        foreach (var line in layout)
        {
            line.transform.parent.gameObject.SetActive(enable);
        }
    }
}
