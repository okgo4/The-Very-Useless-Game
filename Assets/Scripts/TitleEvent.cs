using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEvent : MonoBehaviour
{
    public void loadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("WPTank");
    }


    public void exitGame()
    {
        Application.Quit();
    }
}

