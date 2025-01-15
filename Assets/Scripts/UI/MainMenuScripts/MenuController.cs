using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string enterGame;
    public void StartButton()
    {   
        SceneManager.LoadScene(enterGame);
    }



    public void ExitButton()
    {
        Application.Quit();
    }
}
