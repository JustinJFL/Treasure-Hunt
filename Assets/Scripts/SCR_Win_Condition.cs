using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCR_Win_Condition : MonoBehaviour
{

    public GameObject DoorInteractText;
    public GameObject InteractPanel;
    public GameObject VictoryScreen;

    public static bool GamePause = false;

    public GameObject PaintingCanvas;

    // Start is called before the first frame update
    void Start()
    {
        PaintingCanvas.SetActive(true);

        InteractPanel.SetActive(false);
        DoorInteractText.SetActive(false);
        VictoryScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {


    }
       
        

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PaintingCanvas.activeInHierarchy == false)
        {
            //InteractPanel.SetActive(true);
            //DoorInteractText.SetActive(true);

            VictoryScreen.SetActive(true);

            Time.timeScale = 0f;
            GamePause = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractPanel.SetActive(false);
            DoorInteractText.SetActive(false);
            VictoryScreen.SetActive(false);

        }

    }

    public void Restart()
    {
        SceneManager.LoadScene("Alleyway");
    }

    public void QuitGame()
    {

        SceneManager.LoadScene("Main Menu");
    }

}
