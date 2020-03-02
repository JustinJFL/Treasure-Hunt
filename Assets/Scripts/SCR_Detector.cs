using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Detector : MonoBehaviour
{
    public bool isFound = false;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            isFound = true;
        }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("player"))
        {
                isFound = false;
        }
        }
    }
}
