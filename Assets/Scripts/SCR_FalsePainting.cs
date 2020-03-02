using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_FalsePainting : MonoBehaviour
{
    //Cameras
    public Camera PlayerCam;
    public Camera PaintingCam;
    //Freeze camera movement
    public bool camMove1;
    //Pan Speed
    public float panSpeed = 20f;
    //Border Thickness is area the mouse has to reach before moving camera
    public float panBorderThickness = 10f;
    //Zoom Speed
    public float scrollSpeed = 20f;
    //Camera boundaries
    public float minY = 0f;
    public float maxY = 0f;
    public float minX = 0f;
    public float maxX = 0f;
    public float minZ = 0f;
    public float maxZ = 0f;
    public GameObject Player;
    //public GameObject Painting;
    //public GameObject EmptyPainting;
    public Rigidbody PlayerRB;
    public CharacterController PlayerCC;
    public Transform PlayerTransform;
    public GameObject IntPanel;

    public GameObject PaintingInteractText;
    
    public SCR_CameraController PlayerCamScript;
    public GameObject mcguffin;
    public SCR_Detector SDetect;
    private bool isInspecting = false;

    //LayerMask
    public LayerMask layerMask;

    public GameObject PaintingCanvas;
    public GameObject PaintingSignature;

    bool enter;
    bool playerMove;


    // Start is called before the first frame update

    void Start()
    {
        GetComponent<GameObject>();
        PlayerCam.enabled = true;
        PaintingCam.enabled = false;
        PlayerCamScript = PlayerCam.GetComponent<SCR_CameraController>();
        enter = false;
        GameObject Player = GameObject.FindWithTag("Player");
        mcguffin = GameObject.FindWithTag("mcguffin");
        PlayerRB = Player.GetComponent<Rigidbody>();

        PlayerCC = Player.GetComponent<CharacterController>();

        PlayerTransform = Player.GetComponent<Transform>();

        PaintingInteractText.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Locked;
        //RaycastHit hit;

        PaintingCanvas.SetActive(true);
        PaintingSignature.SetActive(true);

        //SCR_PlayerMovement PlayerMovement = GetComponent<SCR_PlayerMovement>();
        //CHAR_Player.GetComponent<SCR_PlayerMovement>();
    }

    void FixedUpdate()
    {

        Vector3 pos = transform.position;
        //Pan Up
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness || Input.GetAxis("Mouse Y") > 0)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        //Pan Down
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness || Input.GetAxis("Mouse Y") < 0)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        //Pan Left
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness || Input.GetAxis("Mouse X") > 0)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        //Pan Right
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness || Input.GetAxis("Mouse X") < 0)
        {
            //Set pan speed
            pos.x -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.z += scroll * scrollSpeed * 100f * Time.deltaTime;
        //Camera Boundaries
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            enter = true;
            IntPanel.SetActive(true);
            PaintingInteractText.SetActive(true);
            
        }


    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            enter = false;
            IntPanel.SetActive(false);
            PaintingInteractText.SetActive(false);
            
        }


    }

    void Update()
    {
        Debug.Log("last pressed is " + isInspecting.ToString());
        Debug.Log(Player.active);


        //Toggle Inspection On

        if (enter == true && Input.GetButtonDown("Interact"))
        {
            if (isInspecting == false)
            {
                PlayerCam.enabled = false;
                PaintingCam.enabled = true;
                Player.SetActive(false);
                isInspecting = true;
            }
            else
            {
                PlayerCam.enabled = true;
                PaintingCam.enabled = false;
                isInspecting = false;
                Player.SetActive(true);
            }

            //changing from setactive to disablig the character controller that allows movement ,
            //this way the player cannot move while inspecting the painting
            //PlayerCC.enabled = false;
            PlayerCamScript.enabled = false;



            PaintingInteractText.SetActive(false);
            



            if (isInspecting == false)
            {
                Debug.Log("last pressed is working");
                //Reset button 0
                isInspecting = true;
                PlayerCC.enabled = true;
                PlayerCamScript.enabled = true;
                PaintingInteractText.SetActive(true);
                
                Player.SetActive(true);
            }




        }

        else if (enter && PaintingCam.enabled)
        {
            //This shits works
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log(hit.transform.tag);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }

        }
    }
}
