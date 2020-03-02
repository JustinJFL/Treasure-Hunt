using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCR_Main_Menu : MonoBehaviour
{

    public void PlayGame ()
    {
        SceneManager.LoadScene("Alleyway");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
