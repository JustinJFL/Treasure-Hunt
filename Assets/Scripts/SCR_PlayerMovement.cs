using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayerMovement : MonoBehaviour
{
    public AudioClip speakingClip;
    public CharacterController controller;
    public float speedDampTime = 0.1f;
    public float speed = 12f;
    public static Vector3 playerPos;
    private Rigidbody rig;
    CharacterController cc;
    private Animator anim;
    //private HashIDs hash;
    public GameObject footstepsSFX;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        //Set Animation layer order
    }

    void Start()
    {
        //StartCoroutine(TrackPlayer());

    }

    IEnumerator TrackPlayer()
    {
        while(true)
        {
            playerPos = gameObject.transform.position;
            yield return null;
        }
    }

    void FixedUpdate()
    { //Sneaking
        bool sneak = Input.GetButton("Sneak");
    }
    //void AudioManager(bool speak)
            //{
                //if(anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.locomotionState)
                //{
                    //if(!GetComponent<AudioSource>().isPlaying)
                    //{
                        //GetComponent<AudioSource>().Play ();
                    //}
                //}
                //else
                //{
                    //GetComponent<AudioSource>().Stop();
                //}

                //if(speak)
                //{
                    //AudioSource.PlayClipAtPoint(speakingClip, transform.position);
                //}
            //}
    void Update()
    { //Conditional to allow freezing of movement in SCR_Painting_Camera
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
       if(x != 0 || z != 0)
        {
        //Activates footstep sound gameobject when walking.
        footstepsSFX.SetActive(true);
        anim.SetBool("IsWalking", true);
        }
        else 
        {
            footstepsSFX.SetActive(false);
            anim.SetBool("IsWalking", false);
        }
        

        //bool speak = Input.GetButtonDown("Speak");
        //anim.SetBool(hash.speakingBool, speak);
        //AudioManager(speak);

    }



}
