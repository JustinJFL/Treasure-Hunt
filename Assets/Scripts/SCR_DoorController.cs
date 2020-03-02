using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SCR_DoorController : MonoBehaviour
{

    private bool isOpened = false;

    public Animator doorAnimator;

    public GameObject DoorInteractText;
    public GameObject InteractPanel;

    private GameObject DoorOpenSFX;
    private GameObject DoorCloseSFX;
    private int SFXCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        InteractPanel.SetActive(false);
        DoorInteractText.SetActive(false);

        //finding door sound effects game objects by tag as opposed to manually assigning them on each door.
        DoorOpenSFX = GameObject.FindWithTag("DoorOpenSFX");
        DoorCloseSFX = GameObject.FindWithTag("DoorCloseSFX");
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpened == true && Input.GetButtonDown("Interact"))
        {
            
            doorAnimator.SetBool("CloseDoor", false);
            doorAnimator.SetBool("OpenDoor", true);
            doorAnimator.SetBool("Idle", false);
            //using count to ensure the sound only plays once
            if(SFXCount == 0)
            {
                DoorOpenSFX.GetComponent<AudioSource>().Play();
                SFXCount++;
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractPanel.SetActive(true);
            isOpened = true;
            DoorInteractText.SetActive(true);
        }
        if (other.CompareTag("guard"))
        {
            isOpened = true;
            doorAnimator.SetBool("OpenDoor", true);
            if(SFXCount == 0)
            {
                DoorOpenSFX.GetComponent<AudioSource>().Play();
                SFXCount++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            doorAnimator.SetBool("CloseDoor", true);
            doorAnimator.SetBool("OpenDoor", false);
            doorAnimator.SetBool("Idle", true);
            isOpened = false;
            InteractPanel.SetActive(false);
            DoorInteractText.SetActive(false);
            //Resets the count to allow door open sound to play again
            if(SFXCount >0)
            {
                DoorCloseSFX.GetComponent<AudioSource>().Play();
                SFXCount = 0;
            }
        }
        
        if (other.CompareTag("guard"))
        {

            doorAnimator.SetBool("CloseDoor", true);
            doorAnimator.SetBool("OpenDoor", false);
            doorAnimator.SetBool("Idle", true);
            isOpened = false;
            //Resets the count to allow door open sound to play again
            if(SFXCount >0)
            {
                DoorCloseSFX.GetComponent<AudioSource>().Play();
                SFXCount = 0;
            }
        }

    }
}

