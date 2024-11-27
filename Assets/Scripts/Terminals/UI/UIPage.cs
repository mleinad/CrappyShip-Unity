using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;
public class UIPage
{
    public List<List<TMP_Text>> content;
    
    int currentPage;
    public UIPage(List<TMP_Text> pageContent)
    {
        content = new List<List<TMP_Text>>();
        content.Add(pageContent);
        currentPage = 0;
    }
    public void UpdatePage()
    {
        
    }

   public int GetElementID(string s)
    {
        return content[currentPage].FindIndex(line => line.text.Contains(s));
    }

    public void TurnOffPage()
    {
        EnableUI(content[currentPage],false);
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
