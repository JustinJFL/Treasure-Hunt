using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SCR_CameraController : MonoBehaviour
{
    public GameObject Player;

    public Camera cam;

    public NavMeshAgent agent;

    float xRotation = 0f;

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    
    float gamepadXRotation = 0f;
    

    void Start()
    {
        //Hides and locks the cursor to the center of the sceen
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;


        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
        //Mouse movement tied to player's sight
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        //Clamps to make sure there is no over rotation
        xRotation = Mathf.Clamp(xRotation, -55f, 60f);

        //Player body turns and pivots with camera movement
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        
        
        //When the player hits the crouch button (as defined by input manager) they should crouch and then return to original height when let go
        if (Input.GetButtonDown("Crouch"))
            transform.position += Vector3.down * 10;
        else if(Input.GetButtonUp("Crouch"))
            transform.position += Vector3.up * 10;
        

        //Creating new crouch binded to animation
        

        //Attempting gamepad controls
        /*
        float gamepadX = Input.GetAxis("JoystickRightHorizontal") * mouseSensitivity * Time.deltaTime;
        float gamepadY = Input.GetAxis("JoystickRightVertical") * mouseSensitivity * Time.deltaTime;

        gamepadXRotation -= gamepadY;

        gamepadXRotation = Mathf.Clamp(gamepadXRotation,-90f,90f);

        transform.localRotation = Quaternion.Euler(gamepadXRotation,0f,0f);
        playerBody.Rotate(Vector3.up * gamepadX);
        */

        //float distance = Vector3.Distance(transform.position, Player.transform.position);

        //Debug.Log("Distance: " + distance);

        //Run Away
        //if (distance < EnemyDistanceRun)
        { 
            //Vector Player to me
            //Vector3 

       // if (Input.GetMouseButtonDown(0))
        //{
           // Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

           // if (Physics.Raycast(ray, out hit))
           // {
                // Move Agent
            //    agent.SetDestination(hit.point);
            //}
        }
    }
}
